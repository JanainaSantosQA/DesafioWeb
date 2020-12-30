using OpenQA.Selenium;
using AutomacaoMantis.Bases;
using AutomacaoMantis.Helpers;

namespace AutomacaoMantis.Pages
{
    public class ManageUserPage : PageBase
    {
        #region Mapping
        By criarNovaContaButton = By.XPath("//*[@id='manage-user-div']/div[1]/a");
        By mostrarDesativadosCheckbox = By.XPath("//*[@id='manage-user-filter']/fieldset/label[2]/span");
        By aplicarFiltroButton = By.XPath("//*[@id='manage-user-filter']/fieldset/input[7]");
        By pesquisarUsuarioField = By.Id("search");
        By nomeUsuarioInfoTextArea = By.XPath("//*[@class='table-responsive']/table/tbody/tr/td[1]/a");
        #endregion

        #region Actions
        public void AbrirManageUserPage()
        {
            NavigateTo(BuilderJson.ReturnParameterAppSettings("DEFAULT_APPLICATION_URL") + "manage_user_page.php");           
        }
        public void ClicarCriarNovaConta()
        {
            Click(criarNovaContaButton);
        }
        public void ClicarMostrarDesativados()
        {
            Click(mostrarDesativadosCheckbox);
        }
        public void PesquisarUsuario(string username)
        {
            SendKeys(pesquisarUsuarioField, username);
        }
        public void ClicarAplicarFiltro()
        {
            Click(aplicarFiltroButton);
        }
        public string RetornarUsuarioExibidoResultadoPesquisa()
        {
            return GetText(nomeUsuarioInfoTextArea);
        }
        #endregion
    }
}