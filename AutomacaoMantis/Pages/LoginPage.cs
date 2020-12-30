using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class LoginPage : PageBase
    {
        #region Mapping
        By usernameField = By.Id("username");
        By passwordField = By.Id("password");
        By loginButton = By.XPath("//input[@type='submit']");
        By messageErroTextArea = By.XPath("//div[@class='alert alert-danger']/p");
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
        public string RetornarMensagemDeErro()
        {
            return GetText(messageErroTextArea);
        }
        #endregion
    }
}