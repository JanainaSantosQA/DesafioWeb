using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class AccountProfEditPage : PageBase
    {
        #region Mapping
        By platformField = By.Name("platform");
        By osField = By.Name("os");
        By osVersionField = By.Name("os_build");        
        By updateProfileButton = By.XPath("//input[@value='Atualizar Perfil']");
        #endregion

        #region Actions
        public void PreencherPlataforma(string platform)
        {
            ClearAndSendKeys(platformField, platform);
        }

        public void PreencherSO(string os)
        {
            ClearAndSendKeys(osField, os);
        }

        public void PreencherVersaoSO(string osVersion)
        {
            ClearAndSendKeys(osVersionField, osVersion);
        }
         
        public void ClicarAtualizarPerfil()
        {
            Click(updateProfileButton);
        }
        #endregion
    }
}