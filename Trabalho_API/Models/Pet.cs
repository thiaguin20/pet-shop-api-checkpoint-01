using System.ComponentModel.DataAnnotations;

namespace Trabalho_API.Models
{
    public class Pet
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do pet é obrigatório.")]
        [StringLength(80, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 80 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A espécie é obrigatória.")]
        [StringLength(50, ErrorMessage = "A espécie deve ter no máximo 50 caracteres.")]
        public string Especie { get; set; } = string.Empty;

        [StringLength(80, ErrorMessage = "A raça deve ter no máximo 80 caracteres.")]
        public string Raca { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Informe um cliente válido.")]
        public int ClienteId { get; set; }

        public Cliente? Cliente { get; set; }

        public ICollection<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();
    }
}
