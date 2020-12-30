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

        #region Pages and Flows Objects
        LoginFlows loginFlows;
        ManageUserPage manageUserPage;
        #endregion

        [SetUp]
        public void Setup()
        {
            loginFlows = new LoginFlows();
            loginFlows.EfetuarLogin(BuilderJson.ReturnParameterAppSettings("USER_LOGIN_PADRAO"), BuilderJson.ReturnParameterAppSettings("PASSWORD_LOGIN_PADRAO"));
        }

        [Test]
        public void PesquisarUsuarioAtivado()
        {
            manageUserPage = new ManageUserPage();

            #region Parameters / Inserindo um novo usuário
            string username = "ConsultaUsuarioAtivado";
            string realname = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string enabled = "1";
            string cookie = "ConsultaUsuarioAtivado";
            string email = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";
            usersDBSteps.InserirUsuarioDB(username,realname, enabled, cookie, email);

            var consultarUsuarioDB = usersDBSteps.ConsultarUsuarioDB(username);
            #endregion

            manageUserPage.AbrirManageUserPage();
            manageUserPage.PesquisarUsuario(username);
            manageUserPage.ClicarAplicarFiltro();

            Assert.AreEqual(username, manageUserPage.RetornarUsuarioExibidoResultadoPesquisa(), "O usuário retornado não é o esperado.");

            usersDBSteps.DeletarUsuarioDB(consultarUsuarioDB.UserId);
        }

        [Test]
        public void PesquisarUsuarioDesativado()
        {
            manageUserPage = new ManageUserPage();

            #region Parameters / Inserindo um novo usuário
            string username = "ConsultaUsuarioDesativado";
            string realname = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string enabled = "0";
            string cookie = "ConsultaUsuarioDesativado";
            string email = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";
            usersDBSteps.InserirUsuarioDB(username, realname, enabled, cookie, email);

            var consultarUsuarioDB = usersDBSteps.ConsultarUsuarioDB(username);
            #endregion

            manageUserPage.AbrirManageUserPage();
            manageUserPage.ClicarMostrarDesativados();
            manageUserPage.PesquisarUsuario(username);
            manageUserPage.ClicarAplicarFiltro();

            Assert.AreEqual(username, manageUserPage.RetornarUsuarioExibidoResultadoPesquisa(), "O usuário retornado não é o esperado.");

            usersDBSteps.DeletarUsuarioDB(consultarUsuarioDB.UserId);
        }
    }
}