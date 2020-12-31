using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class ManageUserEditPage : PageBase
    {
        #region Mapping
        By nomeUsuarioLink = By.XPath("//*[@id='main-container']//tbody/tr/td[1]/a");
        By usernameField = By.Id("edit-username");
        By realnameField = By.Id("edit-realname");
        By emailField = By.Id("email-field");
        By atualizarUsuarioButton = By.XPath("//input[@value='Atualizar Usuário']");        
        #endregion

        #region Actions
        public void ClicarNomeUsuario()
        {
            Click(nomeUsuarioLink);
        }

        public void PreencherUsername(string username)
        {
            SendKeys(usernameField, username);
        }

        public void PreencherEmail(string email)
        {
            SendKeys(emailField, email);
        }

        public void PreencherRealname(string realname)
        {
            SendKeys(realnameField, realname);
        }

        public void ClicarAtualizarUsuario()
        {
            Click(atualizarUsuarioButton);
        }

        public void LimparUsername()
        {
            Clear(usernameField);
        }

        public void LimparRealname()
        {
            Clear(realnameField);
        }

        public void LimparEmail()
        {
            Clear(emailField);
        }
        #endregion
    }
}