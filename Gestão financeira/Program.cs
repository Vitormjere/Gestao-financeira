using GestaoFinanceira.Database;
using GestaoFinanceira.Services;
using GestaoFinanceira.UI;

DatabaseInit.Verificar();

var authService = new AuthService();
var menuAuth = new MenuAuth(authService);

while (true) {
    var logado = menuAuth.Exibir();
    if (!logado) break;

    var menuPrincipal = new MenuPrincipal(authService);
    menuPrincipal.Exibir();
}