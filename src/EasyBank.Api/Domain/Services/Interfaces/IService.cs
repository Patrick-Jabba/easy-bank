namespace EasyBank.Api.Domain.Services.Interfaces
{
    /// <summary>
    /// Interface genérica para criação de serviços do tipo CRUD.
    /// </summary>
    /// <typeparam name="Request">Contrato de request</typeparam>
    /// <typeparam name="Response">Contrato de response</typeparam>
    /// <typeparam name="Identificador">Tipo do Id</typeparam>
    public interface IService<Request, Response, Identificador> where Request : class
    {
        Task<IEnumerable<Response>> Obter();

        Task<Response> ObterPorId(Identificador id);

        Task<Response> Adicionar(Request entidade, Identificador idUsuario);
        Task<Response> Atualizar(Identificador id, Request entidade, Identificador idUsuario);

        Task Inativar(Identificador id);
    }
}