using System.ComponentModel.DataAnnotations;

namespace EasyBank.Api.Domain.Models
{
    public class APagar : Titulo
    {
       [Required(ErrorMessage = "O campo de valor pago é obrigatório.")]
        public double ValorPago { get; set; }
        

         public DateTime? DataPagamento { get; set; }
    }
}