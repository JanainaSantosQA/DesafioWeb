using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class LoginPage : PageBase
    {
        #region Mapping
        By usernameField = By.Id("username");
        By passwordField = By.Id("password");
        By loginButton = By.XPath("//input[@value='Entrar']");
        By createNewAccountButton = By.LinkText("criar uma nova conta");
        By messageErrorTextArea = By.XPath("//div[@class='alert alert-danger']/p");
        #endregion

        #region Actions
        public void PreencherUsuario(string username)
        {
            SendKeys(usernameField, username);  
        }

        public void PreencherUsuarioComJavaScript(string username)
        {
            SendKeysJavaScript(usernameField, username);
        }

        public void PreencherSenha(string password)
        {
            SendKeys(passwordField, password);
        }

        public void PreencherSenhaComJavaScript(string password)
        {
            SendKeysJavaScript(passwordField, password);
        }

        public void ClicarEmLogin()
        {
            Click(loginButton);
        }

        public void ClicarEmLoginComJavaScript()
        {
            ClickJavaScript(loginButton);
        }

        public void ClicarCriarNovaConta()
        {
            Click(createNewAccountButton);
        }

        public string RetornaMensagemDeErro()
        {
            return GetText(messageErrorTextArea);
        }
        #endregion
    }
}