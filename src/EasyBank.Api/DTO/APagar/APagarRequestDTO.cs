namespace EasyBank.Api.DTO.APagar
{
    public class APagarRequestDTO
    {
        public long IdNaturezaDeLancamento { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string Observacao { get; set; } = string.Empty;

        public double ValorOriginal { get; set; }

        public double ValorPago {get; set;}

        public DateTime DataVencimento { get; set; }

        public DateTime? DataReferencia { get; set; }

        public DateTime? DataPagamento {get; set;}
    }
}