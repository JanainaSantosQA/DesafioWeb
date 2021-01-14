using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class ManageProjEditPage : PageBase
    {
        #region Mapping
        By subprojetoDropDown = By.Name("subproject_id");

        By versionNameField = By.Name("version");

        By adicionarVersaoButton = By.Name("add_version");
        public By AlterarVersaoButton(string versionName)
        {
            return By.XPath($"//*[text()='{versionName}']//..//button[text()='Alterar']");
        }        
        public By ApagarVersaoButton(string versionName)
        {
            return By.XPath($"//*[text()='{versionName}']//..//button[text()='Apagar']");
        }
        By apagarVersaoConfirmacaoButton = By.XPath("//input[@value='Apagar Versão']");

        By adicionarComoSubprojetoButton = By.XPath("//input[@value='Adicionar como Subprojeto']");

        By subprojetosExistentesTable = By.XPath("//*[@id='manage-project-update-subprojects-form']//table/tbody");

        By apagarVersaoInfoTextArea = By.CssSelector("p.bigger-110");
        By messageSucessTextArea = By.CssSelector("p.bold.bigger-110");
        By messageErroTextArea = By.XPath("//*[@class='alert alert-danger']/p[2]");
        #endregion

        #region Actions
        public void SelecionarProjectName(string projectName)
        {
            ComboBoxSelectByVisibleText(subprojetoDropDown, projectName);
        }

        public void ClicarAdicionarComoSubprojeto()
        {
            Click(adicionarComoSubprojetoButton);
        }

        public void ClicarAdicionarVersao()
        {
            Click(adicionarVersaoButton);
        }

        public void ClicarAlterarVersao(string versionName)
        {
            Click(AlterarVersaoButton(versionName));
        }

        public void ClicarApagarVersao(string versionName)
        {
            Click(ApagarVersaoButton(versionName));
        }

        public void ClicarApagarVersaoConfirmacao(string versionName)
        {
            VerifyTextElement(apagarVersaoInfoTextArea, versionName);
            Click(apagarVersaoConfirmacaoButton);
        }

        public void VerificarSeOSubprojetoEstaSendoExibidoNaTela(string projectName)
        {
            VerifyTextElement(subprojetosExistentesTable, projectName);
        }

        public void ClicarDesvincular(int childId, int parentId)
        {
            Click(By.XPath("//a[@href[contains(.,'manage_proj_subproj_delete.php?project_id=" + parentId + "&subproject_id=" + childId + "')] and text()='Desvincular']"));
        }

        public void PreencherVersionName(string versionName)
        {            
            SendKeys(versionNameField, versionName);
        }

        public string RetornarMensagemDeSucesso()
        {
            return GetText(messageSucessTextArea);
        }

        public string RetornarMensagemDeErro()
        {
            return GetText(messageErroTextArea);
        }
        #endregion
    }
}