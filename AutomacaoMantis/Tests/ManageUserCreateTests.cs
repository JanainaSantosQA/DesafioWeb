using NUnit.Framework;
using AutomacaoMantis.Flows;
using AutomacaoMantis.Bases;
using AutomacaoMantis.Pages;
using AutomacaoMantis.Domain;
using AutomacaoMantis.Helpers;
using AutomacaoMantis.DBSteps.Users;

namespace AutomacaoMantis.Tests
{
    public class ManageUserCreateTests : TestBase
    {
        #region Pages and Flows Objects
        LoginFlows loginFlows;
        ManageUserPage manageUserPage;
        ManageUserCreatePage manageUserCreatePage;
        #endregion

        UsersDBSteps usersDBSteps = new UsersDBSteps();

        [SetUp]
        public void Setup()
        {
            loginFlows = new LoginFlows();
            manageUserPage = new ManageUserPage();
            manageUserCreatePage = new ManageUserCreatePage();
            loginFlows.EfetuarLogin(BuilderJson.ReturnParameterAppSettings("USER_LOGIN_PADRAO"), BuilderJson.ReturnParameterAppSettings("PASSWORD_LOGIN_PADRAO"));
        }

        [Test]
        [TestCaseSource(typeof(DataDrivenHelpers), "CriarUsuarioComSucessoTestData")]
        public void CriarUsuarioComSucesso(UserDomain coluna)
        {
            #region Parameters
            string username = coluna.Username;
            string realName = coluna.RealName;
            string email = coluna.Email;
            string acessLevelName = coluna.AccessLevel;
            #endregion                       

            manageUserPage.AbrirManageUserPage();
            manageUserPage.ClicarCriarNovaConta();
            manageUserCreatePage.PreencherUsername(username);
            manageUserCreatePage.PreencherRealname(realName);
            manageUserCreatePage.PreencherEmail(email);
            manageUserCreatePage.SelecionarNivelAcesso(acessLevelName);
            manageUserCreatePage.ClicarCriarUsuario();

            var consultarUsuarioDB = usersDBSteps.ConsultarUsuarioDB(username);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(consultarUsuarioDB.Username, username, "O usuário não está correto.");
                Assert.AreEqual(consultarUsuarioDB.RealName, realName, "O nome do usuário não está correto.");
                Assert.AreEqual(consultarUsuarioDB.Email, email, "O e-mail não está correto.");
            });

