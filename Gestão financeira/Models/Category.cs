namespace GestaoFinanceira.Models {
    public class Category {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; } // "receita" ou "despesa"
        public int UserId { get; set; }
    }
}