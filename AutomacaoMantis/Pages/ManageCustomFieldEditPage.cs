using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class ManageCustomFieldEditPage : PageBase
    {
        #region Mapping
        By nameCustomField = By.Id("custom-field-name");
        By updateCustomFieldButton = By.XPath("//input[@value='Atualizar Campo Personalizado']");
        By deleteCustomFieldButton = By.XPath("//input[@value='Apagar Campo Personalizado']");
        By deleteFieldButton = By.XPath("//input[@value='Apagar Campo']");
        By deleteFieldInfoTextArea = By.CssSelector("p.bigger-110");
        #endregion

        #region Actions
        public void PreencherNomeCampoPersonalizado(string customFieldName)
        {
            ClearAndSendKeys(nameCustomField, customFieldName);
        }

        public void ClicarAtualizarCampoPersonalizado()
        {
            Click(updateCustomFieldButton);
        }

        public void ClicarApagarCampoPersonalizado()
        {
            Click(deleteCustomFieldButton);
        }

        public void ClicarConfirmacaoApagarCampo(string customFieldName)
        {
            VerifyTextElement(deleteFieldInfoTextArea, customFieldName);
            Click(deleteFieldButton);
        }
        #endregion
    }
}