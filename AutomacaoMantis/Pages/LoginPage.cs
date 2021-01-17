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
        public void PreencherUsuario(string usuario)
        {
            SendKeys(usernameField, usuario);  
        }

        public void PreencherSenha(string senha)
        {
            SendKeys(passwordField, senha);
        }

        public void ClicarEmLogin()
        {
            Click(loginButton);
        }

        public void ClicarCriarNovaConta()
        {
            Click(createNewAccountButton);
        }

        public string RetornarURLAtual()
        {
            return GetURL();
        }

        public string RetornarMensagemDeErro()
        {
            return GetText(messageErrorTextArea);
        }
        #endregion
    }
}