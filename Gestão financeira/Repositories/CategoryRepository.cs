using GestaoFinanceira.Database;
using GestaoFinanceira.Models;
using MySql.Data.MySqlClient;

namespace GestaoFinanceira.Repositories {
    public class CategoryRepository {
        public void Criar(Category category) {
            using var conn = DatabaseConfig.GetConnection();
            var cmd = new MySqlCommand(
                "INSERT INTO Categories (nome, tipo, user_id) VALUES (@nome, @tipo, @userId)", conn);
            cmd.Parameters.AddWithValue("@nome", category.Nome);
            cmd.Parameters.AddWithValue("@tipo", category.Tipo);
            cmd.Parameters.AddWithValue("@userId", category.UserId);
            cmd.ExecuteNonQuery();
        }

        public List<Category> ListarPorUsuario(int userId) {
            using var conn = DatabaseConfig.GetConnection();
            var cmd = new MySqlCommand(
                "SELECT * FROM Categories WHERE user_id = @userId", conn);
            cmd.Parameters.AddWithValue("@userId", userId);

            var categories = new List<Category>();
            using var reader = cmd.ExecuteReader();
            while (reader.Read()) {
                categories.Add(new Category {
                    Id = reader.GetInt32("id"),
                    Nome = reader.GetString("nome"),
                    Tipo = reader.GetString("tipo"),
                    UserId = reader.GetInt32("user_id")
                });
            }
            return categories;
        }

        public Category? BuscarPorId(int id) {
            using var conn = DatabaseConfig.GetConnection();
            var cmd = new MySqlCommand("SELECT * FROM Categories WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read()) {
                return new Category {
                    Id = reader.GetInt32("id"),
                    Nome = reader.GetString("nome"),
                    Tipo = reader.GetString("tipo"),
                    UserId = reader.GetInt32("user_id")
                };
            }
            return null;
        }
    }
}