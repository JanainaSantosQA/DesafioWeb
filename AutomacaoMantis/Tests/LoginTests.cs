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
        MyViewPage myViewPage;
        SignupPage signupPage;
        #endregion

        [SetUp]
        public void Setup()
        {
            loginPage = new LoginPage();
            myViewPage = new MyViewPage();
            signupPage = new SignupPage();
        }

        [Test]
        public void RealizarLoginComSucesso()
        {
            #region Parameters
            string username = BuilderJson.ReturnParameterAppSettings("USER_LOGIN_PADRAO");
            string password = BuilderJson.ReturnParameterAppSettings("PASSWORD_LOGIN_PADRAO");
            #endregion

            #region Actions
            loginPage.PreencherUsuario(username);
            loginPage.ClicarEmLogin();
            loginPage.PreencherSenha(password);
            loginPage.ClicarEmLogin();
            #endregion

            #region Validations
            Assert.AreEqual(username, myViewPage.RetornarUsernameDasInformacoesDeLogin(), "O usuário retornado não é o esperado.");
            #endregion
        }

        [Test]
        public void RealizarLoginUsuarioESenhaInvalidos()
        {
            #region Parameters
            string username = "usuarioInvalido";
            string password = "usuarioInvalido";

            //Resultado Esperado
            string messageErrorExpected = "Sua conta pode estar desativada ou bloqueada ou o nome de usuário e a senha que você digitou não estão corretos.";
            #endregion

            #region Actions
            loginPage.PreencherUsuario(username);
            loginPage.ClicarEmLogin();
            loginPage.PreencherSenha(password);
            loginPage.ClicarEmLogin();
            #endregion

            #region Validations
            Assert.AreEqual(messageErrorExpected, loginPage.RetornarMensagemDeErro(), "A mensagem retornada não é a esperada.");
            #endregion
        }

        [Test]
        public void RealizarLoginSemInformarUsuario()
        {
            #region Parameters
            //Resultado Esperado
            string messageErrorExpected = "Sua conta pode estar desativada ou bloqueada ou o nome de usuário e a senha que você digitou não estão corretos.";
            #endregion

            #region Actions
            loginPage.ClicarEmLogin();
            #endregion

            #region Validations
            Assert.AreEqual(messageErrorExpected, loginPage.RetornarMensagemDeErro(), "A mensagem retornada não é a esperada.");
            #endregion
        }

        [Test]
        public void RealizarLoginSemInformarSenha()
        {
            #region Parameters
            string username = BuilderJson.ReturnParameterAppSettings("USER_LOGIN_PADRAO");

            //Resultado Esperado
            string messageErrorExpected = "Sua conta pode estar desativada ou bloqueada ou o nome de usuário e a senha que você digitou não estão corretos.";
            #endregion

            #region Actions
            loginPage.PreencherUsuario(username);
            loginPage.ClicarEmLogin();
            loginPage.ClicarEmLogin();
            #endregion

            #region Validations
            Assert.AreEqual(messageErrorExpected, loginPage.RetornarMensagemDeErro(), "A mensagem retornada não é a esperada.");
            #endregion
        }

        [Test]
        public void RealizarLogoffComSucesso()
        {
            #region Parameters
            string username = BuilderJson.ReturnParameterAppSettings("USER_LOGIN_PADRAO");
            string password = BuilderJson.ReturnParameterAppSettings("PASSWORD_LOGIN_PADRAO");

            //Resultado esperado
            string urlExpected = "login_page.php";
            #endregion

            #region Actions
            loginPage.PreencherUsuario(username);
            loginPage.ClicarEmLogin();
            loginPage.PreencherSenha(password);
            loginPage.ClicarEmLogin();
            myViewPage.ClicarUsuarioInfo();
            myViewPage.ClicarSair();
            #endregion

            #region Validations
            StringAssert.Contains(urlExpected, loginPage.RetornarURLAtual(), "A página atual não é a esperada.");
            #endregion
        }

        [Test]
        public void CriarNovaContaSemInformarDados()
        {
            #region Parameters
            //Resultado Esperado
            string messageErrorExpected = "O código de confirmação não combina. Por favor, tente novamente.";
            #endregion

            #region Actions
            loginPage.ClicarCriarNovaConta();
            signupPage.ClicarCriarConta();
            #endregion

            #region Validations
            Assert.AreEqual(messageErrorExpected, signupPage.RetornarMensagemDeErro(), "A mensagem retornada não é a esperada.");
            #endregion
        }

        [Test]
        public void CriarNovaContaCaptchaIncorreto()
        {
            #region Parameters
            string username = GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string email = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";
            string captcha = GeneralHelpers.ReturnStringWithRandomCharacters(5);

            //Resultado Esperado
            string messageErrorExpected = "O código de confirmação não combina. Por favor, tente novamente.";
            #endregion

            #region Actions
            loginPage.ClicarCriarNovaConta();
            signupPage.PreencherUsername(username);
            signupPage.PreencherEmail(email);
            signupPage.PreencherCaptcha(captcha);
            signupPage.ClicarCriarConta();
            #endregion

            #region Validations
            Assert.AreEqual(messageErrorExpected, signupPage.RetornarMensagemDeErro(), "A mensagem retornada não é a esperada.");
            #endregion
        }

        [Test]
        public void CriarNovaContaGerarNovoCaptcha()
        {
            #region Parameters
            string srcAntesGerarNovoCaptcha;
            string srcDepoisGerarNovoCaptcha;
            #endregion

            #region Actions
            loginPage.ClicarCriarNovaConta();
            srcAntesGerarNovoCaptcha = signupPage.RetornarSRCImagem();
            signupPage.GerarNovoCaptcha();
            srcDepoisGerarNovoCaptcha = signupPage.RetornarSRCImagem();
            #endregion

            #region Validations
            Assert.IsTrue(srcAntesGerarNovoCaptcha != srcDepoisGerarNovoCaptcha, "Um novo captcha não foi gerado.");
            #endregion
        }
    }
}