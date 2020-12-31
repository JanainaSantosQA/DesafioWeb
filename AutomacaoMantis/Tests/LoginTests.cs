using NUnit.Framework;
using AutomacaoMantis.Pages;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Tests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        #region Pages and Flows Objects
        LoginPage loginPage;
        MainPage mainPage;
        #endregion

        [SetUp]
        public void Setup()
        {
            loginPage = new LoginPage();
            mainPage = new MainPage();
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
    }
}