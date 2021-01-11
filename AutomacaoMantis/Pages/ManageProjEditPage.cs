using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class ManageProjEditPage : PageBase
    {
        #region Mapping
        By subprojetoDropDown = By.Name("subproject_id");

        By adicionarComoSubprojetoButton = By.XPath("//input[@value='Adicionar como Subprojeto']");

        By subprojetosExistentesTable = By.XPath("//*[@id='manage-project-update-subprojects-form']//table/tbody");
       
        By messageSucessTextArea = By.CssSelector("p.bold.bigger-110");
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

        public void VerificarSeOSubprojetoEstaSendoExibidoNaTela(string projectName)
        {
            VerifyTextElement(subprojetosExistentesTable, projectName);
        }

        public string RetornarMensagemDeSucesso()
        {
            return GetText(messageSucessTextArea);
        }
        #endregion
    }
}