using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class MainPage : PageBase
    {
        #region Mapping
        By usernameLoginInfoTextArea = By.CssSelector("span.user-info");
        By gerenciarMenu = By.XPath("//div[@id='sidebar']/ul/li[6]/a/i");        
        By gerenciarUsuariosLink = By.LinkText("Gerenciar Usuários");
        #endregion

        #region Actions
        public void ClicarMenu(string menu)
        {
            switch (menu)
            {
                case "menuGerenciarUsuarios":
                    Click(gerenciarMenu);
                    Click(gerenciarUsuariosLink);
                    break;
            }
        }
        public string RetornarUsernameDasInformacoesDeLogin()
        {
            return GetText(usernameLoginInfoTextArea);
        }
        #endregion
    }
}