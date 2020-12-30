using AutomacaoMantis.Pages;

namespace AutomacaoMantis.Flows
{
    public class ManageUserFlows
    {
        #region Page Object and Constructor
        ManageUserPage manageUserPage;

        public ManageUserFlows()
        {
            manageUserPage = new ManageUserPage();
        }
        #endregion

        public void PesquisarUsuarioAtivado(string username)
        {
            manageUserPage.AbrirManageUserPage();
            manageUserPage.PesquisarUsuario(username);
            manageUserPage.ClicarAplicarFiltro();
        }
    }
}