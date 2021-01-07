using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class ManageProjPage : PageBase
    {
        #region Mapping
        By categoryNameField = By.Name("name");

        By criarNovoProjetoButton = By.XPath("//button[@type='submit']");
        By adicionarCategoriaButton = By.XPath("//input[@value='Adicionar Categoria']");
        #endregion

        #region Actions
        public void PreencherCategoryName(string categoryName)
        {
            SendKeys(categoryNameField, categoryName);
        }

        public void ClicarCriarNovoProjeto()
        {
            Click(criarNovoProjetoButton);
        }

        public void ClicarAdicionarCategoria()
        {
            Click(adicionarCategoriaButton);
        }

        public void ClicarApagarCategoria(string categoryName)
        {
            Click(By.XPath("//*[text()='"+categoryName+"']//..//button[text()='Apagar']"));
        }

        public void ClicarEditarCategoria(string categoryName)
        {
            Click(By.XPath("//*[text()='" + categoryName + "']//..//button[text()='Alterar']"));
        }
        #endregion
    }
}