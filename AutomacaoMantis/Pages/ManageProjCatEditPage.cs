using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class ManageProjCatEditPage : PageBase
    {
        #region Mapping
        By categoryNameField = By.Id("proj-category-name");

        By atualizarCategoriaButton = By.XPath("//input[@value='Atualizar Categoria']");

        By messageSucessTextArea = By.XPath("//div[@id='main-container']/div[2]/div[2]/div/div/div/div[2]");
        #endregion

        #region Actions
        public void PreencherCategoryName(string categoryName)
        {
            SendKeys(categoryNameField, categoryName);
        }

        public void LimparCategoryName()
        {
            Clear(categoryNameField);
        }

        public void ClicarAtualizarCategoria()
        {
            Click(atualizarCategoriaButton);
        }

        public string RetornarMensagemDeSucesso()
        {
            return GetText(messageSucessTextArea);
        }
        #endregion

    }
}