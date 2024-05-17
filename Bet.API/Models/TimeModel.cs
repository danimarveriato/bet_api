using System.ComponentModel.DataAnnotations;

namespace Bet.API.Models
{
    public class TimeModel
    {
        public int Codigo { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(50, ErrorMessage = "O campo Nome não pode ter mais de 50 caracteres.")]
        public string Nome { get; set; }
        
        [StringLength(100, ErrorMessage = "O campo Cidade não pode ter mais de 100 caracteres.")]
        public string Cidade { get; set; }
        
        [StringLength(30, ErrorMessage = "O campo Estado não pode ter mais de 30 caracteres.")]
        public string Estado { get; set; }

        public DateTime? DataFundacao { get; set; }

        public int QuantidadeSocios { get; set; }

        [StringLength(100, ErrorMessage = "O campo Nome Presidente não pode ter mais de 100 caracteres.")]
        public string NomePresidente { get; set; }

        [StringLength(100, ErrorMessage = "O campo Nome Estadio não pode ter mais de 100 caracteres.")]
        public string NomeEstadio { get; set; }

        [StringLength(100, ErrorMessage = "O campo Nome Treinador não pode ter mais de 100 caracteres.")]
        public string NomeTreinador { get; set; }

        [StringLength(100, ErrorMessage = "O campo Site não pode ter mais de 60 caracteres.")]
        public string Site { get; set; }
        public Boolean IndAtivo { get; set; }
    }
}
