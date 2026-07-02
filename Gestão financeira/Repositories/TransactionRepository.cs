using GestaoFinanceira.Database;
using GestaoFinanceira.Models;
using MySql.Data.MySqlClient;

namespace GestaoFinanceira.Repositories {
    public class TransactionRepository {
        public void Criar(Transaction transaction) {
            using var conn = DatabaseConfig.GetConnection();
            var cmd = new MySqlCommand(
                "INSERT INTO Transactions (descricao, valor, tipo, data, category_id, user_id) VALUES (@descricao, @valor, @tipo, @data, @categoryId, @userId)", conn);
            cmd.Parameters.AddWithValue("@descricao", transaction.Descricao);
            cmd.Parameters.AddWithValue("@valor", transaction.Valor);
            cmd.Parameters.AddWithValue("@tipo", transaction.Tipo);
            cmd.Parameters.AddWithValue("@data", transaction.Data);
            cmd.Parameters.AddWithValue("@categoryId", transaction.CategoryId);
            cmd.Parameters.AddWithValue("@userId", transaction.UserId);
            cmd.ExecuteNonQuery();
        }

        public List<Transaction> ListarPorUsuario(int userId) {
            using var conn = DatabaseConfig.GetConnection();
            var cmd = new MySqlCommand(
                "SELECT * FROM Transactions WHERE user_id = @userId ORDER BY data DESC", conn);
            cmd.Parameters.AddWithValue("@userId", userId);

            var transactions = new List<Transaction>();
            using var reader = cmd.ExecuteReader();
            while (reader.Read()) {
                transactions.Add(new Transaction {
                    Id = reader.GetInt32("id"),
                    Descricao = reader.GetString("descricao"),
                    Valor = reader.GetDecimal("valor"),
                    Tipo = reader.GetString("tipo"),
                    Data = reader.GetDateTime("data"),
                    CategoryId = reader.GetInt32("category_id"),
                    UserId = reader.GetInt32("user_id")
                });
            }
            return transactions;
        }

        public List<Transaction> ListarPorPeriodo(int userId, DateTime inicio, DateTime fim) {
            using var conn = DatabaseConfig.GetConnection();
            var cmd = new MySqlCommand(
                "SELECT * FROM Transactions WHERE user_id = @userId AND data BETWEEN @inicio AND @fim ORDER BY data DESC", conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@inicio", inicio);
            cmd.Parameters.AddWithValue("@fim", fim);

            var transactions = new List<Transaction>();
            using var reader = cmd.ExecuteReader();
            while (reader.Read()) {
                transactions.Add(new Transaction {
                    Id = reader.GetInt32("id"),
                    Descricao = reader.GetString("descricao"),
                    Valor = reader.GetDecimal("valor"),
                    Tipo = reader.GetString("tipo"),
                    Data = reader.GetDateTime("data"),
                    CategoryId = reader.GetInt32("category_id"),
                    UserId = reader.GetInt32("user_id")
                });
            }
            return transactions;
        }
    }
}