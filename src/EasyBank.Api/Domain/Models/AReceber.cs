using System.ComponentModel.DataAnnotations;

namespace EasyBank.Api.Domain.Models
{
    public class AReceber : Titulo
    {
        [Required(ErrorMessage = "O campo de valor recebido é obrigatório.")]
        public double ValorRecebido { get; set; }


        public DateTime? DataRecebimento { get; set; }

    }
}