using NUnit.Framework;
using AutomacaoMantis.Bases;
using AutomacaoMantis.Pages;
using AutomacaoMantis.Flows;
using AutomacaoMantis.Helpers;
using AutomacaoMantis.DBSteps.Users;

namespace AutomacaoMantis.Tests
{
    public class ManageUserTests : TestBase
    {
        UsersDBSteps usersDBSteps = new UsersDBSteps();

        #region Pages, DBSteps and Flows Objects
        LoginFlows loginFlows;
        ManageUserPage manageUserPage;
        MainPage mainPage;
        #endregion

        #region Parameters
        string menu = "menuGerenciarUsuarios";
        #endregion

        [SetUp]
        public void Setup()
        {
            loginFlows = new LoginFlows();
            manageUserPage = new ManageUserPage();
            mainPage = new MainPage();

            loginFlows.EfetuarLogin(BuilderJson.ReturnParameterAppSettings("USER_LOGIN_PADRAO"), BuilderJson.ReturnParameterAppSettings("PASSWORD_LOGIN_PADRAO"));
        }

        [Test]
        public void PesquisarUsuarioAtivado()
        {          
            #region Inserindo um novo usuário
            string username = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string realname = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string enabled = "1";
            string cookie = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string email = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";

            usersDBSteps.InserirUsuarioDB(username,realname, enabled, cookie, email);

            var consultarUsuarioDB = usersDBSteps.ConsultarUsuarioDB(username);
            #endregion

            mainPage.ClicarMenu(menu);
            manageUserPage.PesquisarUsuario(username);
            manageUserPage.ClicarAplicarFiltro();

            Assert.AreEqual(username, manageUserPage.RetornarUsuarioExibidoResultadoPesquisa(), "O usuário retornado não é o esperado.");

            usersDBSteps.DeletarUsuarioDB(consultarUsuarioDB.UserId);
        }

        [Test]
        public void PesquisarUsuarioDesativado()
        {
            #region Inserindo um novo usuário
            string username = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string realname = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string enabled = "0";
            string cookie = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string email = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";

            usersDBSteps.InserirUsuarioDB(username, realname, enabled, cookie, email);

            var consultarUsuarioDB = usersDBSteps.ConsultarUsuarioDB(username);
            #endregion

            mainPage.ClicarMenu(menu);
            manageUserPage.ClicarMostrarDesativados();
            manageUserPage.PesquisarUsuario(username);
            manageUserPage.ClicarAplicarFiltro();

            Assert.AreEqual(username, manageUserPage.RetornarUsuarioExibidoResultadoPesquisa(), "O usuário retornado não é o esperado.");

            usersDBSteps.DeletarUsuarioDB(consultarUsuarioDB.UserId);
        }
    }
}