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
        By createUserButton = By.XPath("//input[@value='Criar Usuário']");
        By messageErrorTextArea = By.XPath("//*[@class='alert alert-danger']/p[2]");
        By messageSucessTextArea = By.XPath("//div[@id='main-container']/div[2]/div[2]/div/div/div/div[2]");
        #endregion

        #region Actions
        public void PreencherUsername(string username)
        {
            SendKeys(usernameField, username);
        }

        public void PreencherNomeUsuario(string realname)
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
            Click(createUserButton);
        }

        public string RetornarMensagemDeSucesso()
        {
            return GetText(messageSucessTextArea);
        }

        public string RetornarMensagemDeErro()
        {
            return GetText(messageErrorTextArea);
        }
        #endregion
    }
}