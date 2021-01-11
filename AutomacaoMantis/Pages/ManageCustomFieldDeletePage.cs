using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class ManageCustomFieldDeletePage : PageBase
    {
        #region Mapping
        By apagarCustomFieldButton = By.XPath("//input[@value='Apagar Campo']");

        By apagarCustomFieldInfoTextArea = By.CssSelector("p.bigger-110");
        By messageSucessTextArea = By.XPath("//div[@id='main-container']/div[2]/div[2]/div/div/div/div[2]");
        #endregion

        #region Actions
        public void ClicarApagarCustomField(string customFieldName)
        {
            VerifyTextElement(apagarCustomFieldInfoTextArea, customFieldName);
            Click(apagarCustomFieldButton);
        }

        public string RetornarMensagemDeSucesso()
        {
            return GetText(messageSucessTextArea);
        }
        #endregion
    }
}