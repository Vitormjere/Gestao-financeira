using GestaoFinanceira.Models;
using GestaoFinanceira.Repositories;
using GestaoFinanceira.Services;

namespace GestaoFinanceira.UI {
    public class MenuTransacoes {
        private readonly TransactionService _transactionService;
        private readonly CategoryRepository _categoryRepository;
        private readonly int _userId;

        public MenuTransacoes(TransactionService transactionService, int userId) {
            _transactionService = transactionService;
            _categoryRepository = new CategoryRepository();
            _userId = userId;
        }

        public void Exibir() {
            while (true) {
                Console.Clear();
                Console.WriteLine("===== TRANSAÇÕES =====");
                Console.WriteLine("1. Adicionar transação");
                Console.WriteLine("2. Listar transações");
                Console.WriteLine("0. Voltar");
                Console.Write("\nEscolha: ");

                var opcao = Console.ReadLine();

                switch (opcao) {
                    case "1":
                        AdicionarTransacao();
                        break;
                    case "2":
                        ListarTransacoes();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void AdicionarTransacao() {
            Console.Clear();
            Console.WriteLine("===== NOVA TRANSAÇÃO =====");

            Console.Write("Tipo (receita/despesa): ");
            var tipo = Console.ReadLine() ?? "";

            if (tipo != "receita" && tipo != "despesa") {
                Console.WriteLine("Tipo inválido!");
                Console.ReadKey();
                return;
            }

            var categories = _categoryRepository.ListarPorUsuario(_userId)
                .Where(c => c.Tipo == tipo).ToList();

            if (categories.Count == 0) {
                Console.WriteLine($"Nenhuma categoria de {tipo} encontrada. Crie uma primeiro!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nCategorias disponíveis:");
            foreach (var cat in categories)
                Console.WriteLine($"{cat.Id}. {cat.Nome}");

            Console.Write("\nID da categoria: ");
            if (!int.TryParse(Console.ReadLine(), out int categoryId)) {
                Console.WriteLine("ID inválido!");
                Console.ReadKey();
                return;
            }

            Console.Write("Descrição: ");
            var descricao = Console.ReadLine() ?? "";

            Console.Write("Valor: R$");
            if (!decimal.TryParse(Console.ReadLine(), out decimal valor)) {
                Console.WriteLine("Valor inválido!");
                Console.ReadKey();
                return;
            }

            _transactionService.AdicionarTransacao(_userId, descricao, valor, tipo, categoryId);
            Console.ReadKey();
        }

        private void ListarTransacoes() {
            Console.Clear();
            var transactions = _transactionService.ListarTransacoes(_userId);

            if (transactions.Count == 0) {
                Console.WriteLine("Nenhuma transação encontrada.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("===== SUAS TRANSAÇÕES =====");
            foreach (var t in transactions) {
                var sinal = t.Tipo == "receita" ? "+" : "-";
                Console.WriteLine($"{t.Data:dd/MM/yyyy} | {sinal}R${t.Valor:F2} | {t.Descricao}");
            }

            Console.ReadKey();
        }
    }
}