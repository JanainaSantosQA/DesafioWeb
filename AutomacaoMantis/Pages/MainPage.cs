using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class MainPage : PageBase
    {
        #region Mapping
        By usernameLoginInfoTextArea = By.XPath("//span[@class='user-info']");
        #endregion

        #region Actions
        public string RetornarUsernameDasInformacoesDeLogin()
        {
            return GetText(usernameLoginInfoTextArea);
        }
        #endregion
    }
}