using GestaoFinanceira.Models;
using GestaoFinanceira.Repositories;

namespace GestaoFinanceira.Services {
    public class TransactionService {
        private readonly TransactionRepository _transactionRepository = new();
        private readonly CategoryRepository _categoryRepository = new();

        public void AdicionarTransacao(int userId, string descricao, decimal valor, string tipo, int categoryId) {
            var category = _categoryRepository.BuscarPorId(categoryId);
            if (category == null || category.UserId != userId) {
                Console.WriteLine("Categoria inválida!");
                return;
            }

            if (category.Tipo != tipo) {
                Console.WriteLine($"Essa categoria é do tipo '{category.Tipo}', não '{tipo}'!");
                return;
            }

            var transaction = new Transaction {
                Descricao = descricao,
                Valor = valor,
                Tipo = tipo,
                Data = DateTime.Now,
                CategoryId = categoryId,
                UserId = userId
            };

            _transactionRepository.Criar(transaction);
            Console.WriteLine("Transação adicionada com sucesso!");
        }

        public List<Transaction> ListarTransacoes(int userId) {
            return _transactionRepository.ListarPorUsuario(userId);
        }

        public List<Transaction> ListarPorPeriodo(int userId, DateTime inicio, DateTime fim) {
            return _transactionRepository.ListarPorPeriodo(userId, inicio, fim);
        }
    }
}