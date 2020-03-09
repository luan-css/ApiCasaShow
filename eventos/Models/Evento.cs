using System;

namespace eventos.Models
{
    public class Evento
    {
        public int Id { get; set; }
        public string Nome{get; set;}
        public int capacidade{get; set;}
        public  DateTime Data{get; set;}
        public float ValorIngresso { get; set; }
        public Casa Casa{get; set;}
        public Genero Genero{get; set;}
        public bool Status{get; set;}
        public int Quantidade{get; set;}
        public string imagem{get; set;}
    }
}