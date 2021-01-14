using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class ManageProjVerEditPage : PageBase
    {
        #region Mapping
        By versionNameField = By.Id("proj-version-new-version");

        By atualizarVersaoButton = By.XPath("//input[@value='Atualizar Versão']");

        By messageSucessTextArea = By.CssSelector("p.bold.bigger-110");
        By messageErroTextArea = By.XPath("//*[@class='alert alert-danger']/p[2]");
        #endregion

        #region Actions
        public void PreencherVersionName(string versionName)
        {
            Clear(versionNameField);
            SendKeys(versionNameField, versionName);
        }

        public void ClicarAtualizarVersao()
        {
            Click(atualizarVersaoButton);
        }

        public string RetornarMensagemDeSucesso()
        {
            return GetText(messageSucessTextArea);
        }

        public string RetornarMensagemDeErro()
        {
            return GetText(messageErroTextArea);
        }
        #endregion
    }
}