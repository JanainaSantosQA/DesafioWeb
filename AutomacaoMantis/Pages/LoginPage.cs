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
        //By mensagemErroTextArea = By.XPath("/html/body/div[2]/font"); //exemplo de mapping incorreto
        #endregion

        #region Actions
        public void PreencherUsuario(string usuario)
        {
            SendKeys(usernameField, usuario);
            Click(loginButton);
        }

        public void PreencherSenha(string senha)
        {
            SendKeys(passwordField, senha);
        }

        public void ClicarEmLogin()
        {
            Click(loginButton);
        }

        //public string RetornaMensagemDeErro()
        //{
        //    return GetText(mensagemErroTextArea);
        //}
        #endregion
    }
}