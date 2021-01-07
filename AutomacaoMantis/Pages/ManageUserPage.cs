using OpenQA.Selenium;
using AutomacaoMantis.Bases;
using AutomacaoMantis.Helpers;

namespace AutomacaoMantis.Pages
{
    public class ManageUserPage : PageBase
    {
        #region Mapping
        By criarNovaContaButton = By.XPath("//*[@id='manage-user-div']/div[1]/a");
        By aplicarFiltroButton = By.XPath("//input[@value='Aplicar Filtro']");
        By mostrarDesativadosCheckbox = By.XPath("//*[@id='manage-user-filter']/fieldset/label[2]/span");       
        By pesquisarUsuarioField = By.Id("search");
        By nomeUsuarioInfoTextArea = By.XPath("//*[@class='table-responsive']/table/tbody/tr/td[1]/a");
        #endregion

        #region Actions
        public void ClicarCriarNovaConta()
        {
            Click(criarNovaContaButton);
        }
        public void ClicarMostrarDesativados()
        {
            Click(mostrarDesativadosCheckbox);
        }
        public void ClicarAplicarFiltro()
        {
            Click(aplicarFiltroButton);
        }
        public void PesquisarUsuario(string username)
        {
            SendKeys(pesquisarUsuarioField, username);
        }        
        public string RetornarUsuarioExibidoResultadoPesquisa()
        {
            return GetText(nomeUsuarioInfoTextArea);
        }
        #endregion
    }
}