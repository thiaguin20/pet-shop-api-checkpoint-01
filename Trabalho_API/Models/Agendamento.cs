using System.ComponentModel.DataAnnotations;

namespace Trabalho_API.Models
{
    public class Agendamento
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Informe um pet válido.")]
        public int PetId { get; set; }

        [Required(ErrorMessage = "A data e a hora são obrigatórias.")]
        public DateTime DataHora { get; set; }

        [Required(ErrorMessage = "O serviço é obrigatório.")]
        [StringLength(100, ErrorMessage = "O serviço deve ter no máximo 100 caracteres.")]
        public string Servico { get; set; } = string.Empty;

        [Required(ErrorMessage = "O status é obrigatório.")]
        [RegularExpression("^(Agendado|Concluido|Cancelado)$", ErrorMessage = "O status deve ser Agendado, Concluido ou Cancelado.")]
        public string Status { get; set; } = string.Empty;

        public Pet? Pet { get; set; }
    }
}
