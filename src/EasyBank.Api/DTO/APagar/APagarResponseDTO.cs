namespace EasyBank.Api.DTO.APagar
{
    public class APagarResponseDTO : APagarRequestDTO
    {
        public long Id { get; set; }
        public long IdUsuario { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataInativacao { get; set; }
    }
}