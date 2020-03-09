using System.ComponentModel.DataAnnotations;

namespace eventos.DTO
{
    public class GeneroDTO
    {

        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage="Nome de genero muito grande, tente um menor!")]
        [MinLength(2, ErrorMessage="Nome muito curto, tente com mais de 2 caracteres.")]
        public string Nome { get; set; }
    }
}