using GestaoFinanceira.Models;
using GestaoFinanceira.Repositories;

namespace GestaoFinanceira.UI {
    public class MenuCategorias {
        private readonly CategoryRepository _categoryRepository;
        private readonly int _userId;

        public MenuCategorias(int userId) {
            _categoryRepository = new CategoryRepository();
            _userId = userId;
        }

        public void Exibir() {
            while (true) {
                Console.Clear();
                Console.WriteLine("===== CATEGORIAS =====");
                Console.WriteLine("1. Nova categoria");
                Console.WriteLine("2. Listar categorias");
                Console.WriteLine("0. Voltar");
                Console.Write("\nEscolha: ");

                var opcao = Console.ReadLine();

                switch (opcao) {
                    case "1":
                        NovaCategorias();
                        break;
                    case "2":
                        ListarCategorias();
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

        private void NovaCategorias() {
            Console.Clear();
            Console.WriteLine("===== NOVA CATEGORIA =====");

            Console.Write("Nome: ");
            var nome = Console.ReadLine() ?? "";

            Console.Write("Tipo (receita/despesa): ");
            var tipo = Console.ReadLine() ?? "";

            if (tipo != "receita" && tipo != "despesa") {
                Console.WriteLine("Tipo inválido!");
                Console.ReadKey();
                return;
            }

            var category = new Category {
                Nome = nome,
                Tipo = tipo,
                UserId = _userId
            };

            _categoryRepository.Criar(category);
            Console.WriteLine("Categoria criada com sucesso!");
            Console.ReadKey();
        }

        private void ListarCategorias() {
            Console.Clear();
            var categories = _categoryRepository.ListarPorUsuario(_userId);

            if (categories.Count == 0) {
                Console.WriteLine("Nenhuma categoria encontrada.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("===== SUAS CATEGORIAS =====");
            foreach (var cat in categories)
                Console.WriteLine($"{cat.Id}. {cat.Nome} ({cat.Tipo})");

            Console.ReadKey();
        }
    }
}
