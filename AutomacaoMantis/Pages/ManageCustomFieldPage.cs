using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class ManageCustomFieldPage : PageBase
    {
        #region Mapping
        By nameCustomField = By.Name("name");

        By newCustomFieldButton = By.XPath("//input[@value='Novo Campo Personalizado']");

        By messageSucessTextArea = By.XPath("//div[@id='main-container']/div[2]/div[2]/div/div/div/div[2]");
        #endregion

        #region Actions
        public void PreencherCustomField(string customFieldName)
        {
            SendKeys(nameCustomField, customFieldName);
        }

        public void ClicarNewCustomField()
        {
            Click(newCustomFieldButton);
        }

        public void ClicarCustomFieldLink(string customFieldName)
        {
            Click(By.XPath("//a[text()='" + customFieldName + "']"));
        }

        public string RetornarMensagemDeSucesso()
        {
            return GetText(messageSucessTextArea);
        }
        #endregion
    }
}