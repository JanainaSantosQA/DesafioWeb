using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class ManageProjCatDeletePage : PageBase
    {
        #region Mapping
        By apagarCategoriaButton = By.XPath("//input[@value='Apagar Categoria']");

        By apagarCategoriaInfoTextArea = By.CssSelector("p.bigger-110");

        By messageSucessTextArea = By.XPath("//div[@id='main-container']/div[2]/div[2]/div/div/div/div[2]");
        #endregion

        #region Actions
        public void ClicarApagarCategoria(string categoryName)
        {
            VerifyTextElement(apagarCategoriaInfoTextArea, categoryName);
            Click(apagarCategoriaButton);
        }

        public string RetornarMensagemDeSucesso()
        {
            return GetText(messageSucessTextArea);
        }
        #endregion
    }
}