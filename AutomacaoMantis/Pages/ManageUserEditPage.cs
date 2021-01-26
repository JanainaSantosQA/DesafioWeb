using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class ManageUserEditPage : PageBase
    {
        #region Mapping
        By userNameLink = By.XPath("//*[@id='main-container']//tbody/tr/td[1]/a");
        By usernameField = By.Id("edit-username");
        By realnameField = By.Id("edit-realname");
        By emailField = By.Id("email-field");
        private By ProjectNameSelect(string projectName)
        {
            return By.XPath("//select[@id='add-user-project-id']/option[text()='" + projectName + "']");
        }
        By addUserButton = By.XPath("//input[@value='Adicionar Usuário']");
        By updateUserButton = By.XPath("//input[@value='Atualizar Usuário']");
        By redefinePasswordButton = By.XPath("//input[@value='Redefinir Senha']");
        By deleteUserButton = By.XPath("//input[@value='Apagar Usuário']");
        By deleteAccountButton = By.XPath("//input[@value='Apagar Conta']");
        By removeUserButton = By.XPath("//input[@value='Remover Usuário']");
        private By RemoveProjectAssignedButtonBy(int projectId)
        {
            return By.XPath("//input[@name='project_id' and @value='" + projectId + "']/..//input[@value='Remover']");
        }
        By deleteUserInfoTextArea = By.CssSelector("p.bigger-110");
        By messageSucessTextArea = By.CssSelector("p.bold.bigger-110");
        By messageErrorTextArea = By.XPath("//div[@class='alert alert-danger']/p[2]");
        #endregion

        #region Actions
        public void PreencherUsername(string username)
        {
            ClearAndSendKeys(usernameField, username);
            //SendKeys(usernameField, username);
        }

        public void PreencherNomeUsuario(string realname)
        {
            ClearAndSendKeys(realnameField, realname);
            //SendKeys(realnameField, realname);
        }

        public void PreencherEmail(string email)
        {
            ClearAndSendKeys(emailField, email);
            //SendKeys(emailField, email);
        }

        public void ClicarAdicionarUsuario()
        {
            Click(addUserButton);
        }

        public void ClicarAtualizarUsuario()
        {
            Click(updateUserButton);
        }

        public void ClicarNomeUsuario(string username)
        {
            VerifyTextElement(userNameLink, username);
            Click(userNameLink);
        }

        public void ClicarNomeProjeto(string projectName)
        {
            Click(ProjectNameSelect(projectName));
        }

        public void ClicarRemoverProjetoAtribuido(int projectId)
        {
            Click(RemoveProjectAssignedButtonBy(projectId));
        }

        public void ClicarRedefinirSenha()
        {
            Click(redefinePasswordButton);
        }

        public void ClicarApagarUsuario()
        {
            Click(deleteUserButton);
        }

        public void ClicarApagarConta(string username)
        {
            VerifyTextElement(deleteUserInfoTextArea, username);
            Click(deleteAccountButton);
        }

        public void ClicarRemoverUsuario(string projectName)
        {
            VerifyTextElement(deleteUserInfoTextArea, projectName);
            Click(removeUserButton);
        }

        public string RetornaMensagemDeSucesso()
        {
            return GetText(messageSucessTextArea);
        }

        public string RetornaMensagemDeErro()
        {
            return GetText(messageErrorTextArea);
        }
        #endregion
    }
}