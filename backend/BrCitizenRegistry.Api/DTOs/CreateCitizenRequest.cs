using System.ComponentModel.DataAnnotations;

namespace BrCitizenRegistry.Api.DTOs;

public class CreateCitizenRequest
{
    [Required(ErrorMessage = "O nome completo é obrigatório.")]
    [MinLength(3, ErrorMessage = "O nome completo deve possuir pelo menos 3 caracteres.")]
    [MaxLength(150, ErrorMessage = "O nome completo deve possuir no máximo 150 caracteres.")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "O CPF é obrigatório.")]
    public string Cpf { get; set; } = string.Empty;
}