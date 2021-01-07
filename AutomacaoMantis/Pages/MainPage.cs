using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class MainPage : PageBase
    {
        #region Mapping
        By usernameLoginInfoTextArea = By.CssSelector("span.user-info");
        By gerenciarMenu = By.XPath("//span[text()=' Gerenciar ']");
        By gerenciarUsuariosLink = By.LinkText("Gerenciar Usuários");
        By gerenciarProjetosLink = By.LinkText("Gerenciar Projetos");
        By gerenciarMarcadoresLink = By.LinkText("Gerenciar Marcadores");
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

                case "menuGerenciarProjetos":
                    Click(gerenciarMenu);
                    Click(gerenciarProjetosLink);
                    break;

                case "menuGerenciarMarcadores":
                    Click(gerenciarMenu);
                    Click(gerenciarMarcadoresLink);
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