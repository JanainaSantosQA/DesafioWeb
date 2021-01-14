using NUnit.Framework;
using AutomacaoMantis.Pages;
using AutomacaoMantis.Bases;
using AutomacaoMantis.Flows;
using AutomacaoMantis.Helpers;
using AutomacaoMantis.DBSteps.Projects;

namespace AutomacaoMantis.Tests
{
    public class ManageProjEditTests : TestBase
    {
        #region Pages, DBSteps and Flows Objects
        LoginFlows loginFlows;
        MainPage mainPage;
        ManageProjEditPage manageProjEditPage;
        ManageProjPage manageProjPage;
        ManageProjVerEditPage manageProjVerEditPage;

        ProjectsDBSteps projectsDBSteps;
        #endregion

        #region Parameters
        string menu = "menuGerenciarProjetos";
        #endregion

        [SetUp]
        public void Setup()
        {
            loginFlows = new LoginFlows();
            mainPage = new MainPage();
            manageProjEditPage = new ManageProjEditPage();
            manageProjPage = new ManageProjPage();
            manageProjVerEditPage = new ManageProjVerEditPage();

            projectsDBSteps = new ProjectsDBSteps();

            loginFlows.EfetuarLogin(BuilderJson.ReturnParameterAppSettings("USER_LOGIN_PADRAO"), BuilderJson.ReturnParameterAppSettings("PASSWORD_LOGIN_PADRAO"));
        }

        [Test]
        public void CriarSubProjetoComSucesso()
        {                
            #region Inserindo um novo projeto
            string projectNameOne = "Projeto_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            var projetoCriadoOneDB = projectsDBSteps.InserirProjetoDB(projectNameOne);

            string projectNameTwo = "Projeto_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            var projetoCriadoTwoDB = projectsDBSteps.InserirProjetoDB(projectNameTwo);
            #endregion

            #region Parameters  
            //Resultado esperado
            string messageSucessExpected = "Operação realizada com sucesso.";
            #endregion
        
            mainPage.ClicarMenu(menu);
            manageProjPage.ClicarProjectName(projectNameOne);
            manageProjEditPage.SelecionarProjectName(projectNameTwo);
            manageProjEditPage.ClicarAdicionarComoSubprojeto();            
           
            Assert.AreEqual(messageSucessExpected, manageProjEditPage.RetornarMensagemDeSucesso(), "A mensagem retornada não é a esperada.");
            manageProjEditPage.VerificarSeOSubprojetoEstaSendoExibidoNaTela(projectNameTwo);

            var consultaSubProjetoDB = projectsDBSteps.ConsultarSubProjetoDB(projetoCriadoTwoDB.ProjectId, projetoCriadoOneDB.ProjectId);
            Assert.IsNotNull(consultaSubProjetoDB, "O subprojeto não foi adicionado.");            

            projectsDBSteps.DeletarProjetoDB(projetoCriadoOneDB.ProjectId);
            projectsDBSteps.DeletarProjetoDB(projetoCriadoTwoDB.ProjectId);
            projectsDBSteps.DeletarSubProjetoDB(projetoCriadoTwoDB.ProjectId, projetoCriadoOneDB.ProjectId);
        }

        [Test]
        public void DesvincularSubProjetoComSucesso()
        {
            #region Inserindo um subprojeto
            string projectNameOne = "Projeto_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            var projetoCriadoOneDB = projectsDBSteps.InserirProjetoDB(projectNameOne);

            string projectNameTwo = "Projeto_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            var projetoCriadoTwoDB = projectsDBSteps.InserirProjetoDB(projectNameTwo);

            projectsDBSteps.InserirSubProjetoDB(projetoCriadoTwoDB.ProjectId, projetoCriadoOneDB.ProjectId, "1") ;
            #endregion

            #region Parameters  
            //Resultado esperado
            string messageSucessExpected = "Operação realizada com sucesso.";
            #endregion

            mainPage.ClicarMenu(menu);
            manageProjPage.ClicarProjectName(projectNameOne);
            manageProjEditPage.ClicarDesvincular(projetoCriadoTwoDB.ProjectId, projetoCriadoOneDB.ProjectId);

            Assert.AreEqual(messageSucessExpected, manageProjEditPage.RetornarMensagemDeSucesso(), "A mensagem retornada não é a esperada.");
  
            var consultaSubProjetoDB = projectsDBSteps.ConsultarSubProjetoDB(projetoCriadoTwoDB.ProjectId, projetoCriadoOneDB.ProjectId);
            Assert.IsNull(consultaSubProjetoDB, "O subprojeto não foi desvinculado.");

            projectsDBSteps.DeletarProjetoDB(projetoCriadoOneDB.ProjectId);
            projectsDBSteps.DeletarProjetoDB(projetoCriadoTwoDB.ProjectId);            
        }

        [Test]
        public void CriarVersaoProjetoComSucesso()
        {
            #region Inserindo um novo projeto
            string projectName = "Projeto_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            var projetoCriadoDB = projectsDBSteps.InserirProjetoDB(projectName);
            #endregion

            #region Parameters       
            string versionName = "Versao_" + GeneralHelpers.ReturnStringWithRandomNumbers(2);

            //Resultado esperado
            string messageSucessExpected = "Operação realizada com sucesso.";
            #endregion

            mainPage.ClicarMenu(menu);
            manageProjPage.ClicarProjectName(projectName);
            manageProjEditPage.PreencherVersionName(versionName);
            manageProjEditPage.ClicarAdicionarVersao();
                
            Assert.AreEqual(messageSucessExpected, manageProjEditPage.RetornarMensagemDeSucesso(), "A mensagem retornada não é a esperada.");

            var consultaVersaoProjetoDB = projectsDBSteps.ConsultarVersaoProjetoDB(versionName);
            Assert.IsNotNull(consultaVersaoProjetoDB, "A nova versão do projeto não foi criada.");

            projectsDBSteps.DeletarVersaoProjetoDB(versionName);
            projectsDBSteps.DeletarProjetoDB(projetoCriadoDB.ProjectId);
        }

