namespace GestaoFinanceira.Models {
    public class Transaction {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public string Tipo { get; set; } // "receita" ou "despesa"
        public DateTime Data { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}