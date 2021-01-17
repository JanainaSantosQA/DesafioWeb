using AutomacaoMantis.Pages;

namespace AutomacaoMantis.Flows
{
    public class ManageProjFlows
    {
        #region Page Object and Constructor
        MyViewPage mainPage;
        ManageProjPage manageProjPage;

        public ManageProjFlows()
        {
            mainPage = new MyViewPage();
            manageProjPage = new ManageProjPage();
        }
        #endregion

        public void AcessarProjetoCriado(string menu, string projectName)
        {
            mainPage.ClicarMenu(menu);
            manageProjPage.ClicarNomeProjeto(projectName);
        }
    }
}