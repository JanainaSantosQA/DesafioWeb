using AutomacaoMantis.Pages;

namespace AutomacaoMantis.Flows
{
    public class ManageProjFlows
    {
        #region Page Object and Constructor
        MainPage mainPage;
        ManageProjPage manageProjPage;
        ManageProjCreatePage manageProjCreatePage;

        public ManageProjFlows()
        {
            mainPage = new MainPage();
            manageProjPage = new ManageProjPage();
            manageProjCreatePage = new ManageProjCreatePage();
        }
        #endregion

        public void CriarProjetoComSucesso(string menu, string projectName, string status, string viewState, string description)
        {

            mainPage.ClicarMenu(menu);
            manageProjPage.ClicarCriarNovoProjeto();
            manageProjCreatePage.PreencherProjectName(projectName);
            manageProjCreatePage.SelecionarStatus(status);
            manageProjCreatePage.SelecionarViewState(viewState);
            manageProjCreatePage.PreencherDescription(description);
            manageProjCreatePage.ClicarAdicionarProjeto();
        }
    }
}