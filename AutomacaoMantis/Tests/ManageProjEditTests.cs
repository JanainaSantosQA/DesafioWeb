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
    }
}