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

        By adicionarProjetoButton = By.XPath("//input[@value='Adicionar projeto']");

        By messageSucessTextArea = By.CssSelector("p.bold.bigger-110");
        By messageErroTextArea = By.XPath("//*[@class='alert alert-danger']/p[2]");
        #endregion

        #region Actions
        public void PreencherProjectName(string projectName)
        {
            SendKeys(projectNameField, projectName);
        }

        public void SelecionarStatus(string status)
        {
            ComboBoxSelectByVisibleText(statusDropDown, status);
        }

        public void SelecionarViewState(string viewState)
        {
            ComboBoxSelectByVisibleText(viewStateDropDown, viewState);
        }

        public void PreencherDescription(string description)
        {
            SendKeys(descriptionField, description);
        }

        public void ClicarAdicionarProjeto()
        {
            Click(adicionarProjetoButton);
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