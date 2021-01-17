using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class ManageProjCreatePage : PageBase
    {
        #region Mapping
        By projectNameField = By.Id("project-name");
        By descriptionField = By.Id("project-description");
        By statusDropDown = By.Id("project-status");
        By viewStateDropDown = By.Id("project-view-state");
        By addProjectButton = By.XPath("//input[@value='Adicionar projeto']");
        By messageSucessTextArea = By.CssSelector("p.bold.bigger-110");
        By messageErrorTextArea = By.XPath("//*[@class='alert alert-danger']/p[2]");
        #endregion

        #region Actions
        public void PreencherNomeProjeto(string projectName)
        {
            SendKeys(projectNameField, projectName);
        }

        public void SelecionarEstadoProjeto(string status)
        {
            ComboBoxSelectByVisibleText(statusDropDown, status);
        }

        public void SelecionarVisibilidade(string viewState)
        {
            ComboBoxSelectByVisibleText(viewStateDropDown, viewState);
        }

        public void PreencherDescricao(string description)
        {
            SendKeys(descriptionField, description);
        }

        public void ClicarAdicionarProjeto()
        {
            Click(addProjectButton);
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