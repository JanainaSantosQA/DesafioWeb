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

        [Test]
        public void RealizarLoginComSucesso()
        {
            loginPage = new LoginPage();
            mainPage = new MainPage();

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
        public void RealizarLoginSemSucessoUsuarioInexistente()
        {
            loginPage = new LoginPage();

            #region Parameters
            string username = "usuarioInexistente";
            string password = "usuarioInexistente";

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
        public void RealizarLoginSemSucessoSemInformarUsuario()
        {
            loginPage = new LoginPage();

            #region Parameters

            //Resultado Esperado
            string messageErroExpected = "Sua conta pode estar desativada ou bloqueada ou o nome de usuário e a senha que você digitou não estão corretos.";
            #endregion

            loginPage.ClicarEmLogin();

            Assert.AreEqual(messageErroExpected, loginPage.RetornarMensagemDeErro(), "A mensagem retornada não é a esperada.");
        }

        [Test]
        public void RealizarLoginSemSucessoSemInformarSenha()
        {
            loginPage = new LoginPage();

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