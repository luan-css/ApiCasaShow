namespace eventos.Models
{
    public class Compra
    {
        public int Id { get; set; }
        public string Nome{get; set;}
        public int capacidade{get; set;}
        public float ValorIngresso { get; set; }
        public float total{get; set;}
        public int Quantidade{get; set;}
        public string usuario{get; set;}
        public string imagem{get; set;}
    }
}