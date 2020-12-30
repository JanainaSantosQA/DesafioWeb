using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class ManageUserCreatePage : PageBase
    {
        #region Mapping
        By usernameField = By.Id("user-username");
        By realnameField = By.Id("user-realname");
        By emailField = By.Id("email-field");
        By accessLevelDropDown = By.Id("user-access-level");
        By criarUsuarioButton = By.XPath("//input[@class='btn btn-primary btn-white btn-round']");
        By messageErroTextArea = By.XPath("//*[@class='alert alert-danger']/p[2]");
        #endregion

        #region Actions
        public void PreencherUsername(string username)
        {
            SendKeys(usernameField, username);
        }
        public void PreencherRealname(string realname)
        {
            SendKeys(realnameField, realname);
        }
        public void PreencherEmail(string email)
        {
            SendKeys(emailField, email);
        }
        public void SelecionarNivelAcesso(string accessLevel)
        {
            ComboBoxSelectByVisibleText(accessLevelDropDown, accessLevel);
        }
        public void ClicarCriarUsuario()
        {
            Click(criarUsuarioButton);
        }
        public string RetornarMensagemDeErro()
        {
            return GetText(messageErroTextArea);
        }
        #endregion
    }
}