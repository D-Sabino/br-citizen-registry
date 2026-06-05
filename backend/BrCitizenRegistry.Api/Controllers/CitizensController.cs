using BrCitizenRegistry.Application.DTOs;
using BrCitizenRegistry.Application.Ports.In;
using Microsoft.AspNetCore.Mvc;

namespace BrCitizenRegistry.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitizensController : ControllerBase
{
    private readonly ICitizenUseCase _citizenUseCase;

    public CitizensController(ICitizenUseCase citizenUseCase)
    {
        _citizenUseCase = citizenUseCase;
    }

    [HttpPost]
    public async Task<ActionResult<CitizenResponse>> CreateAsync([FromBody] CreateCitizenRequest request)
    {
        try
        {
            var citizen = await _citizenUseCase.CreateAsync(request);

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

        try
        {
            var citizens = await _citizenUseCase.SearchAsync(term);

            if (citizens.Count == 0)
            {
                return NotFound(new
                {
                    message = "Cidadão não encontrado"
                });
            }

            return Ok(citizens);
        }
        catch (InvalidOperationException exception)
        {
            return BadRequest(new
            {
                message = exception.Message
            });
        }
    }
}