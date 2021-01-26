using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class ManageProjPage : PageBase
    {
        #region Mapping
        By categoryNameField = By.Name("name");
        By createNewProjectButton = By.XPath("//button[@type='submit']");
        By addCategoryButton = By.XPath("//input[@value='Adicionar Categoria']");
        By deleteCategoryConfirmationButton = By.XPath("//input[@value='Apagar Categoria']");
        private By AlterCategoryButtonBy(string categoryName)
        {
            return By.XPath("//*[text()='" + categoryName + "']//..//button[text()='Alterar']");

        }
        private By DeleteCategoryButtonBy(string categoryName)
        {
            return By.XPath("//*[text()='" + categoryName + "']//..//button[text()='Apagar']");

        }
        private By ProjectNameLinkBy(string projectName)
        {
            return By.XPath("//table[@class='table table-striped table-bordered table-condensed table-hover']//a[contains(text(),'" + projectName + "')]");

        }
        private By CategoryNameTextBy(string categoryName)
        {
            return By.XPath("//h4[contains(., 'Categorias Globais')]/../../*[contains(.,'" + categoryName + "')]");

        }
        By deleteCategoryInfoTextArea = By.CssSelector("p.bigger-110");
        By messageSucessTextArea = By.CssSelector("p.bold.bigger-110");
        By messageErrorTextArea = By.XPath("//*[@class='alert alert-danger']/p[2]");
        #endregion

        #region Actions
        public void PreencherNomeCategoria(string categoryName)
        {
            SendKeys(categoryNameField, categoryName);
        }

        public void ClicarCriarNovoProjeto()
        {
            Click(createNewProjectButton);
        }

        public void ClicarAdicionarCategoria()
        {
            Click(addCategoryButton);
        }

        public void ClicarApagarCategoria(string categoryName)
        {
            Click(DeleteCategoryButtonBy(categoryName));
        }

        public void ClicarApagarCategoriaConfirmacao(string categoryName)
        {
            VerifyTextElement(deleteCategoryInfoTextArea, categoryName);
            Click(deleteCategoryConfirmationButton);
        }

        public void ClicarAlterarCategoria(string categoryName)
        {
            Click(AlterCategoryButtonBy(categoryName));
        }

        public void ClicarNomeProjeto(string projectName)
        {
            Click(By.LinkText(projectName));
        }

        public bool RetornaSeONomeDoProjetoEstaSendoExibidoNaTela(string projectName)
        {
            return IsElementExists(ProjectNameLinkBy(projectName));
        }

        public bool RetornaSeONomeDaCategoriaEstaSendoExibidoNaTela(string categoryName)
        {
            return IsElementExists(CategoryNameTextBy(categoryName));
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