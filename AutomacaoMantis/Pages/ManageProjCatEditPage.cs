using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class ManageProjCatEditPage : PageBase
    {
        #region Mapping
        By categoryNameField = By.Id("proj-category-name");
        By updateCategoryButton = By.XPath("//input[@value='Atualizar Categoria']");
        By messageSucessTextArea = By.CssSelector("p.bold.bigger-110");
        By messageErrorTextArea = By.XPath("//*[@class='alert alert-danger']/p[2]");
        #endregion

        #region Actions
        public void PreencherNomeCategoria(string categoryName)
        {
            ClearAndSendKeys(categoryNameField, categoryName);
        }

        public void LimparNomeCategoria()
        {
            Clear(categoryNameField);
        }

        public void ClicarAtualizarCategoria()
        {
            Click(updateCategoryButton);
        }

        public string RetornaMensagemDeSucesso()
        {
            return GetText(messageSucessTextArea);
        }

        public string RetornaMensagemDeErro()
        {
            return GetText(messageErrorTextArea);
        }
        #endregion

    }
}