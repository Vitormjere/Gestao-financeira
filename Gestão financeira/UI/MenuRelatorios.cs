using GestaoFinanceira.Services;

namespace GestaoFinanceira.UI {
    public class MenuRelatorios {
        private readonly ReportService _reportService;
        private readonly int _userId;

        public MenuRelatorios(ReportService reportService, int userId) {
            _reportService = reportService;
            _userId = userId;
        }

        public void Exibir() {
            while (true) {
                Console.Clear();
                Console.WriteLine("===== RELATÓRIOS =====");
                Console.WriteLine("1. Extrato completo");
                Console.WriteLine("2. Extrato por período");
                Console.WriteLine("3. Relatório por categoria");
                Console.WriteLine("0. Voltar");
                Console.Write("\nEscolha: ");

                var opcao = Console.ReadLine();

                switch (opcao) {
                    case "1":
                        Console.Clear();
                        _reportService.ExibirExtrato(_userId);
                        Console.ReadKey();
                        break;

                    case "2":
                        ExtratoporPeriodo();
                        break;

                    case "3":
                        Console.Clear();
                        _reportService.ExibirPorCategoria(_userId);
                        Console.ReadKey();
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

        private void ExtratoporPeriodo() {
            Console.Clear();
            Console.WriteLine("===== EXTRATO POR PERÍODO =====");

            Console.Write("Data início (dd/MM/yyyy): ");
            if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy",
                null, System.Globalization.DateTimeStyles.None, out DateTime inicio)) {
                Console.WriteLine("Data inválida!");
                Console.ReadKey();
                return;
            }

            Console.Write("Data fim (dd/MM/yyyy): ");
            if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy",
                null, System.Globalization.DateTimeStyles.None, out DateTime fim)) {
                Console.WriteLine("Data inválida!");
                Console.ReadKey();
                return;
            }

            _reportService.ExibirPorPeriodo(_userId, inicio, fim);
            Console.ReadKey();
        }
    }
}