using System.ComponentModel.DataAnnotations;

namespace Bet.API.Models
{
    public class UpdateTimeModel
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(50, ErrorMessage = "O campo Nome não pode ter mais de 50 caracteres.")]
        public string Nome { get; set; }

        public int QuantidadeSocios { get; set; }

        [StringLength(100, ErrorMessage = "O campo Nome Presidente não pode ter mais de 100 caracteres.")]
        public string NomePresidente { get; set; }


        [StringLength(100, ErrorMessage = "O campo Nome Treinador não pode ter mais de 100 caracteres.")]
        public string NomeTreinador { get; set; }

        public Boolean IndAtivo { get; set; }
    }
}
