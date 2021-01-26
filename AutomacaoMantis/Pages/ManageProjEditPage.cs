using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class ManageProjEditPage : PageBase
    {
        #region Mapping
        By subProjetoDropDown = By.Name("subproject_id");
        By projectNameField = By.Id("project-name");
        By versionNameField = By.Name("version");
        By updateProjectButton = By.XPath("//input[@value='Atualizar Projeto']");
        By addVersionButton = By.Name("add_version");
        private By AlterVersionButtonBy(string versionName)
        {
            return By.XPath("//*[text()='" + versionName + "']//..//button[text()='Alterar']");
        }
        private By DeleteVersionButtonBy(string versionName)
        {
            return By.XPath("//*[text()='" + versionName + "']//..//button[text()='Apagar']");
        }
        By deleteVersionConfirmationButton = By.XPath("//input[@value='Apagar Versão']");
        By addSubProjectButton = By.XPath("//input[@value='Adicionar como Subprojeto']");
        private By SubProjetoLinkBy(string projectName)
        {
            return By.XPath("//*[@id='manage-project-update-subprojects-form']//a[contains(., '" + projectName + "')]");
        }
        By deleteVersionInfoTextArea = By.CssSelector("p.bigger-110");
        By messageSucessTextArea = By.CssSelector("p.bold.bigger-110");
        By messageErrorTextArea = By.XPath("//*[@class='alert alert-danger']/p[2]");
        #endregion

        #region Actions
        public void SelecionarNomeProjeto(string projectName)
        {
            ComboBoxSelectByVisibleText(subProjetoDropDown, projectName);
        }

        public void ClicarAtualizarProjeto()
        {
            Click(updateProjectButton);
        }

        public void ClicarAdicionarComoSubProjeto()
        {
            Click(addSubProjectButton);
        }

        public void ClicarAdicionarVersao()
        {
            Click(addVersionButton);
        }

        public void ClicarAlterarVersao(string versionName)
        {
            Click(AlterVersionButtonBy(versionName));
        }

        public void ClicarApagarVersao(string versionName)
        {
            Click(DeleteVersionButtonBy(versionName));
        }

        public void ClicarApagarVersaoConfirmacao(string versionName)
        {
            VerifyTextElement(deleteVersionInfoTextArea, versionName);
            Click(deleteVersionConfirmationButton);
        }

        public bool RetornaSeOSubProjetoEstaSendoExibidoNaTela(string projectName)
        {
            return IsElementExists(SubProjetoLinkBy(projectName));
        }

        public void ClicarDesvincular(int childId, int parentId)
        {
            Click(By.XPath("//a[@href[contains(.,'manage_proj_subproj_delete.php?project_id=" + parentId + "&subproject_id=" + childId + "')] and text()='Desvincular']"));
        }

        public void PreencherNomeProjeto(string projectName)
        {
            ClearAndSendKeys(projectNameField, projectName);
        }

        public void PreencherNomeVersao(string versionName)
        {
            SendKeys(versionNameField, versionName);
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