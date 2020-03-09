using System.ComponentModel.DataAnnotations;

namespace eventos.DTO
{
    public class CasaDTO
    {

        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage="Nome obrigatório")]
        [StringLength(100, ErrorMessage="Nome da casa muito grande, tente um menor!")]
        [MinLength(2, ErrorMessage="Nome muito curto, tente com mais de 2 caracteres.")]
        public string Nome{get; set;}
        [Required(ErrorMessage="Endereço obrigatório")]
        [StringLength(250, ErrorMessage="Nome do endereço muito grande, tente um menor!")]
        [MinLength(2, ErrorMessage="Endereço muito curto, tente com mais de 2 caracteres.")]
        public string Endereco{get; set;}
    }
}