using AutomacaoMantis.Pages;

namespace AutomacaoMantis.Flows
{
    public class ManageUserFlows
    {
        #region Page Object and Constructor
        MyViewPage mainPage;
        ManageUserPage manageUserPage;
        ManageUserEditPage manageUserEditPage;

        public ManageUserFlows()
        {
            mainPage = new MyViewPage();
            manageUserPage = new ManageUserPage();
            manageUserEditPage = new ManageUserEditPage();
        }
        #endregion

        public void AcessarUsuarioCriadoAtivo(string menu, string username)
        {
            mainPage.ClicarMenu(menu);
            manageUserPage.PesquisarUsuario(username);
            manageUserPage.ClicarAplicarFiltro();
            manageUserEditPage.ClicarNomeUsuario(username);
        }
    }
}