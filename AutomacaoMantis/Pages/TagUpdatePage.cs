using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class TagUpdatePage : PageBase
    {
        #region Mapping
        By tagNameField = By.Id("tag-name");

        By atualizarMarcadorButton = By.XPath("//input[@value='Atualizar Marcador']");

        By messageErroTextArea = By.XPath("//*[@class='alert alert-danger']/p[2]");
        #endregion

        #region Actions
        public void PreencherNomeMarcador(string tagName)
        {
            SendKeys(tagNameField, tagName);
        }
        public void LimparTagName()
        {
            Clear(tagNameField);
        }
        public void ClicarAtualizarMarcador()
        {
            Click(atualizarMarcadorButton);
        }
        public string RetornarMensagemDeErro()
        {
            return GetText(messageErroTextArea);
        }
        #endregion
    }
}