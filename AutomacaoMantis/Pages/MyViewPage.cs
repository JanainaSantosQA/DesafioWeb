using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class MyViewPage : PageBase
    {
        #region Mapping
        By usernameLoginInfoTextArea = By.CssSelector("span.user-info");
        By manageMenu = By.XPath("//span[text()=' Gerenciar ']");
        By manageUsersLink = By.LinkText("Gerenciar Usuários");
        By manageProjectsLink = By.LinkText("Gerenciar Projetos");
        By manageTagsLink = By.LinkText("Gerenciar Marcadores");
        By manageCustomFieldLink = By.LinkText("Gerenciar Campos Personalizados");
        By manageProfMenuLink = By.LinkText("Gerenciar Perfís Globais");
        By signOutLink = By.XPath("//a[contains(.,'Sair')]");
        #endregion

        #region Actions
        public void ClicarMenu(string menu)
        {
            switch (menu)
            {
                case "menuGerenciarUsuarios":
                    Click(manageMenu);
                    Click(manageUsersLink);
                    break;

                case "menuGerenciarProjetos":
                    Click(manageMenu);
                    Click(manageProjectsLink);
                    break;

                case "menuGerenciarMarcadores":
                    Click(manageMenu);
                    Click(manageTagsLink);
                    break;

                case "menuGerenciarCamposPersonalizados":
                    Click(manageMenu);
                    Click(manageCustomFieldLink);
                    break;

                case "menuGerenciarPerfisGlobais":
                    Click(manageMenu);
                    Click(manageProfMenuLink);
                    break;
            }
        }

        public void ClicarUsuarioInfo()
        {
            Click(usernameLoginInfoTextArea);
        }

        public void ClicarSair()
        {
            Click(signOutLink);
        }

        public string RetornarUsernameDasInformacoesDeLogin()
        {
            return GetText(usernameLoginInfoTextArea);
        }
        #endregion
    }
}