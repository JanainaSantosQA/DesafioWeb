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
        private By RetornarLocalizadorAlterarCategoriaButton(string categoryName)
        {
            return By.XPath("//*[text()='" + categoryName + "']//..//button[text()='Alterar']");

        }
        private By RetornarLocalizadorApagarCategoriaButton(string categoryName)
        {
            return By.XPath("//*[text()='" + categoryName + "']//..//button[text()='Apagar']");

        }
        private By RetornarLocalizadorNomeProjetoLink(string projectName)
        {
            return By.XPath("//table[@class='table table-striped table-bordered table-condensed table-hover']//a[contains(text(),'" + projectName + "')]");

        }
        private By RetornarLocalizadorNomeCategoriaText(string categoryName)
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
            Click(RetornarLocalizadorApagarCategoriaButton(categoryName));
        }

        public void ClicarApagarCategoriaConfirmacao(string categoryName)
        {
            VerifyTextElement(deleteCategoryInfoTextArea, categoryName);
            Click(deleteCategoryConfirmationButton);
        }

        public void ClicarAlterarCategoria(string categoryName)
        {
            Click(RetornarLocalizadorAlterarCategoriaButton(categoryName));
        }

        public void ClicarNomeProjeto(string projectName)
        {
            Click(By.LinkText(projectName));
        }

        public bool RetornarSeONomeDoProjetoEstaSendoExibidoNaTela(string projectName)
        {
            return IsElementExists(RetornarLocalizadorNomeProjetoLink(projectName));
        }

        public bool RetornarSeONomeDaCategoriaEstaSendoExibidoNaTela(string categoryName)
        {
            return IsElementExists(RetornarLocalizadorNomeCategoriaText(categoryName));
        }

        public string RetornarMensagemDeSucesso()
        {
            return GetText(messageSucessTextArea);
        }

        public string RetornarMensagemDeErro()
        {
            return GetText(messageErrorTextArea);
        }
        #endregion
    }
}