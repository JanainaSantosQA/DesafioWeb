using AutomacaoMantis.Pages;

namespace AutomacaoMantis.Flows
{
    public class ManageTagsFlows
    {
        #region Page Object and Constructor
        MyViewPage mainPage;
        ManageTagsPage manageTagsPage;

        public ManageTagsFlows()
        {
            mainPage = new MyViewPage();
            manageTagsPage = new ManageTagsPage();
        }
        #endregion

        public void AcessarMarcadorCriado(string menu, string tagName)
        {
            mainPage.ClicarMenu(menu);
            manageTagsPage.ClicarNomeMarcador(tagName);
        }
    }
}