            usersDBSteps.DeletarUsuarioDB(consultarUsuarioDB.UserId);
            usersDBSteps.DeletarEmailUsuarioDB(email);
        }

        [Test]
        [TestCaseSource(typeof(DataDrivenHelpers), "CriarUsuarioEmailInvalidoTestData")]
        public void CriarUsuarioEmailInvalido(UserDomain coluna)
        {
            #region Parameters
            string username = coluna.Username;
            string realName = coluna.RealName;
            string email = coluna.Email;

            //Resultado esperado
            string messageErroExpected = "Não é permitida a utilização de endereços de e-mail descartáveis.";
            #endregion                      

            manageUserPage.AbrirManageUserPage();
            manageUserPage.ClicarCriarNovaConta();
            manageUserCreatePage.PreencherUsername(username);
            manageUserCreatePage.PreencherRealname(realName);
            manageUserCreatePage.PreencherEmail(email);
            manageUserCreatePage.ClicarCriarUsuario();

            var consultarUsuarioDB = usersDBSteps.ConsultarUsuarioDB(username);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(messageErroExpected, manageUserCreatePage.RetornarMensagemDeErro(), "A mensagem retornada não é a esperada.");
                Assert.IsNull(consultarUsuarioDB, "Usuário existente no banco.");
            });
        }

        [Test]
        [TestCaseSource(typeof(DataDrivenHelpers), "CriarUsuarioSemInformarUsernameTestData")]
        public void CriarUsuarioSemInformarUsername(UserDomain coluna)
        {
            #region Parameters
            string realName = coluna.RealName;
            string email = coluna.Email;

            //Resultado esperado
            string messageErroExpected = "O nome de usuário não é inválido. Nomes de usuário podem conter apenas letras, números, espaços, hífens, pontos, sinais de mais e sublinhados.";
            #endregion                      

            manageUserPage.AbrirManageUserPage();
            manageUserPage.ClicarCriarNovaConta();
            manageUserCreatePage.PreencherRealname(realName);
            manageUserCreatePage.PreencherEmail(email);
            manageUserCreatePage.ClicarCriarUsuario();

            Assert.AreEqual(messageErroExpected, manageUserCreatePage.RetornarMensagemDeErro(), "A mensagem retornada não é a esperada.");
        }

        [Test]
        public void CriarUsuarioUsernameJaExiste()
        {
            #region Inserindo novo usuário
            string usernameUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string realnameUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string enabledUserOne = "1";
            string cookieUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string emailUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";
            
            usersDBSteps.InserirUsuarioDB(usernameUserOne, realnameUserOne, enabledUserOne, cookieUserOne, emailUserOne);
            #endregion

            #region Parameters       
            string realNameUserTwo = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string emailUserTwo = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";
            string acessLevelNameUserTwo = "gerente";

            //Resultado esperado
            string messageErroExpected = "Este nome de usuário já está sendo usado. Por favor, volte e selecione um outro.";
            #endregion                      

            manageUserPage.AbrirManageUserPage();
            manageUserPage.ClicarCriarNovaConta();
            manageUserCreatePage.PreencherUsername(usernameUserOne);
            manageUserCreatePage.PreencherRealname(realNameUserTwo);
            manageUserCreatePage.PreencherEmail(emailUserTwo);
            manageUserCreatePage.SelecionarNivelAcesso(acessLevelNameUserTwo);
            manageUserCreatePage.ClicarCriarUsuario();

            Assert.AreEqual(messageErroExpected, manageUserCreatePage.RetornarMensagemDeErro(), "A mensagem retornada não é a esperada.");

            var consultarUserOneDB = usersDBSteps.ConsultarUsuarioDB(usernameUserOne);
            usersDBSteps.DeletarUsuarioDB(consultarUserOneDB.UserId);
        }

        [Test]
        public void CriarUsuarioEmailJaExiste()
        {
            #region Inserindo novo usuário
            string usernameUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string realnameUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string enabledUserOne = "1";
            string cookieUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string emailUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";
           
            usersDBSteps.InserirUsuarioDB(usernameUserOne, realnameUserOne, enabledUserOne, cookieUserOne, emailUserOne);
            #endregion

            #region Parameters       
            string usernameUserTwo = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string realNameUserTwo = GeneralHelpers.ReturnStringWithRandomCharacters(6);            
            string acessLevelNameUserTwo = "gerente";

            //Resultado esperado
            string messageErroExpected = "Este e-mail já está sendo usado. Por favor, volte e selecione outro.";
            #endregion                      

            manageUserPage.AbrirManageUserPage();
            manageUserPage.ClicarCriarNovaConta();
            manageUserCreatePage.PreencherUsername(usernameUserTwo);
            manageUserCreatePage.PreencherRealname(realNameUserTwo);
            manageUserCreatePage.PreencherEmail(emailUserOne);
            manageUserCreatePage.SelecionarNivelAcesso(acessLevelNameUserTwo);
            manageUserCreatePage.ClicarCriarUsuario();

            Assert.AreEqual(messageErroExpected, manageUserCreatePage.RetornarMensagemDeErro(), "A mensagem retornada não é a esperada.");

            var consultarUserOneDB = usersDBSteps.ConsultarUsuarioDB(usernameUserOne);
            usersDBSteps.DeletarUsuarioDB(consultarUserOneDB.UserId);
        }
    }
}