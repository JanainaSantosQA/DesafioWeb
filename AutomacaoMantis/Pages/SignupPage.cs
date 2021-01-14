using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class SignupPage : PageBase
    {
        #region Mapping
        By usernameField = By.Id("username");
        By emailField = By.Id("email-field");
        By captchaField = By.Id("captcha-field");
        By criarContaButton = By.XPath("//input[@value='Criar Conta']");
        By gerarNovoCodigoLink = By.XPath("//ul[@id='captcha-refresh']/li/a");
        By messageErroTextArea = By.XPath("//div[@class='alert alert-danger']/p[2]");
        By captchaImg = By.XPath("//span/img");
        #endregion

        #region Actions
        public void PreencherUsername(string username)
        {
            SendKeys(usernameField, username);
        }
        public void PreencherEmail(string email)
        {
            SendKeys(emailField, email);
        }
        public void PreencherCaptcha(string captcha)
        {
            SendKeys(captchaField, captcha);
        }
        public string RetornarSRCImagem()
        {
            return GetSRC(captchaImg);
        }
        public void GerarNovoCaptcha()
        {
            Click(gerarNovoCodigoLink);
        }
        public void ClicarCriarConta()
        {
            Click(criarContaButton);
        }
        public string RetornarMensagemDeErro()
        {
            return GetText(messageErroTextArea);
        }
        #endregion
    }
}