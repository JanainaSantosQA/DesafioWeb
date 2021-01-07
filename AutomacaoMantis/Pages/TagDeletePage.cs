using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class TagDeletePage : PageBase
    {
        #region Mapping
        By apagarMarcadorButton = By.XPath("//input[@value='Apagar Marcador']");
        #endregion

        #region Actions
        public void ClicarApagarMarcador()
        {
            Click(apagarMarcadorButton);
        }
        #endregion
    }
}