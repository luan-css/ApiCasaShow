using System;
using System.ComponentModel.DataAnnotations;


namespace eventos.DTO
{
    public class EventoDTO
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage="Nome do evento obrigatório")]
        [StringLength(100, ErrorMessage="Nome do evento muito grande, tente um menor!")]
        [MinLength(3, ErrorMessage="Nome muito curto, tente com mais de 2 caracteres.")]
        public string Nome{get; set;}
        [Required(ErrorMessage="Digite a capacidade do local")]
        public int capacidade{get; set;}
        [Required(ErrorMessage="Digite a data e a hora do local")]
        public  DateTime Data{get; set;}
        [Required(ErrorMessage="Digite o valor do ingresso")]
        public float ValorIngresso { get; set; }
        [Required(ErrorMessage="Escolha a casa de show")]
        public int CasaID{get; set;}
        [Required(ErrorMessage="Escolha o genero")]
        public int GeneroID{get; set;}
        [Required(ErrorMessage="Coloque o link de uma imagem")]
        public string Imagem{get; set;}
        public int Quantidade{get; set;}
        public string usuario{get; set;}
      
    }
    
}