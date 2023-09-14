namespace EasyBank.Api.DTO.NaturezaDeLancamento
{
    public class NaturezaDeLancamentoResponseDTO : NaturezaDeLancamentoRequestDTO
    {
        public long Id { get; set; }
        public long IdUsuario { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataInativacao { get; set; }
    }
}