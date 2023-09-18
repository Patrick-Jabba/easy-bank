namespace EasyBank.Api.DTO.ModelErrorDTO
{
    public class ModelErrorDTO
    {
        public int StatusCode { get; set; }

        public string Titulo { get; set; }

        public string Mensagem { get; set; }

        public DateTime MomentoOcorrencia { get; set; }
    }
}