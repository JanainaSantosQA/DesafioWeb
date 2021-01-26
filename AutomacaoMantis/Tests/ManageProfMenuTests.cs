using NUnit.Framework;
using AutomacaoMantis.Bases;
using AutomacaoMantis.Pages;
using AutomacaoMantis.Flows;
using AutomacaoMantis.Helpers;
using AutomacaoMantis.DBSteps.Users;

namespace AutomacaoMantis.Tests
{
    public class ManageProfMenuTests : TestBase
    {
        #region Pages, DBSteps and Flows Objects
        MyViewPage myViewPage;
        AccountProfEditPage accountProfEditPage;
        ManageProfMenuPage manageProfMenuPage;

        UsersDBSteps usersDBSteps;

        LoginFlows loginFlows;
        #endregion

        #region Parameters
        string menu = "menuGerenciarPerfisGlobais";
        #endregion

        [SetUp]
        public void Setup()
        {
            myViewPage = new MyViewPage();
            accountProfEditPage = new AccountProfEditPage();
            manageProfMenuPage = new ManageProfMenuPage();

            usersDBSteps = new UsersDBSteps();

            loginFlows = new LoginFlows();

            loginFlows.EfetuarLogin(BuilderJson.ReturnParameterAppSettings("USER_LOGIN_PADRAO"), BuilderJson.ReturnParameterAppSettings("PASSWORD_LOGIN_PADRAO"));
        }

        [Test]
        public void AdicionarPerfilComSucesso()
        {
            #region Parameters
            string platform = "Platform_" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string os = "OS_" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string osVersion = GeneralHelpers.ReturnStringWithRandomNumbers(3);
            #endregion

            #region Actions
            myViewPage.ClicarMenu(menu);
            manageProfMenuPage.PreencherPlataforma(platform);
            manageProfMenuPage.PreencherSO(os);
            manageProfMenuPage.PreencherVersaoSO(osVersion);
            manageProfMenuPage.ClicarAdicionarPerfil();
            #endregion

            #region Validations
            var perfilCriadoDB = usersDBSteps.ConsultarPerfilUsuarioDB(platform, os, osVersion);
            Assert.IsNotNull(perfilCriadoDB, "O perfil não foi criado.");
            #endregion

            usersDBSteps.DeletarPerfilUsuarioDB(platform, os, osVersion);
        }

        [Test]
        public void ApagarPerfilComSucesso()
        {
            #region Inserindo um novo perfil
            string platform = "Platform_" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string os = "OS_" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string osVersion = GeneralHelpers.ReturnStringWithRandomNumbers(3);
            string description = GeneralHelpers.ReturnStringWithRandomNumbers(6);

            usersDBSteps.InserirPerfilUsuarioDB(platform, os, osVersion, description);
            #endregion

            #region Parameters
            string profile = platform + " " + os + " " + osVersion;
            #endregion

            #region Actions
            myViewPage.ClicarMenu(menu);
            manageProfMenuPage.ClicarApagarPerfil();
            manageProfMenuPage.SelecionarPerfil(profile);
            manageProfMenuPage.ClicarEnviar();
            #endregion

            #region Validations
            var perfilCriadoDB = usersDBSteps.ConsultarPerfilUsuarioDB(platform, os, osVersion);
            Assert.IsNull(perfilCriadoDB, "O perfil não foi excluído.");
            #endregion
        }

        [Test]
        public void EditarPlataformaComSucesso()
        {
            #region Inserindo um novo perfil
            string platform = "Platform_" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string os = "OS_" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string osVersion = GeneralHelpers.ReturnStringWithRandomNumbers(3);
            string description = GeneralHelpers.ReturnStringWithRandomNumbers(6);

            usersDBSteps.InserirPerfilUsuarioDB(platform, os, osVersion, description);
            #endregion

            #region Parameters
            string profile = platform + " " + os + " " + osVersion;

            //Resultado esperado
            string newPlatform = "Platform_" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            #endregion

            #region Actions
            myViewPage.ClicarMenu(menu);
            manageProfMenuPage.ClicarEditarPerfil();
            manageProfMenuPage.SelecionarPerfil(profile);
            manageProfMenuPage.ClicarEnviar();
            accountProfEditPage.PreencherPlataforma(newPlatform);
            accountProfEditPage.ClicarAtualizarPerfil();
            #endregion

            #region Validations
            Assert.IsTrue(manageProfMenuPage.RetornaSeOPerfilEstaSendoExibidoNaTela(newPlatform + " " + os + " " + osVersion), "O perfil com a plataforma atualizada não está sendo exibido na tela.");

            var perfilCriadoDB = usersDBSteps.ConsultarPerfilUsuarioDB(newPlatform, os, osVersion);
            Assert.IsNotNull(perfilCriadoDB, "A platform não foi alterada.");
            #endregion

            usersDBSteps.DeletarPerfilUsuarioDB(newPlatform, os, osVersion);
        }

