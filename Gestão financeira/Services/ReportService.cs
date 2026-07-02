using GestaoFinanceira.Models;
using GestaoFinanceira.Repositories;

namespace GestaoFinanceira.Services {
    public class ReportService {
        private readonly TransactionRepository _transactionRepository = new();
        private readonly CategoryRepository _categoryRepository = new();

        public void ExibirExtrato(int userId) {
            var transactions = _transactionRepository.ListarPorUsuario(userId);

            if (transactions.Count == 0) {
                Console.WriteLine("Nenhuma transação encontrada.");
                return;
            }

            Console.WriteLine("\n===== EXTRATO COMPLETO =====");
            foreach (var t in transactions) {
                var sinal = t.Tipo == "receita" ? "+" : "-";
                Console.WriteLine($"{t.Data:dd/MM/yyyy} | {sinal}R${t.Valor:F2} | {t.Descricao}");
            }

            ExibirSaldo(transactions);
        }

        public void ExibirPorPeriodo(int userId, DateTime inicio, DateTime fim) {
            var transactions = _transactionRepository.ListarPorPeriodo(userId, inicio, fim);

            if (transactions.Count == 0) {
                Console.WriteLine("Nenhuma transação encontrada no período.");
                return;
            }

            Console.WriteLine($"\n===== EXTRATO: {inicio:dd/MM/yyyy} a {fim:dd/MM/yyyy} =====");
            foreach (var t in transactions) {
                var sinal = t.Tipo == "receita" ? "+" : "-";
                Console.WriteLine($"{t.Data:dd/MM/yyyy} | {sinal}R${t.Valor:F2} | {t.Descricao}");
            }

            ExibirSaldo(transactions);
        }

        public void ExibirPorCategoria(int userId) {
            var transactions = _transactionRepository.ListarPorUsuario(userId);
            var categories = _categoryRepository.ListarPorUsuario(userId);

            Console.WriteLine("\n===== RELATÓRIO POR CATEGORIA =====");
            foreach (var cat in categories) {
                var total = transactions
                    .Where(t => t.CategoryId == cat.Id)
                    .Sum(t => t.Valor);

                if (total > 0)
                    Console.WriteLine($"{cat.Nome} ({cat.Tipo}): R${total:F2}");
            }

            ExibirSaldo(transactions);
        }

        private void ExibirSaldo(List<Transaction> transactions) {
            var receitas = transactions.Where(t => t.Tipo == "receita").Sum(t => t.Valor);
            var despesas = transactions.Where(t => t.Tipo == "despesa").Sum(t => t.Valor);
            var saldo = receitas - despesas;

            Console.WriteLine($"\nReceitas: +R${receitas:F2}");
            Console.WriteLine($"Despesas: -R${despesas:F2}");
            Console.WriteLine($"Saldo: R${saldo:F2}");
        }
    }
}
