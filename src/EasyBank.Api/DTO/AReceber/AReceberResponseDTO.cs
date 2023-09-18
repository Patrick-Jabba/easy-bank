namespace EasyBank.Api.DTO.AReceber
{
    public class AReceberResponseDTO : AReceberRequestDTO
    {
        public long Id { get; set; }
        public long IdUsuario { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataInativacao { get; set; }
    }
}