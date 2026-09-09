namespace Trabalho_API.Models
{
    public class Agendamento
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public DateTime DataHora { get; set; }
        public string Servico { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
