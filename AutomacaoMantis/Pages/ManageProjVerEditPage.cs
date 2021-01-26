using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class ManageProjVerEditPage : PageBase
    {
        #region Mapping
        By versionNameField = By.Id("proj-version-new-version");
        By updateVersionButton = By.XPath("//input[@value='Atualizar Versão']");
        By messageSucessTextArea = By.CssSelector("p.bold.bigger-110");
        By messageErrorTextArea = By.XPath("//*[@class='alert alert-danger']/p[2]");
        #endregion

        #region Actions
        public void PreencherNomeVersao(string versionName)
        {
            Clear(versionNameField);
            SendKeys(versionNameField, versionName);
        }

        public void ClicarAtualizarVersao()
        {
            Click(updateVersionButton);
        }

        public string RetornaMensagemDeSucesso()
        {
            return GetText(messageSucessTextArea);
        }

        public string RetornaMensagemDeErro()
        {
            return GetText(messageErrorTextArea);
        }
        #endregion
    }
}