        [Test]
        public void EditarSOComSucesso()
        {
            #region Inserindo um novo perfil
            string platform = "Platform_" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string os = "OS_" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string osVersion = GeneralHelpers.ReturnStringWithRandomNumbers(3);
            string description = GeneralHelpers.ReturnStringWithRandomNumbers(6);

            usersDBSteps.InserirPerfilUsuarioDB(platform, os, osVersion, description);
            #endregion

            #region Parameters
            string profile = platform + " " + os + " " + osVersion;

            //Resultado esperado
            string newOs = "OS_" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            #endregion

            #region Actions
            myViewPage.ClicarMenu(menu);
            manageProfMenuPage.ClicarEditarPerfil();
            manageProfMenuPage.SelecionarPerfil(profile);
            manageProfMenuPage.ClicarEnviar();
            accountProfEditPage.PreencherSO(newOs);
            accountProfEditPage.ClicarAtualizarPerfil();
            #endregion

            #region Validations
            Assert.IsTrue(manageProfMenuPage.RetornaSeOPerfilEstaSendoExibidoNaTela(platform + " " + newOs + " " + osVersion), "O perfil com o SO atualizado não está sendo exibido na tela.");

            var perfilCriadoDB = usersDBSteps.ConsultarPerfilUsuarioDB(platform, newOs, osVersion);
            Assert.IsNotNull(perfilCriadoDB, "O SO não foi alterado.");
            #endregion

            usersDBSteps.DeletarPerfilUsuarioDB(platform, newOs, osVersion);
        }

        [Test]
        public void EditarVersaoSOComSucesso()
        {
            #region Inserindo um novo perfil
            string platform = "Platform_" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string os = "OS_" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string osVersion = GeneralHelpers.ReturnStringWithRandomNumbers(3);
            string description = GeneralHelpers.ReturnStringWithRandomNumbers(6);

            usersDBSteps.InserirPerfilUsuarioDB(platform, os, osVersion, description);
            #endregion

            #region Parameters
            string profile = platform + " " + os + " " + osVersion;

            //Resultado esperado
            string newOSVersion = GeneralHelpers.ReturnStringWithRandomNumbers(3);
            #endregion

            #region Actions
            myViewPage.ClicarMenu(menu);
            manageProfMenuPage.ClicarEditarPerfil();
            manageProfMenuPage.SelecionarPerfil(profile);
            manageProfMenuPage.ClicarEnviar();
            accountProfEditPage.PreencherVersaoSO(newOSVersion);
            accountProfEditPage.ClicarAtualizarPerfil();
            #endregion

            #region Validations
            Assert.IsTrue(manageProfMenuPage.RetornaSeOPerfilEstaSendoExibidoNaTela(platform + " " + os + " " + newOSVersion), "O perfil com a versão SO atualizada não está sendo exibido na tela.");

            var perfilCriadoDB = usersDBSteps.ConsultarPerfilUsuarioDB(platform, os, newOSVersion);
            Assert.IsNotNull(perfilCriadoDB, "A versão SO não foi alterada.");
            #endregion

            usersDBSteps.DeletarPerfilUsuarioDB(platform, os, newOSVersion);
        }

        [Test]
        public void ClicarNoEnviarSemSelecionarUmaAcao()
        {
            #region Inserindo um novo perfil
            string platform = "Platform_" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string os = "OS_" + GeneralHelpers.ReturnStringWithRandomCharacters(3);
            string osVersion = GeneralHelpers.ReturnStringWithRandomNumbers(3);
            string description = GeneralHelpers.ReturnStringWithRandomNumbers(6);

            usersDBSteps.InserirPerfilUsuarioDB(platform, os, osVersion, description);
            #endregion

            #region Parameters
            string profile = platform + " " + os + " " + osVersion;

            //Resultado esperado
            string messageErrorExpected = "Um parâmetro necessário para esta página (action) não foi encontrado.";
            #endregion

            #region Actions
            myViewPage.ClicarMenu(menu);
            manageProfMenuPage.SelecionarPerfil(profile);
            manageProfMenuPage.ClicarEnviar();
            #endregion

            #region Validations
            StringAssert.Contains(messageErrorExpected, manageProfMenuPage.RetornaMensagemDeErro(), "A mensagem retornada não é o esperada."); ;
            #endregion

            usersDBSteps.DeletarPerfilUsuarioDB(platform, os, osVersion);
        }
    }
}