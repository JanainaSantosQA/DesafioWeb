using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class ManageCustomFieldCreatePage : PageBase
    {
        #region Mapping
        By messageErroTextArea = By.XPath("//*[@class='alert alert-danger']/p[2]");
        By messageSucessTextArea = By.XPath("//div[@id='main-container']/div[2]/div[2]/div/div/div/div[2]");
        #endregion

        #region Actions
        public string RetornarMensagemDeSucesso()
        {
            return GetText(messageSucessTextArea);
        }
        public string RetornarMensagemDeErro()
        {
            return GetText(messageErroTextArea);
        }
        #endregion
    }
}