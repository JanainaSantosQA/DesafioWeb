using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class TagViewPage : PageBase
    {
        #region Mapping
        By apagarMarcadorButton = By.XPath("//input[@value='Apagar Marcador']");
        By atualizarMarcadorButton = By.XPath("//input[@value='Atualizar Marcador']");
        #endregion

        #region Actions
        public void ClicarAtualizarMarcador()
        {
            Click(atualizarMarcadorButton);
        }
        public void ClicarApagarMarcador()
        {
            Click(apagarMarcadorButton);
        }
        #endregion
    }
}