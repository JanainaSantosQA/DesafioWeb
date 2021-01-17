using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class TagViewPage : PageBase
    {
        #region Mapping
        By deleteTagButton = By.XPath("//input[@value='Apagar Marcador']");
        By updateTagButton = By.XPath("//input[@value='Atualizar Marcador']");
        #endregion

        #region Actions
        public void ClicarAtualizarMarcador()
        {
            Click(updateTagButton);
        }

        public void ClicarApagarMarcador()
        {
            Click(deleteTagButton);
        }

        public void ClicarApagarMarcadorConfirmacao()
        {
            Click(deleteTagButton);
        }
        #endregion
    }
}