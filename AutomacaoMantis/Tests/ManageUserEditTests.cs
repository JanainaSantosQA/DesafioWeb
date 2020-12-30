using NUnit.Framework;
using AutomacaoMantis.Bases;
using AutomacaoMantis.Pages;
using AutomacaoMantis.Flows;
using AutomacaoMantis.Helpers;
using AutomacaoMantis.DBSteps.Users;

namespace AutomacaoMantis.Tests
{
    public class ManageUserEditTests : TestBase
    {
        UsersDBSteps usersDBSteps = new UsersDBSteps();

        #region Pages and Flows Objects
        ManageUserEditPage manageUserEditPage;
        ManageUserUpdatePage manageUserUpdate;
        LoginFlows loginFlows;
        ManageUserFlows manageUserFlows;
        #endregion

        [SetUp]
        public void Setup()
        {
            loginFlows = new LoginFlows();
            loginFlows.EfetuarLogin(BuilderJson.ReturnParameterAppSettings("USER_LOGIN_PADRAO"), BuilderJson.ReturnParameterAppSettings("PASSWORD_LOGIN_PADRAO"));
        }

        [Test]
        public void EditarUsernameComSucesso()
        {
            manageUserEditPage = new ManageUserEditPage();    
            manageUserFlows = new ManageUserFlows();

            #region Parameters / Inserindo novo usuário
            string username = "EditarUsernameComSucesso";
            string realname = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string enabled = "1";
            string cookie = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string email = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";
            usersDBSteps.InserirUsuarioDB(username, realname, enabled, cookie, email);            

            //Resultado esperado
            string newUsername = "Editado";
            #endregion

            manageUserFlows.PesquisarUsuarioAtivado(username);
            manageUserEditPage.ClicarNomeUsuario();
            manageUserEditPage.LimparUsername();
            manageUserEditPage.PreencherUsername(newUsername);
            manageUserEditPage.ClicarAtualizarUsuario();

            var consultarUsuarioDB = usersDBSteps.ConsultarUsuarioDB(newUsername);
            Assert.IsNotNull(consultarUsuarioDB, "O username não foi atualizado.");

            usersDBSteps.DeletarUsuarioDB(consultarUsuarioDB.UserId);
        }

        [Test]
        public void EditarRealnameComSucesso()
        {
            manageUserEditPage = new ManageUserEditPage();
            manageUserFlows = new ManageUserFlows();

            #region Parameters / Inserindo novo usuário
            string username = "EditarRealnameComSucesso";
            string realname = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string enabled = "1";
            string cookie = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string email = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";
            usersDBSteps.InserirUsuarioDB(username, realname, enabled, cookie, email);

            //Resultado esperado
            string newRealname = "Editado";
            #endregion

            manageUserFlows.PesquisarUsuarioAtivado(username);
            manageUserEditPage.ClicarNomeUsuario();
            manageUserEditPage.LimparRealname();
            manageUserEditPage.PreencherRealname(newRealname);
            manageUserEditPage.ClicarAtualizarUsuario();

            var consultarUsuarioDB = usersDBSteps.ConsultarUsuarioDB(username);
            Assert.AreEqual(consultarUsuarioDB.RealName, newRealname, "O realname não foi atualizado.");

            usersDBSteps.DeletarUsuarioDB(consultarUsuarioDB.UserId);
        }

        [Test]
        public void EditarUserSemSucessoUsernameJaExiste()
        {
            manageUserEditPage = new ManageUserEditPage();
            manageUserFlows = new ManageUserFlows();
            manageUserUpdate = new ManageUserUpdatePage();

            #region Parameters / Inserindo novo usuário
            string usernameUserOne = "EditarUsernameSemSucesso1";
            string realnameUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string enabledUserOne = "1";
            string cookieUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string emailUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";
            usersDBSteps.InserirUsuarioDB(usernameUserOne, realnameUserOne, enabledUserOne, cookieUserOne, emailUserOne);

            string usernameUserTwo = "EditarUsernameSemSucesso2";
            string realnameUserTwo = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string enabledUserTwo = "1";
            string cookieUserTwo = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string emailUserTwo = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";
            usersDBSteps.InserirUsuarioDB(usernameUserTwo, usernameUserTwo, enabledUserTwo, cookieUserTwo, emailUserTwo);

            //Resultado esperado
            string messageError = "Este nome de usuário já está sendo usado. Por favor, volte e selecione um outro.";
            #endregion

            manageUserFlows.PesquisarUsuarioAtivado(usernameUserOne);
            manageUserEditPage.ClicarNomeUsuario();
            manageUserEditPage.LimparUsername();
            manageUserEditPage.PreencherUsername(usernameUserTwo);
            manageUserEditPage.ClicarAtualizarUsuario();

            Assert.AreEqual(messageError, manageUserUpdate.RetornarMensagemDeErro(), "A mensagem retornada não é o esperada.");

            var consultarUserOneDB = usersDBSteps.ConsultarUsuarioDB(usernameUserOne);
            var consultarUserTwoDB = usersDBSteps.ConsultarUsuarioDB(usernameUserTwo);

            usersDBSteps.DeletarUsuarioDB(consultarUserOneDB.UserId);
            usersDBSteps.DeletarUsuarioDB(consultarUserTwoDB.UserId);
        }

        [Test]
        public void EditarUserSemSucessoEmailJaExiste()
        {
            manageUserEditPage = new ManageUserEditPage();
            manageUserFlows = new ManageUserFlows();
            manageUserUpdate = new ManageUserUpdatePage();

            #region Parameters / Inserindo novo usuário
            string usernameUserOne = "EditarEmailSemSucesso1";
            string realnameUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string enabledUserOne = "1";
            string cookieUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string emailUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";
            usersDBSteps.InserirUsuarioDB(usernameUserOne, realnameUserOne, enabledUserOne, cookieUserOne, emailUserOne);

            string usernameUserTwo = "EditarEmailSemSucesso2";
            string realnameUserTwo = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string enabledUserTwo = "1";
            string cookieUserTwo = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string emailUserTwo = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";
            usersDBSteps.InserirUsuarioDB(usernameUserTwo, usernameUserTwo, enabledUserTwo, cookieUserTwo, emailUserTwo);

            //Resultado esperado
            string messageError = "Este e-mail já está sendo usado. Por favor, volte e selecione outro.";
            #endregion

            manageUserFlows.PesquisarUsuarioAtivado(usernameUserOne);
            manageUserEditPage.ClicarNomeUsuario();
            manageUserEditPage.LimparEmail();
            manageUserEditPage.PreencherEmail(emailUserTwo);
            manageUserEditPage.ClicarAtualizarUsuario();

            Assert.AreEqual(messageError, manageUserUpdate.RetornarMensagemDeErro(), "A mensagem retornada não é o esperada.");

            var consultarUserOneDB = usersDBSteps.ConsultarUsuarioDB(usernameUserOne);
            var consultarUserTwoDB = usersDBSteps.ConsultarUsuarioDB(usernameUserTwo);

            usersDBSteps.DeletarUsuarioDB(consultarUserOneDB.UserId);
            usersDBSteps.DeletarUsuarioDB(consultarUserTwoDB.UserId);
        }

    }
}