        [Test]
        public void CriarVersaoDuplicadaProjeto()
        {
            #region Inserindo um novo projeto
            string projectName = "Projeto_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            var projetoCriadoDB = projectsDBSteps.InserirProjetoDB(projectName);
            #endregion

            #region Criando uma nova versão para o projeto
            string versionName = "Versao_" + GeneralHelpers.ReturnStringWithRandomNumbers(2);
            projectsDBSteps.InserirVersaoProjetoDB(projetoCriadoDB.ProjectId, versionName);
            #endregion

            #region Parameters     
            //Resultado esperado
            string messageErrorExpected = "Uma versão com este nome já existe.";
            #endregion

            mainPage.ClicarMenu(menu);
            manageProjPage.ClicarProjectName(projectName);
            manageProjEditPage.PreencherVersionName(versionName);
            manageProjEditPage.ClicarAdicionarVersao();

            Assert.AreEqual(messageErrorExpected, manageProjEditPage.RetornarMensagemDeErro(), "A mensagem retornada não é a esperada.");
            
            projectsDBSteps.DeletarVersaoProjetoDB(versionName);
            projectsDBSteps.DeletarProjetoDB(projetoCriadoDB.ProjectId);
        }

        [Test]
        public void AlterarNomeVersaoComSucesso()
        {
            #region Inserindo um novo projeto
            string projectName = "Projeto_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            var projetoCriadoDB = projectsDBSteps.InserirProjetoDB(projectName);
            #endregion

            #region Criando uma nova versão para o projeto
            string versionName = "Versao_" + GeneralHelpers.ReturnStringWithRandomNumbers(2);
            projectsDBSteps.InserirVersaoProjetoDB(projetoCriadoDB.ProjectId, versionName);
            #endregion

            #region Parameters     
            string newVersionName = "Versao_" + GeneralHelpers.ReturnStringWithRandomNumbers(2);

            //Resultado esperado
            string messageSucessExpected = "Operação realizada com sucesso.";
            #endregion

            mainPage.ClicarMenu(menu);
            manageProjPage.ClicarProjectName(projectName);
            manageProjEditPage.ClicarAlterarVersao(versionName);
            manageProjVerEditPage.PreencherVersionName(newVersionName);
            manageProjVerEditPage.ClicarAtualizarVersao();

            Assert.AreEqual(messageSucessExpected, manageProjVerEditPage.RetornarMensagemDeSucesso(), "A mensagem retornada não é a esperada.");

            projectsDBSteps.DeletarVersaoProjetoDB(newVersionName);
            projectsDBSteps.DeletarProjetoDB(projetoCriadoDB.ProjectId);
        }

        [Test]
        public void AlterarVersaoNomeVazio()
        {
            #region Inserindo um novo projeto
            string projectName = "Projeto_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            var projetoCriadoDB = projectsDBSteps.InserirProjetoDB(projectName);
            #endregion

            #region Criando uma nova versão para o projeto
            string versionName = "Versao_" + GeneralHelpers.ReturnStringWithRandomNumbers(2);
            projectsDBSteps.InserirVersaoProjetoDB(projetoCriadoDB.ProjectId, versionName);
            #endregion

            #region Parameters  
            //Resultado esperado
            string messageErrorExpected = "Um campo necessário '' estava vazio.";
            #endregion

            mainPage.ClicarMenu(menu);
            manageProjPage.ClicarProjectName(projectName);
            manageProjEditPage.ClicarAlterarVersao(versionName);
            manageProjVerEditPage.PreencherVersionName(string.Empty);
            manageProjVerEditPage.ClicarAtualizarVersao();

            StringAssert.Contains(messageErrorExpected, manageProjVerEditPage.RetornarMensagemDeErro(), "A mensagem retornada não é o esperada.");

            projectsDBSteps.DeletarVersaoProjetoDB(versionName);
            projectsDBSteps.DeletarProjetoDB(projetoCriadoDB.ProjectId);
        }

        [Test]
        public void ApagarVersaoComSucesso()
        {
            #region Inserindo um novo projeto
            string projectName = "Projeto_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            var projetoCriadoDB = projectsDBSteps.InserirProjetoDB(projectName);
            #endregion

            #region Criando uma nova versão para o projeto
            string versionName = "Versao_" + GeneralHelpers.ReturnStringWithRandomNumbers(2);
            projectsDBSteps.InserirVersaoProjetoDB(projetoCriadoDB.ProjectId, versionName);
            #endregion

            #region Parameters   
            //Resultado esperado
            string messageSucessExpected = "Operação realizada com sucesso.";
            #endregion

            mainPage.ClicarMenu(menu);
            manageProjPage.ClicarProjectName(projectName);
            manageProjEditPage.ClicarApagarVersao(versionName);
            manageProjEditPage.ClicarApagarVersaoConfirmacao(versionName);

            Assert.AreEqual(messageSucessExpected, manageProjVerEditPage.RetornarMensagemDeSucesso(), "A mensagem retornada não é a esperada.");

            var consultaVersaoProjetoDB = projectsDBSteps.ConsultarVersaoProjetoDB(versionName);
            Assert.IsNull(consultaVersaoProjetoDB, "A versão não foi excluída.");

            projectsDBSteps.DeletarProjetoDB(projetoCriadoDB.ProjectId);
        }
    }
}