using NUnit.Framework;
using AutomacaoMantis.Pages;
using AutomacaoMantis.Bases;
using AutomacaoMantis.Helpers;

namespace AutomacaoMantis.Tests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        #region Pages and Flows Objects
        LoginPage loginPage;
        MainPage mainPage;
        SignupPage signupPage;
        #endregion

        [SetUp]
        public void Setup()
        {
            loginPage = new LoginPage();
            mainPage = new MainPage();
            signupPage = new SignupPage();
        }

        [Test]
        public void RealizarLoginComSucesso()
        {

            #region Parameters
            string username = "administrator";
            string password = "administrator";
            #endregion

            loginPage.PreencherUsuario(username);
            loginPage.ClicarEmLogin();
            loginPage.PreencherSenha(password);
            loginPage.ClicarEmLogin();

            Assert.AreEqual(username, mainPage.RetornarUsernameDasInformacoesDeLogin(), "O usuário retornado não é o esperado.");
        }

        [Test]
        public void RealizarLoginUsuarioInvalido()
        {

            #region Parameters
            string username = "usuarioInvalido";
            string password = "usuarioInvalido";

            //Resultado Esperado
            string messageErroExpected = "Sua conta pode estar desativada ou bloqueada ou o nome de usuário e a senha que você digitou não estão corretos.";
            #endregion

            loginPage.PreencherUsuario(username);
            loginPage.ClicarEmLogin();
            loginPage.PreencherSenha(password);
            loginPage.ClicarEmLogin();

            Assert.AreEqual(messageErroExpected, loginPage.RetornarMensagemDeErro(), "A mensagem retornada não é a esperada.");
        }

        [Test]
        public void RealizarLoginSemInformarUsuario()
        {
            #region Parameters

            //Resultado Esperado
            string messageErroExpected = "Sua conta pode estar desativada ou bloqueada ou o nome de usuário e a senha que você digitou não estão corretos.";
            #endregion

            loginPage.ClicarEmLogin();

            Assert.AreEqual(messageErroExpected, loginPage.RetornarMensagemDeErro(), "A mensagem retornada não é a esperada.");
        }

        [Test]
        public void RealizarLoginSemInformarSenha()
        {

            #region Parameters
            string username = "administrator";

            //Resultado Esperado
            string messageErroExpected = "Sua conta pode estar desativada ou bloqueada ou o nome de usuário e a senha que você digitou não estão corretos.";
            #endregion

            loginPage.PreencherUsuario(username);
            loginPage.ClicarEmLogin();
            loginPage.ClicarEmLogin();

            Assert.AreEqual(messageErroExpected, loginPage.RetornarMensagemDeErro(), "A mensagem retornada não é a esperada.");
        }

        [Test]
        public void RealizarLogoffComSucesso()
        {
            #region Parameters
            string username = "administrator";
            string password = "administrator";

            //Resultado esperado
            string urlExpected = "login_page.php";
            #endregion

            loginPage.PreencherUsuario(username);
            loginPage.ClicarEmLogin();
            loginPage.PreencherSenha(password);
            loginPage.ClicarEmLogin();
            mainPage.ClicarUserInfo();
            mainPage.ClicarSair();

            StringAssert.Contains(urlExpected, loginPage.RetornarURLAtual(), "A página atual não é a esperada.");
        }

        [Test]
        public void CriarNovaContaSemInformarDados()
        {
            #region Parameters
            //Resultado Esperado
            string messageErroExpected = "O código de confirmação não combina. Por favor, tente novamente.";
            #endregion

            loginPage.ClicarCriarNovaConta();
            signupPage.ClicarCriarConta();

            Assert.AreEqual(messageErroExpected, signupPage.RetornarMensagemDeErro(), "A mensagem retornada não é a esperada.");
        }

        [Test]
        public void CriarNovaContaCaptchaIncorreto()
        {
            #region Parameters
            string username = GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string email = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";
            string captcha = GeneralHelpers.ReturnStringWithRandomCharacters(5);

            //Resultado Esperado
            string messageErroExpected = "O código de confirmação não combina. Por favor, tente novamente.";
            #endregion

            loginPage.ClicarCriarNovaConta();
            signupPage.PreencherUsername(username);
            signupPage.PreencherEmail(email);
            signupPage.PreencherCaptcha(captcha);
            signupPage.ClicarCriarConta();

            Assert.AreEqual(messageErroExpected, signupPage.RetornarMensagemDeErro(), "A mensagem retornada não é a esperada.");
        }

        [Test]
        public void CriarNovaContaGerarNovoCaptcha()
        {
            #region Parameters
            string srcAntesGerarNovo;
            string srcDepoisGerarNovo;
            #endregion

            loginPage.ClicarCriarNovaConta();
            srcAntesGerarNovo = signupPage.RetornarSRCImagem();
            signupPage.GerarNovoCaptcha();
            srcDepoisGerarNovo = signupPage.RetornarSRCImagem();

            Assert.IsTrue(srcAntesGerarNovo != srcDepoisGerarNovo, "Um novo captcha não foi gerado.");
        }
    }
}