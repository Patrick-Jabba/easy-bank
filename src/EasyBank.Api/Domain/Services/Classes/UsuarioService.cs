using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using EasyBank.Api.Domain.Models;
using EasyBank.Api.Domain.Repository.Interfaces;
using EasyBank.Api.Domain.Services.Interfaces;
using EasyBank.Api.DTO.Usuario;

namespace EasyBank.Api.Domain.Services.Classes
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }
        public async Task<UsuarioResponseDTO> Adicionar(UsuarioRequestDTO entidade, long idUsuario)
        {
            var usuario = _mapper.Map<Usuario>(entidade);

            usuario.Senha = GerarHashSenha(usuario.Senha);

            usuario = await _usuarioRepository.Adicionar(usuario);

            return _mapper.Map<UsuarioResponseDTO>(usuario);
        }

        public Task<UsuarioResponseDTO> Atualizar(long id, UsuarioRequestDTO entidade, long idUsuario)
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioLoginResponseDTO> Autenticar(UsuarioLoginRequestDTO usuarioLoginRequest)
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioResponseDTO> Inativar(long id, long idUsuario)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UsuarioResponseDTO>> Obter(long idUsuario)
        {
            var usuarios = await _usuarioRepository.Obter();

            return usuarios.Select(usuario => _mapper.Map<UsuarioResponseDTO>(usuario));
        }

        public Task<UsuarioResponseDTO> ObterPorId(long id, long idUsuario)
        {
            throw new NotImplementedException();
        }

        private string GerarHashSenha(string senha)
        {
            string hashSenha;

            using(SHA256 sha256 = SHA256.Create())
            {
                byte[] bytesSenha = Encoding.UTF8.GetBytes(senha);
                byte[] byteHashSenha = sha256.ComputeHash(bytesSenha);

                hashSenha = BitConverter.ToString(byteHashSenha).ToLower();
            }

            return hashSenha;
        }
    }
}