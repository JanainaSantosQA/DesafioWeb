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
        #region Pages, DBSteps and Flows Objects
        ManageUserEditPage manageUserEditPage;
        ManageUserUpdatePage manageUserUpdate;
        LoginFlows loginFlows;
        ManageUserFlows manageUserFlows;
        UsersDBSteps usersDBSteps;
        #endregion

        #region Parameters
        string menu = "menuGerenciarUsuarios";
        #endregion

        [SetUp]
        public void Setup()
        {
            loginFlows = new LoginFlows();
            manageUserEditPage = new ManageUserEditPage();
            manageUserFlows = new ManageUserFlows();
            manageUserUpdate = new ManageUserUpdatePage();
            usersDBSteps = new UsersDBSteps();

            loginFlows.EfetuarLogin(BuilderJson.ReturnParameterAppSettings("USER_LOGIN_PADRAO"), BuilderJson.ReturnParameterAppSettings("PASSWORD_LOGIN_PADRAO"));
        }

        [Test]
        public void EditarUsernameComSucesso()
        {
            #region Inserindo novo usuário
            string username = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string realname = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string enabled = "1";
            string cookie = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string email = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";

            usersDBSteps.InserirUsuarioDB(username, realname, enabled, cookie, email);
            #endregion

            #region Parameters
            //Resultado esperado
            string messageSucessExpected = "Operação realizada com sucesso.";
            string newUsername = "Editado";
            #endregion

            manageUserFlows.PesquisarUsuarioAtivado(menu, username);
            manageUserEditPage.ClicarNomeUsuario();
            manageUserEditPage.LimparUsername();
            manageUserEditPage.PreencherUsername(newUsername);
            manageUserEditPage.ClicarAtualizarUsuario();

            var consultarUsuarioDB = usersDBSteps.ConsultarUsuarioDB(newUsername);

            Assert.Multiple(() =>
            {
                StringAssert.Contains(messageSucessExpected, manageUserUpdate.RetornarMensagemDeSucesso(), "A mensagem retornada não é a esperada");
                Assert.IsNotNull(consultarUsuarioDB, "O username não foi atualizado.");
            });

            usersDBSteps.DeletarUsuarioDB(consultarUsuarioDB.UserId);
        }

        [Test]
        public void EditarRealnameComSucesso()
        {
            #region Inserindo novo usuário
            string username = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string realname = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string enabled = "1";
            string cookie = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string email = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";

            usersDBSteps.InserirUsuarioDB(username, realname, enabled, cookie, email);
            #endregion

            #region Parameters
            //Resultado esperado
            string messageSucessExpected = "Operação realizada com sucesso.";
            string newRealname = "Editado";
            #endregion

            manageUserFlows.PesquisarUsuarioAtivado(menu, username);
            manageUserEditPage.ClicarNomeUsuario();
            manageUserEditPage.LimparRealname();
            manageUserEditPage.PreencherRealname(newRealname);
            manageUserEditPage.ClicarAtualizarUsuario();

            var consultarUsuarioDB = usersDBSteps.ConsultarUsuarioDB(username);

            Assert.Multiple(() =>
            {
                StringAssert.Contains(messageSucessExpected, manageUserUpdate.RetornarMensagemDeSucesso(), "A mensagem retornada não é a esperada");
                Assert.AreEqual(consultarUsuarioDB.RealName, newRealname, "O realname não foi atualizado.");
            });

            usersDBSteps.DeletarUsuarioDB(consultarUsuarioDB.UserId);
        }

        [Test]
        public void EditarUsernameJaExiste()
        {
            #region Inserindo novo usuário
            string usernameUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string realnameUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string enabledUserOne = "1";
            string cookieUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string emailUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";
            
            usersDBSteps.InserirUsuarioDB(usernameUserOne, realnameUserOne, enabledUserOne, cookieUserOne, emailUserOne);

            string usernameUserTwo = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string realnameUserTwo = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string enabledUserTwo = "1";
            string cookieUserTwo = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string emailUserTwo = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";
            
            usersDBSteps.InserirUsuarioDB(usernameUserTwo, usernameUserTwo, enabledUserTwo, cookieUserTwo, emailUserTwo);
            #endregion

            #region Parameters 
            //Resultado esperado
            string messageErrorExpected = "Este nome de usuário já está sendo usado. Por favor, volte e selecione um outro.";
            #endregion

            manageUserFlows.PesquisarUsuarioAtivado(menu, usernameUserOne);
            manageUserEditPage.ClicarNomeUsuario();
            manageUserEditPage.LimparUsername();
            manageUserEditPage.PreencherUsername(usernameUserTwo);
            manageUserEditPage.ClicarAtualizarUsuario();

            Assert.AreEqual(messageErrorExpected, manageUserUpdate.RetornarMensagemDeErro(), "A mensagem retornada não é o esperada.");

            var consultarUserOneDB = usersDBSteps.ConsultarUsuarioDB(usernameUserOne);
            var consultarUserTwoDB = usersDBSteps.ConsultarUsuarioDB(usernameUserTwo);

            usersDBSteps.DeletarUsuarioDB(consultarUserOneDB.UserId);
            usersDBSteps.DeletarUsuarioDB(consultarUserTwoDB.UserId);
        }

        [Test]
        public void EditarEmailJaExiste()
        {
            #region Inserindo novo usuário
            string usernameUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string realnameUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string enabledUserOne = "1";
            string cookieUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string emailUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";
            
            usersDBSteps.InserirUsuarioDB(usernameUserOne, realnameUserOne, enabledUserOne, cookieUserOne, emailUserOne);

            string usernameUserTwo = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string realnameUserTwo = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string enabledUserTwo = "1";
            string cookieUserTwo = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string emailUserTwo = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";
            
            usersDBSteps.InserirUsuarioDB(usernameUserTwo, usernameUserTwo, enabledUserTwo, cookieUserTwo, emailUserTwo);
            #endregion

            #region Parameters
            //Resultado esperado
            string messageErrorExpected = "Este e-mail já está sendo usado. Por favor, volte e selecione outro.";
            #endregion

            manageUserFlows.PesquisarUsuarioAtivado(menu, usernameUserOne);
            manageUserEditPage.ClicarNomeUsuario();
            manageUserEditPage.LimparEmail();
            manageUserEditPage.PreencherEmail(emailUserTwo);
            manageUserEditPage.ClicarAtualizarUsuario();

            Assert.AreEqual(messageErrorExpected, manageUserUpdate.RetornarMensagemDeErro(), "A mensagem retornada não é o esperada.");

            var consultarUserOneDB = usersDBSteps.ConsultarUsuarioDB(usernameUserOne);
            var consultarUserTwoDB = usersDBSteps.ConsultarUsuarioDB(usernameUserTwo);

            usersDBSteps.DeletarUsuarioDB(consultarUserOneDB.UserId);
            usersDBSteps.DeletarUsuarioDB(consultarUserTwoDB.UserId);
        }

    }
}