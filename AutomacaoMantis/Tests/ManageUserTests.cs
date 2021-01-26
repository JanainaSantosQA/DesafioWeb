using NUnit.Framework;
using AutomacaoMantis.Flows;
using AutomacaoMantis.Bases;
using AutomacaoMantis.Pages;
using AutomacaoMantis.Domain;
using AutomacaoMantis.Helpers;
using AutomacaoMantis.DBSteps.Users;
using AutomacaoMantis.DBSteps.Projects;

namespace AutomacaoMantis.Tests
{
    public class ManageUserTests : TestBase
    {
        #region Pages, DBSteps and Flows Objects
        MyViewPage mainPage;
        ManageUserPage manageUserPage;
        ManageUserCreatePage manageUserCreatePage;
        ManageUserEditPage manageUserEditPage;

        ProjectsDBSteps projectsDBSteps;
        UsersDBSteps usersDBSteps;

        LoginFlows loginFlows;
        ManageUserFlows manageUserFlows;
        #endregion

        #region Parameters
        string menu = "menuGerenciarUsuarios";
        #endregion

        [SetUp]
        public void Setup()
        {
            mainPage = new MyViewPage();
            manageUserPage = new ManageUserPage();
            manageUserCreatePage = new ManageUserCreatePage();
            manageUserEditPage = new ManageUserEditPage();

            projectsDBSteps = new ProjectsDBSteps();
            usersDBSteps = new UsersDBSteps();

            loginFlows = new LoginFlows();
            manageUserFlows = new ManageUserFlows();

            loginFlows.EfetuarLogin(BuilderJson.ReturnParameterAppSettings("USER_LOGIN_PADRAO"), BuilderJson.ReturnParameterAppSettings("PASSWORD_LOGIN_PADRAO"));
        }

        [Test]
        public void PesquisarUsuarioAtivoComSucesso()
        {
            #region Inserindo um novo usuário
            string username = "User_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string realname = "Realname_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string enabled = "1";
            string cookie = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string email = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";

            var usuarioCriadoDB = usersDBSteps.InserirUsuarioDB(username, realname, enabled, cookie, email);
            #endregion

            #region Actions
            mainPage.ClicarMenu(menu);
            manageUserPage.PesquisarUsuario(username);
            manageUserPage.ClicarAplicarFiltro();
            #endregion

            #region Validations
            Assert.AreEqual(username, manageUserPage.RetornaUsuarioExibidoResultadoPesquisa(), "O usuário retornado não é o esperado.");
            #endregion

            usersDBSteps.DeletarUsuarioDB(usuarioCriadoDB.UserId);
        }

        [Test]
        public void PesquisarUsuarioDesativadoComSucesso()
        {
            #region Inserindo um novo usuário
            string username = "User_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string realname = "Realname_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string enabled = "0";
            string cookie = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string email = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";

            var usuarioCriadoDB = usersDBSteps.InserirUsuarioDB(username, realname, enabled, cookie, email);
            #endregion

            #region Actions
            mainPage.ClicarMenu(menu);
            manageUserPage.ClicarMostrarUsuariosDesativados();
            manageUserPage.PesquisarUsuario(username);
            manageUserPage.ClicarAplicarFiltro();
            #endregion

            #region Validations
            Assert.AreEqual(username, manageUserPage.RetornaUsuarioExibidoResultadoPesquisa(), "O usuário retornado não é o esperado.");
            #endregion

            usersDBSteps.DeletarUsuarioDB(usuarioCriadoDB.UserId);
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

            //Resultado esperado
            string messageSucessExpected = "Usuário " + username + " criado com um nível de acesso de " + acessLevelName + "";
            #endregion                       

            #region Actions
            mainPage.ClicarMenu(menu);
            manageUserPage.ClicarCriarNovaConta();
            manageUserCreatePage.PreencherUsername(username);
            manageUserCreatePage.PreencherNomeUsuario(realName);
            manageUserCreatePage.PreencherEmail(email);
            manageUserCreatePage.SelecionarNivelAcesso(acessLevelName);
            manageUserCreatePage.ClicarCriarUsuario();
            #endregion

            #region Validations
            StringAssert.Contains(messageSucessExpected, manageUserCreatePage.RetornaMensagemDeSucesso(), "A mensagem retornada não é a esperada");

            var usuarioCriadoDB = usersDBSteps.ConsultarUsuarioDB(username);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(usuarioCriadoDB.Username, username, "O usuário não está correto.");
                Assert.AreEqual(usuarioCriadoDB.RealName, realName, "O nome do usuário não está correto.");
                Assert.AreEqual(usuarioCriadoDB.Email, email, "O e-mail não está correto.");
            });
            #endregion

