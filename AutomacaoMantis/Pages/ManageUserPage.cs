using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class ManageUserPage : PageBase
    {
        #region Mapping
        By searchUserField = By.Id("search");
        By createNewAccountButton = By.XPath("//*[@id='manage-user-div']/div[1]/a");
        By ApplyFilterButton = By.XPath("//input[@value='Aplicar Filtro']");
        By showDisabledCheckbox = By.XPath("//*[@id='manage-user-filter']/fieldset/label[2]/span");     
        By nameUserInfoTextArea = By.XPath("//*[@class='table-responsive']/table/tbody/tr/td[1]/a");
       #endregion

        #region Actions
        public void ClicarCriarNovaConta()
        {
            Click(createNewAccountButton);
        }

        public void ClicarMostrarUsuariosDesativados()
        {
            Click(showDisabledCheckbox);
        }

        public void ClicarAplicarFiltro()
        {
            Click(ApplyFilterButton);
        }

        public void PesquisarUsuario(string username)
        {
            SendKeys(searchUserField, username);
        }        

        public string RetornarUsuarioExibidoResultadoPesquisa()
        {
            return GetText(nameUserInfoTextArea);
        }
        #endregion
    }
}