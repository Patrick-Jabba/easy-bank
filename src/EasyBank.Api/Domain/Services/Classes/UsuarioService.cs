using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using EasyBank.Api.Domain.Models;
using EasyBank.Api.Domain.Repository.Interfaces;
using EasyBank.Api.Domain.Services.Interfaces;
using EasyBank.Api.DTO.Usuario;
using EasyBank.Api.Exceptions;

namespace EasyBank.Api.Domain.Services.Classes
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        private readonly TokenService _tokenService;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper, TokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

         public async Task<UsuarioLoginResponseDTO> Autenticar(UsuarioLoginRequestDTO usuarioLoginRequest)
        {
            UsuarioResponseDTO usuario = await ObterUsuarioPorEmail(usuarioLoginRequest.Email);

            var hashSenha = GerarHashSenha(usuarioLoginRequest.Senha);

            if(usuario is null || usuario.Senha != hashSenha)
            {
                throw new AuthenticationException("Usuário ou senha inválidos.");
            }

            return new UsuarioLoginResponseDTO
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Token = _tokenService.GerarToken(_mapper.Map<Usuario>(usuario))
            };
        }
        public async Task<UsuarioResponseDTO> Adicionar(UsuarioRequestDTO entidade, long idUsuario)
        {
            var usuario = _mapper.Map<Usuario>(entidade);

            usuario.Senha = GerarHashSenha(usuario.Senha);
            usuario.DataCadastro = DateTime.Now;

            usuario = await _usuarioRepository.Adicionar(usuario);

            return _mapper.Map<UsuarioResponseDTO>(usuario);
        }

        public async Task<UsuarioResponseDTO> Atualizar(long id, UsuarioRequestDTO entidade, long idUsuario)
        {
            _ = await ObterPorId(id, idUsuario) ?? throw new NotFoundException("Usuário não encontrado para atualização.");

            var usuario = _mapper.Map<Usuario>(entidade);

            usuario.Id = id;
            usuario.Senha = GerarHashSenha(entidade.Senha);
            usuario = await _usuarioRepository.Atualizar(usuario);

            return _mapper.Map<UsuarioResponseDTO>(usuario);
        }

        public async Task Inativar(long id, long idUsuario)
        {
            var usuario = await _usuarioRepository.ObterPorId(id) ?? throw new NotFoundException("Usuário não encontrado para inativação.");

            await _usuarioRepository.Deletar(_mapper.Map<Usuario>(usuario));
        }

        public async Task<IEnumerable<UsuarioResponseDTO>> Obter()
        {
            var usuarios = await _usuarioRepository.Obter();

            return usuarios.Select(usuario => _mapper.Map<UsuarioResponseDTO>(usuario));
        }

        public async Task<UsuarioResponseDTO> ObterPorId(long id, long idUsuario)
        {
            var usuario = await _usuarioRepository.ObterPorId(id);

            return _mapper.Map<UsuarioResponseDTO>(usuario);
        }

        public async Task<UsuarioResponseDTO> ObterUsuarioPorEmail(string email)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorEmail(email);

            return _mapper.Map<UsuarioResponseDTO>(usuario);
        }

        private string GerarHashSenha(string senha)
        {
            string hashSenha;

            using(SHA256 sha256 = SHA256.Create())
            {
                byte[] bytesSenha = Encoding.UTF8.GetBytes(senha);
                byte[] byteHashSenha = sha256.ComputeHash(bytesSenha);

                hashSenha = BitConverter.ToString(byteHashSenha).Replace("-","").ToLower();
            }

            return hashSenha;
        }
    }
}