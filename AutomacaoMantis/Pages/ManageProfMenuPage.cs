using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class ManageProfMenuPage : PageBase
    {
        #region Mapping
        By platformField = By.Id("platform");
        By osField = By.Id("os");
        By osVersionField = By.Id("os-version");
        By addProfileButton = By.XPath("//input[@value='Adicionar Perfil']");
        By deleteProfileRadioButton = By.XPath("//input[@id='action-delete']/../span");
        By editProfileRadioButton = By.XPath("//input[@id='action-edit']/../span");
        By submitButton = By.XPath("//input[@value='Enviar']");
        By profileSelect = By.Id("select-profile");
        private By RetornarLocalizadorPerfilSelecao(string profile)
        {
            return By.XPath("//select[@id='select-profile']/option[text()='" + profile + "']");
        }
        By messageErrorTextArea = By.XPath("//*[@class='alert alert-danger']/p[2]");
        #endregion

        #region Actions
        public void PreencherPlataforma(string platform)
        {
            SendKeys(platformField, platform);
        }

        public void PreencherSO(string os)
        {
            SendKeys(osField, os);
        }

        public void PreencherVersaoSO(string osVersion)
        {
            SendKeys(osVersionField, osVersion);
        }

        public void ClicarAdicionarPerfil()
        {
            Click(addProfileButton);
        }

        public void ClicarApagarPerfil()
        {
            Click(deleteProfileRadioButton);
        }

        public void ClicarEditarPerfil()
        {
            Click(editProfileRadioButton);
        }

        public void ClicarEnviar()
        {
            Click(submitButton);
        }

        public void SelecionarPerfil(string profile)
        {
            ComboBoxSelectByVisibleText(profileSelect, profile);
        }

        public bool RetornarSeOPerfilEstaSendoExibidoNaTela(string profile)
        {
            return IsElementExists(RetornarLocalizadorPerfilSelecao(profile));
        }

        public string RetornarMensagemDeErro()
        {
            return GetText(messageErrorTextArea);
        }
        #endregion
    }
}