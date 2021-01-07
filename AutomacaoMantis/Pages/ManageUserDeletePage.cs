using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class ManageUserDeletePage : PageBase
    {
        #region Mapping
        By apagarContaButton = By.XPath("//input[@value='Apagar Conta']");
        By apagarUsuarioInfoTextArea = By.CssSelector("p.bigger-110");
        By messageSucessTextArea = By.XPath("//div[@id='main-container']/div[2]/div[2]/div/div/div/div[2]");
        #endregion

        #region Actions
        public void ClicarApagarConta(string username)
        {
            VerifyTextElement(apagarUsuarioInfoTextArea, username);
            Click(apagarContaButton);
        }

        public string RetornarMensagemDeSucesso()
        {
            return GetText(messageSucessTextArea);
        }
        #endregion
    }
}