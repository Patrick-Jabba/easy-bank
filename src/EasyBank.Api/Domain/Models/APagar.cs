using System.ComponentModel.DataAnnotations;

namespace EasyBank.Api.Domain.Models
{
    public class APagar
    {
        [Key]
        public long Id { get; set; }


        [Required(ErrorMessage = "O campo de descrição é obrigatório.")]
        public string Descricao { get; set; } = string.Empty;


        [Required(ErrorMessage = "O campo de valor original é obrigatório.")]
        public double ValorOriginal { get; set; }


        [Required(ErrorMessage = "O campo de valor pago é obrigatório.")]
        public double ValorPago { get; set; }


        public string? Observacao { get; set; } = string.Empty;
        

        [Required]
        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "O campo de data de vencimento é obrigatório.")]
        public DateTime DataVencimento { get; set; }

        public DateTime? DataInativacao { get; set; }

        public DateTime? DataReferencia { get; set; }
        
        public DateTime? DataPagamento { get; set; }

        [Required]
        public long IdUsuario { get; set; }

        public Usuario? Usuario { get; set; }

        [Required]
        public long IdNaturezaDeLancamento { get; set; }

        public NaturezaDeLancamento? NaturezaDeLancamento { get; set; }
    }
}