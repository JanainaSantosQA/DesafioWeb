using NUnit.Framework;
using AutomacaoMantis.Pages;
using AutomacaoMantis.Bases;
using AutomacaoMantis.Flows;
using AutomacaoMantis.Helpers;
using AutomacaoMantis.DBSteps.Projects;

namespace AutomacaoMantis.Tests
{
    public class ManageProjCreateTests : TestBase
    {
        #region Pages, DBSteps and Flows Objects
        LoginFlows loginFlows;  
        MainPage mainPage;
        ManageProjPage manageProjPage;
        ManageProjCreatePage manageProjCreatePage;
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
            manageProjPage = new ManageProjPage();
            manageProjCreatePage = new ManageProjCreatePage();
            projectsDBSteps = new ProjectsDBSteps();

            loginFlows.EfetuarLogin(BuilderJson.ReturnParameterAppSettings("USER_LOGIN_PADRAO"), BuilderJson.ReturnParameterAppSettings("PASSWORD_LOGIN_PADRAO"));
        }

        [Test]
        public void CriarProjetoComSucesso()
        {
            #region Parameters       
            string projectName = "Projeto_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string status = "release";
            string viewState = "privado";
            string description = "Criando um novo projeto.";

            //Resultado esperado
            int statusExpected = 30;
            int viewStateExpected = 50;
            string messageSucessExpected = "Operação realizada com sucesso.";
            #endregion

            mainPage.ClicarMenu(menu);
            manageProjPage.ClicarCriarNovoProjeto();
            manageProjCreatePage.PreencherProjectName(projectName);
            manageProjCreatePage.SelecionarStatus(status);
            manageProjCreatePage.SelecionarViewState(viewState);
            manageProjCreatePage.PreencherDescription(description);
            manageProjCreatePage.ClicarAdicionarProjeto();
   
            Assert.AreEqual(messageSucessExpected, manageProjCreatePage.RetornarMensagemDeSucesso(), "A mensagem retornada não é a esperada.");
                        
            var consultaProjectDB = projectsDBSteps.ConsultarProjetoDB(projectName);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(consultaProjectDB.ProjectName, projectName, projectName, "O nome do projeto não é o esperado.");
                Assert.AreEqual(consultaProjectDB.ProjectStatusId, statusExpected, "O status não é o esperado.");
                Assert.AreEqual(consultaProjectDB.ViewState, viewStateExpected, "A visualização do estado não é a esperada.");
                Assert.AreEqual(consultaProjectDB.Description, description, "A descrição retornada não é a esperada.");
            });

            projectsDBSteps.DeletarProjetoDB(consultaProjectDB.ProjectId);
        }

        [Test]
        public void CriarProjetoNomeJaExiste()
        {
            #region Inserindo um novo projeto
            string projectName = "Projeto_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            var projetoCriadoDB = projectsDBSteps.InserirProjetoDB(projectName);
            #endregion

            #region Parameters     
            string status = "release";
            string viewState = "privado";
            string description = "Criando um novo projeto.";

            //Resultado esperado
            string messageErroExpected = "Um projeto com este nome já existe. Por favor, volte e entre um nome diferente.";
            #endregion

            mainPage.ClicarMenu(menu);
            manageProjPage.ClicarCriarNovoProjeto();
            manageProjCreatePage.PreencherProjectName(projectName);
            manageProjCreatePage.SelecionarStatus(status);
            manageProjCreatePage.SelecionarViewState(viewState);
            manageProjCreatePage.PreencherDescription(description);
            manageProjCreatePage.ClicarAdicionarProjeto();

            Assert.AreEqual(messageErroExpected, manageProjCreatePage.RetornarMensagemDeErro(), "A mensagem retornada não é a esperada.");

            projectsDBSteps.DeletarProjetoDB(projetoCriadoDB.ProjectId);
        }
    }
}