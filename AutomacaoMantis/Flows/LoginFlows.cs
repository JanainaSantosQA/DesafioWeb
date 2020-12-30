using AutomacaoMantis.Pages;

namespace AutomacaoMantis.Flows
{
    public class LoginFlows
    {
        #region Page Object and Constructor
        LoginPage loginPage;

        public LoginFlows()
        {
            loginPage = new LoginPage();
        }
        #endregion

        public void EfetuarLogin(string username, string password)
        {
            loginPage.PreencherUsuario(username);
            loginPage.ClicarEmLogin();
            loginPage.PreencherSenha(password);
            loginPage.ClicarEmLogin();
        }
    }
}