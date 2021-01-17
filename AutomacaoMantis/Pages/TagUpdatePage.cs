using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class TagUpdatePage : PageBase
    {
        #region Mapping
        By tagNameField = By.Id("tag-name");
        By updateTagButton = By.XPath("//input[@value='Atualizar Marcador']");
        By messageErrorTextArea = By.XPath("//*[@class='alert alert-danger']/p[2]");
        #endregion

        #region Actions
        public void PreencherNomeMarcador(string tagName)
        {
            ClearAndSendKeys(tagNameField, tagName);
        }

        public void ClicarAtualizarMarcador()
        {
            Click(updateTagButton);
        }

        public string RetornarMensagemDeErro()
        {
            return GetText(messageErrorTextArea);
        }
        #endregion
    }
}