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
        By adicionarUsuarioButton = By.XPath("//input[@value='Adicionar Usuário']");
        By atualizarUsuarioButton = By.XPath("//input[@value='Atualizar Usuário']");
        By redefinirSenhaButton = By.XPath("//input[@value='Redefinir Senha']");
        By apagarUsuarioButton = By.XPath("//input[@value='Apagar Usuário']");
        public By ProjectSelect(string projectName)
        {
            return By.XPath("//select[@id='add-user-project-id']/option[text()='" + projectName + "']");
        }
        #endregion

        #region Actions
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

        public void ClicarAdicionarUsuario()
        {
            Click(adicionarUsuarioButton);
        }

        public void ClicarAtualizarUsuario()
        {
            Click(atualizarUsuarioButton);
        }

        public void ClicarNomeUsuario(string username)
        {
            VerifyTextElement(nomeUsuarioLink, username);
            Click(nomeUsuarioLink);
        }

        public void ClicarProjectSelect(string projectName)
        {
            Click(ProjectSelect(projectName));
        }

        public void ClicarRedefinirSenha()
        {
            Click(redefinirSenhaButton);
        }

        public void ClicarApagarUsuario()
        {
            Click(apagarUsuarioButton);
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