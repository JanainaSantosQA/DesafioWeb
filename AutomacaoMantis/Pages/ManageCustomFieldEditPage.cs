using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class ManageCustomFieldEditPage : PageBase
    {
        #region Mapping
        By nameCustomField = By.Id("custom-field-name");

        By atualizarCustomFieldButton = By.XPath("//input[@value='Atualizar Campo Personalizado']");
        By apagarCustomFieldButton = By.XPath("//input[@value='Apagar Campo Personalizado']");
        #endregion

        #region Actions
        public void PreencherCustomFieldName(string customFieldName)
        {
            SendKeys(nameCustomField, customFieldName);
        }
        public void LimparCustomFieldName()
        {
            Clear(nameCustomField);
        }
        public void ClicarAtualizarCustomField()
        {
            Click(atualizarCustomFieldButton);
        }
        public void ClicarApagarCustomField()
        {
            Click(apagarCustomFieldButton);
        }
        #endregion
    }
}