            usersDBSteps.DeletarUsuarioDB(usuarioCriadoDB.UserId);
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
            string messageErrorExpected = "Não é permitida a utilização de endereços de e-mail descartáveis.";
            #endregion                      

            #region Actions
            mainPage.ClicarMenu(menu);
            manageUserPage.ClicarCriarNovaConta();
            manageUserCreatePage.PreencherUsername(username);
            manageUserCreatePage.PreencherNomeUsuario(realName);
            manageUserCreatePage.PreencherEmail(email);
            manageUserCreatePage.ClicarCriarUsuario();
            #endregion

            #region Validations
            var usuarioCriadoDB = usersDBSteps.ConsultarUsuarioDB(username);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(messageErrorExpected, manageUserCreatePage.RetornaMensagemDeErro(), "A mensagem retornada não é a esperada.");
                Assert.IsNull(usuarioCriadoDB, "O e-mail informado é inválido, porém mesmo assim o usuário foi criado.");
            });
            #endregion
        }

        [Test]
        [TestCaseSource(typeof(DataDrivenHelpers), "CriarUsuarioSemInformarUsernameTestData")]
        public void CriarUsuarioSemInformarUsername(UserDomain coluna)
        {
            #region Parameters
            string realName = coluna.RealName;
            string email = coluna.Email;

            //Resultado esperado
            string messageErrorExpected = "O nome de usuário não é inválido. Nomes de usuário podem conter apenas letras, números, espaços, hífens, pontos, sinais de mais e sublinhados.";
            #endregion                      

            #region Actions
            mainPage.ClicarMenu(menu);
            manageUserPage.ClicarCriarNovaConta();
            manageUserCreatePage.PreencherNomeUsuario(realName);
            manageUserCreatePage.PreencherEmail(email);
            manageUserCreatePage.ClicarCriarUsuario();
            #endregion

            #region Validations
            Assert.AreEqual(messageErrorExpected, manageUserCreatePage.RetornaMensagemDeErro(), "A mensagem retornada não é a esperada.");
            #endregion
        }

        [Test]
        public void CriarUsuarioUsernameJaExiste()
        {
            #region Inserindo novo usuário
            string usernameUserOne = "User_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string realnameUserOne = "Realname_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string enabledUserOne = "1";
            string cookieUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string emailUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";

            var usuarioCriadoDB = usersDBSteps.InserirUsuarioDB(usernameUserOne, realnameUserOne, enabledUserOne, cookieUserOne, emailUserOne);
            #endregion

            #region Parameters       
            string realNameUserTwo = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string emailUserTwo = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";
            string acessLevelNameUserTwo = "gerente";

            //Resultado esperado
            string messageErrorExpected = "Este nome de usuário já está sendo usado. Por favor, volte e selecione um outro.";
            #endregion

            #region Actions
            mainPage.ClicarMenu(menu);
            manageUserPage.ClicarCriarNovaConta();
            manageUserCreatePage.PreencherUsername(usernameUserOne);
            manageUserCreatePage.PreencherNomeUsuario(realNameUserTwo);
            manageUserCreatePage.PreencherEmail(emailUserTwo);
            manageUserCreatePage.SelecionarNivelAcesso(acessLevelNameUserTwo);
            manageUserCreatePage.ClicarCriarUsuario();
            #endregion

            #region Validations
            Assert.AreEqual(messageErrorExpected, manageUserCreatePage.RetornaMensagemDeErro(), "A mensagem retornada não é a esperada.");
            #endregion

            usersDBSteps.DeletarUsuarioDB(usuarioCriadoDB.UserId);
        }

        [Test]
        public void CriarUsuarioEmailJaExiste()
        {
            #region Inserindo novo usuário
            string usernameUserOne = "User_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string realnameUserOne = "Realname_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string enabledUserOne = "1";
            string cookieUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string emailUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";

            var usuarioCriadoDB = usersDBSteps.InserirUsuarioDB(usernameUserOne, realnameUserOne, enabledUserOne, cookieUserOne, emailUserOne);
            #endregion

            #region Parameters       
            string usernameUserTwo = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string realNameUserTwo = GeneralHelpers.ReturnStringWithRandomCharacters(6);
            string acessLevelNameUserTwo = "gerente";

            //Resultado esperado
            string messageErrorExpected = "Este e-mail já está sendo usado. Por favor, volte e selecione outro.";
            #endregion                      

            #region Actions
            mainPage.ClicarMenu(menu);
            manageUserPage.ClicarCriarNovaConta();
            manageUserCreatePage.PreencherUsername(usernameUserTwo);
            manageUserCreatePage.PreencherNomeUsuario(realNameUserTwo);
            manageUserCreatePage.PreencherEmail(emailUserOne);
            manageUserCreatePage.SelecionarNivelAcesso(acessLevelNameUserTwo);
            manageUserCreatePage.ClicarCriarUsuario();
            #endregion

            #region Validations
            Assert.AreEqual(messageErrorExpected, manageUserCreatePage.RetornaMensagemDeErro(), "A mensagem retornada não é a esperada.");
            #endregion

            usersDBSteps.DeletarUsuarioDB(usuarioCriadoDB.UserId);
        }

        [Test]
        public void EditarUsernameComSucesso()
        {
            #region Inserindo novo usuário
            string username = "User_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string realname = "Realname_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string enabled = "1";
            string cookie = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string email = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";

            usersDBSteps.InserirUsuarioDB(username, realname, enabled, cookie, email);
            #endregion

            #region Parameters
            //Resultado esperado
            string messageSucessExpected = "Operação realizada com sucesso.";
            string newUsername = "User_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            #endregion

            #region Actions
            manageUserFlows.AcessarUsuarioCriadoAtivo(menu, username);
            manageUserEditPage.PreencherUsername(newUsername);
            manageUserEditPage.ClicarAtualizarUsuario();
            #endregion

            #region Validations
            Assert.AreEqual(messageSucessExpected, manageUserEditPage.RetornaMensagemDeSucesso(), "A mensagem retornada não é a esperada.");

            var usuarioCriadoDB = usersDBSteps.ConsultarUsuarioDB(newUsername);
            Assert.IsNotNull(usuarioCriadoDB, "O username não foi atualizado.");
            #endregion

            usersDBSteps.DeletarUsuarioDB(usuarioCriadoDB.UserId);
            usersDBSteps.DeletarEmailUsuarioDB(email);
        }

        [Test]
        public void EditarRealnameComSucesso()
        {
            #region Inserindo novo usuário
            string username = "User_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string realname = "Realname_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string enabled = "1";
            string cookie = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string email = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";

            usersDBSteps.InserirUsuarioDB(username, realname, enabled, cookie, email);
            #endregion

            #region Parameters
            //Resultado esperado
            string newRealname = "User_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string messageSucessExpected = "Operação realizada com sucesso.";
            #endregion

            #region Actions
            manageUserFlows.AcessarUsuarioCriadoAtivo(menu, username);
            manageUserEditPage.PreencherNomeUsuario(newRealname);
            manageUserEditPage.ClicarAtualizarUsuario();
            #endregion

            #region Validations
            Assert.AreEqual(messageSucessExpected, manageUserEditPage.RetornaMensagemDeSucesso(), "A mensagem retornada não é a esperada.");

            var usuarioCriadoDB = usersDBSteps.ConsultarUsuarioDB(username);               
            Assert.AreEqual(usuarioCriadoDB.RealName, newRealname, "O realname não foi atualizado.");
            #endregion

            usersDBSteps.DeletarUsuarioDB(usuarioCriadoDB.UserId);
            usersDBSteps.DeletarEmailUsuarioDB(email);
        }

        [Test]
        public void EditarUsernameJaExiste()
        {
            #region Inserindo dois novos usuários
            string usernameUserOne = "User_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string realnameUserOne = "Realname_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string enabledUserOne = "1";
            string cookieUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string emailUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";
            var primeiroUsuarioCriadoDB = usersDBSteps.InserirUsuarioDB(usernameUserOne, realnameUserOne, enabledUserOne, cookieUserOne, emailUserOne);

            string usernameUserTwo = "User_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string realnameUserTwo= "Realname_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string enabledUserTwo = "1";
            string cookieUserTwo = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string emailUserTwo = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";
            var segundoUsuarioCriadoDB = usersDBSteps.InserirUsuarioDB(usernameUserTwo, realnameUserTwo, enabledUserTwo, cookieUserTwo, emailUserTwo);
            #endregion

            #region Parameters 
            //Resultado esperado
            string messageErrorExpected = "Este nome de usuário já está sendo usado. Por favor, volte e selecione um outro.";
            #endregion

            #region Actions
            manageUserFlows.AcessarUsuarioCriadoAtivo(menu, usernameUserOne);
            manageUserEditPage.PreencherUsername(usernameUserTwo);
            manageUserEditPage.ClicarAtualizarUsuario();
            #endregion

            #region Validations
            Assert.AreEqual(messageErrorExpected, manageUserEditPage.RetornaMensagemDeErro(), "A mensagem retornada não é o esperada.");
            #endregion

            usersDBSteps.DeletarUsuarioDB(primeiroUsuarioCriadoDB.UserId);
            usersDBSteps.DeletarUsuarioDB(segundoUsuarioCriadoDB.UserId);
        }

        [Test]
        public void EditarEmailJaExiste()
        {
            #region Inserindo dois novos usuários
            string usernameUserOne = "User_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string realnameUserOne = "Realname_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string enabledUserOne = "1";
            string cookieUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string emailUserOne = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";
            var primeiroUsuarioCriadoDB = usersDBSteps.InserirUsuarioDB(usernameUserOne, realnameUserOne, enabledUserOne, cookieUserOne, emailUserOne);

            string usernameUserTwo = "User_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string realnameUserTwo = "Realname_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string enabledUserTwo = "1";
            string cookieUserTwo = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string emailUserTwo = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";
            var segundoUsuarioCriadoDB = usersDBSteps.InserirUsuarioDB(usernameUserTwo, realnameUserTwo, enabledUserTwo, cookieUserTwo, emailUserTwo);
            #endregion

            #region Parameters
            //Resultado esperado
            string messageErrorExpected = "Este e-mail já está sendo usado. Por favor, volte e selecione outro.";
            #endregion

            #region Actions
            manageUserFlows.AcessarUsuarioCriadoAtivo(menu, usernameUserOne);
            manageUserEditPage.PreencherEmail(emailUserTwo);
            manageUserEditPage.ClicarAtualizarUsuario();
            #endregion

            #region Validations
            Assert.AreEqual(messageErrorExpected, manageUserEditPage.RetornaMensagemDeErro(), "A mensagem retornada não é o esperada.");
            #endregion

            usersDBSteps.DeletarUsuarioDB(primeiroUsuarioCriadoDB.UserId);
            usersDBSteps.DeletarUsuarioDB(segundoUsuarioCriadoDB.UserId);
        }

        [Test]
        public void RedefinirSenhaComSucesso()
        {
            #region Inserindo novo usuário
            string username = "User_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string realname = "Realname_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string enabled = "1";
            string cookie = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string email = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";

            var usuarioCriadoDB = usersDBSteps.InserirUsuarioDB(username, realname, enabled, cookie, email);
            #endregion

            #region Parameters
            //Resultado esperado
            string messageSucessExpected = "Uma solicitação de confirmação foi enviada ao endereço de e-mail do usuário selecionado. Através deste, o usuário será capaz de alterar sua senha";
            #endregion

            #region Actions
            manageUserFlows.AcessarUsuarioCriadoAtivo(menu, username);
            manageUserEditPage.ClicarRedefinirSenha();
            #endregion

            #region Validations
            StringAssert.Contains(messageSucessExpected, manageUserEditPage.RetornaMensagemDeSucesso(), "A mensagem retornada não é o esperada.");
            #endregion

            usersDBSteps.DeletarUsuarioDB(usuarioCriadoDB.UserId);
            usersDBSteps.DeletarEmailUsuarioDB(email);
        }

        [Test]
        public void ApagarUsuarioComSucesso()
        {
            #region Inserindo novo usuário
            string username = "User_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string realname = "Realname_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string enabled = "1";
            string cookie = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string email = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";

            usersDBSteps.InserirUsuarioDB(username, realname, enabled, cookie, email);
            #endregion

            #region Parameters
            //Resultado esperado
            string messageSucessExpected = "Operação realizada com sucesso.";
            #endregion

            #region Actions
            manageUserFlows.AcessarUsuarioCriadoAtivo(menu, username);
            manageUserEditPage.ClicarApagarUsuario();
            manageUserEditPage.ClicarApagarConta(username);
            #endregion

            #region Validations
            Assert.AreEqual(messageSucessExpected, manageUserEditPage.RetornaMensagemDeSucesso(), "A mensagem retornada não é o esperada.");
            #endregion
        }

        [Test]
        public void AdicionarUsuarioAoProjetoComSucesso()
        {
            #region Inserindo novo usuário
            string username = "User_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string realname = "Realname_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string enabled = "1";
            string cookie = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string email = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";

            var usuarioCriadoDB = usersDBSteps.InserirUsuarioDB(username, realname, enabled, cookie, email);
            #endregion

            #region Inserindo um novo projeto
            string projectName = "Project_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            var projetoCriadoDB = projectsDBSteps.InserirProjetoDB(projectName);
            #endregion

            #region Actions
            manageUserFlows.AcessarUsuarioCriadoAtivo(menu, username);
            manageUserEditPage.ClicarNomeProjeto(projetoCriadoDB.ProjectName);
            manageUserEditPage.ClicarAdicionarUsuario();
            #endregion

            #region Validations
            var projetoAtribuidoUsuarioDB = projectsDBSteps.ConsultarProjetoAtribuidoAoUsuarioDB(projetoCriadoDB.ProjectId, usuarioCriadoDB.UserId);
            Assert.IsNotNull(projetoAtribuidoUsuarioDB, "O projeto não foi atribuído ao usuário.");
            #endregion

            usersDBSteps.DeletarUsuarioDB(usuarioCriadoDB.UserId);
            projectsDBSteps.DeletarProjetoDB(projetoCriadoDB.ProjectId);
            projectsDBSteps.DeletarProjetoAtribuidoAoUsuarioDB(projetoCriadoDB.ProjectId, usuarioCriadoDB.UserId);
        }

        [Test]
        public void RemoverProjetoAdicionadoNoUsuario()
        {
            #region Inserindo novo usuário
            string username = "User_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string realname = "Realname_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string enabled = "1";
            string cookie = GeneralHelpers.ReturnStringWithRandomCharacters(12);
            string email = GeneralHelpers.ReturnStringWithRandomCharacters(10) + "@teste.com";

            var usuarioCriadoDB = usersDBSteps.InserirUsuarioDB(username, realname, enabled, cookie, email);
            #endregion

            #region Inserindo um novo projeto
            string projectName = "Project_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            var projetoCriadoDB = projectsDBSteps.InserirProjetoDB(projectName);
            #endregion

            #region Atribuindo o projeto criado ao usuário
            projectsDBSteps.InserirProjetoAtribuidoAoUsuarioDB(projetoCriadoDB.ProjectId, usuarioCriadoDB.UserId, usuarioCriadoDB.AccessLevel);
            #endregion

            #region Parameters
            //Resultado esperado
            string messageSucessExpected = "Operação realizada com sucesso.";
            #endregion

            #region Actions
            manageUserFlows.AcessarUsuarioCriadoAtivo(menu, username);
            manageUserEditPage.ClicarRemoverProjetoAtribuido(projetoCriadoDB.ProjectId);
            manageUserEditPage.ClicarRemoverUsuario(projetoCriadoDB.ProjectName);
            #endregion

            #region Validations
            Assert.AreEqual(messageSucessExpected, manageUserEditPage.RetornaMensagemDeSucesso(), "A mensagem retornada não é o esperada.");

            var projetoAtribuidoUsuarioDB = projectsDBSteps.ConsultarProjetoAtribuidoAoUsuarioDB(projetoCriadoDB.ProjectId, usuarioCriadoDB.UserId);
            Assert.IsNull(projetoAtribuidoUsuarioDB, "A remoção da atribuição do projeto não foi realizada.");
            #endregion

            usersDBSteps.DeletarUsuarioDB(usuarioCriadoDB.UserId);
            projectsDBSteps.DeletarProjetoDB(projetoCriadoDB.ProjectId);
        }
    }
}