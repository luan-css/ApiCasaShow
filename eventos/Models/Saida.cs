using System;

namespace eventos.Models
{
    public class Saida
    {
        public int Id { get; set; }

        public Evento Evento{ get; set; }

        public float ValorDaVenda{get; set;}

        public DateTime Data{get; set;}
    }
}