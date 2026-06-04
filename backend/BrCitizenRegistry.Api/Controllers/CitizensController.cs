using BrCitizenRegistry.Api.DTOs;
using BrCitizenRegistry.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BrCitizenRegistry.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitizensController : ControllerBase
{
    private readonly ICitizenService _citizenService;

    public CitizensController(ICitizenService citizenService)
    {
        _citizenService = citizenService;
    }

    [HttpPost]
    public async Task<ActionResult<CitizenResponse>> CreateAsync([FromBody] CreateCitizenRequest request)
    {
        try
        {
            var citizen = await _citizenService.CreateAsync(request);

            return Created(string.Empty, citizen);
        }
        catch (InvalidOperationException exception)
        {
            return BadRequest(new
            {
                message = exception.Message
            });
        }
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<CitizenResponse>>> SearchAsync([FromQuery] string term)
    {
        if (string.IsNullOrWhiteSpace(term))
        {
            return BadRequest(new
            {
                message = "Informe um nome ou CPF para pesquisa."
            });
        }

        var citizens = await _citizenService.SearchAsync(term);

        if (citizens.Count == 0)
        {
            return NotFound(new
            {
                message = "Cidadão não encontrado"
            });
        }

        return Ok(citizens);
    }
}