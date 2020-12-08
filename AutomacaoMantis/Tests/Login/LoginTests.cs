using AutomacaoMantis.Bases;
using AutomacaoMantis.Helpers;
using AutomacaoMantis.Pages;
using NUnit.Framework;
using System.Collections;

namespace AutomacaoMantis.Tests.Login
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        #region Pages and Flows Objects
        LoginPage loginPage;
        //MainPage mainPage;
        #endregion

        [Test]
        public void RealizarLoginComSucesso()
        {
            loginPage = new LoginPage();
            //mainPage = new MainPage();

            #region Parameters
            string usuario = "administrator";
            string senha = "administrator";
            #endregion

            loginPage.PreencherUsuario(usuario);
            loginPage.PreencherSenha(senha);
            loginPage.ClicarEmLogin();

            //Assert.AreEqual(usuario, mainPage.RetornaUsernameDasInformacoesDeLogin());
        }

    }
}