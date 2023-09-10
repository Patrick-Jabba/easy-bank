using System.ComponentModel.DataAnnotations;

namespace EasyBank.Api.Domain.Models
{
    public class Usuario
    {
        [Key]
        public long Id { get; set;}

        [Required(ErrorMessage = "O campo e-mail é obrigatório.")]
        public string Email {get; set;} = string.Empty;
        
        [Required(ErrorMessage = "O campo senha é obrigatória.")]
        public string Senha {get; set;} = string.Empty;

        [Required]
        public DateTime DataCadastro {get; set;}

        public DateTime? DataInativacao {get; set;}

        public Usuario()
        {
            DataCadastro = DateTime.Now;
        }
    }
}