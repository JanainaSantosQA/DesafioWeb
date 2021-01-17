using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class ManageCustomFieldPage : PageBase
    {
        #region Mapping
        By nameCustomField = By.Name("name");
        By newCustomFieldButton = By.XPath("//input[@value='Novo Campo Personalizado']");
        private By RetornarLocalizadorNomeCampoPersonalizadoLink(string customFieldName)
        {
            return By.XPath("//a[text()='" + customFieldName + "']");

        }
        By messageErrorTextArea = By.XPath("//*[@class='alert alert-danger']/p[2]");
        By messageSucessTextArea = By.CssSelector("p.bold.bigger-110");
        #endregion

        #region Actions
        public void PreencherNomeCampoPersonalizado(string customFieldName)
        {
            SendKeys(nameCustomField, customFieldName);
        }

        public void ClicarNovoCampoPersonalizado()
        {
            Click(newCustomFieldButton);
        }

        public void ClicarCampoPersonalizadoLink(string customFieldName)
        {
            Click(RetornarLocalizadorNomeCampoPersonalizadoLink(customFieldName));
        }

        public string RetornarMensagemDeErro()
        {
            return GetText(messageErrorTextArea);
        }

        public string RetornarMensagemDeSucesso()
        {
            return GetText(messageSucessTextArea);
        }
        #endregion
    }
}