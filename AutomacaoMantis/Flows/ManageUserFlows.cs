using AutomacaoMantis.Pages;

namespace AutomacaoMantis.Flows
{
    public class ManageUserFlows
    {
        #region Page Object and Constructor
        MainPage mainPage;
        ManageUserPage manageUserPage;

        public ManageUserFlows()
        {
            mainPage = new MainPage();
            manageUserPage = new ManageUserPage();
        }
        #endregion

        public void PesquisarUsuarioAtivado(string menu, string username)
        {
            mainPage.ClicarMenu(menu);
            manageUserPage.PesquisarUsuario(username);
            manageUserPage.ClicarAplicarFiltro();
        }
    }
}