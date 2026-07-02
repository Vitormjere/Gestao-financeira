using GestaoFinanceira.Services;

namespace GestaoFinanceira.UI {
    public class MenuPrincipal {
        private readonly AuthService _authService;
        private readonly TransactionService _transactionService;
        private readonly ReportService _reportService;

        public MenuPrincipal(AuthService authService) {
            _authService = authService;
            _transactionService = new TransactionService();
            _reportService = new ReportService();
        }

        public void Exibir() {
            var userId = _authService.UsuarioLogado!.Id;
            var nome = _authService.UsuarioLogado!.Nome;

            while (true) {
                Console.Clear();
                Console.WriteLine($"===== BEM-VINDO, {nome.ToUpper()} =====");
                Console.WriteLine("1. Transações");
                Console.WriteLine("2. Categorias");
                Console.WriteLine("3. Relatórios");
                Console.WriteLine("0. Sair");
                Console.Write("\nEscolha: ");

                var opcao = Console.ReadLine();

                switch (opcao) {
                    case "1":
                        new MenuTransacoes(_transactionService, userId).Exibir();
                        break;
                    case "2":
                        new MenuCategorias(userId).Exibir();
                        break;
                    case "3":
                        new MenuRelatorios(_reportService, userId).Exibir();
                        break;
                    case "0":
                        _authService.Logout();
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
