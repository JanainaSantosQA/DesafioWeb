using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class ManageUserUpdatePage : PageBase
    {
        #region Mapping
        By messageErroTextArea = By.XPath("//div[@class='alert alert-danger']/p[2]");
        #endregion

        #region Actions
        public string RetornarMensagemDeErro()
        {
            return GetText(messageErroTextArea);
        }
        #endregion
    }
}