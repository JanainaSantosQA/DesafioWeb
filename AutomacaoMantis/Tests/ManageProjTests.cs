using NUnit.Framework;
using AutomacaoMantis.Pages;
using AutomacaoMantis.Bases;
using AutomacaoMantis.Flows;
using AutomacaoMantis.Helpers;
using AutomacaoMantis.DBSteps.Projects;

namespace AutomacaoMantis.Tests
{
    public class ManageProjTests : TestBase
    {
        #region Pages, DBSteps and Flows Objects
        LoginFlows loginFlows;
        MainPage mainPage;
        ManageProjPage manageProjPage;
        ManageProjCatAddPage manageProjCatAddPage;
        ManageProjCatDeletePage manageProjCatDeletePage;
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
            manageProjCatAddPage = new ManageProjCatAddPage();
            manageProjCatDeletePage = new ManageProjCatDeletePage();
            projectsDBSteps = new ProjectsDBSteps();

            loginFlows.EfetuarLogin(BuilderJson.ReturnParameterAppSettings("USER_LOGIN_PADRAO"), BuilderJson.ReturnParameterAppSettings("PASSWORD_LOGIN_PADRAO"));
        }

        [Test]
        public void CriarCategoriaGlobalComSucesso()
        {
            #region Parameters       
            string categoryName = "Categoria_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            #endregion

            mainPage.ClicarMenu(menu);
            manageProjPage.PreencherCategoryName(categoryName);
            manageProjPage.ClicarAdicionarCategoria();

            var consultarCategoriaDB = projectsDBSteps.ConsultarCategoriaDB(categoryName);
            Assert.IsNotNull(consultarCategoriaDB, "A nova categoria não foi registrada.");

            projectsDBSteps.DeletarCategoriaDB(categoryName);          
        }

        [Test]
        public void CriarCategoriaNomeJaExiste()
        {
            #region Inserindo uma nova categoria      
            string categoryName = "Categoria_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            projectsDBSteps.InserirCategoriaDB(categoryName);
            #endregion

            #region Parameters    
            //Resultado esperado
            string messageErroExpected = "Uma categoria com este nome já existe.";
            #endregion

            mainPage.ClicarMenu(menu);
            manageProjPage.PreencherCategoryName(categoryName);
            manageProjPage.ClicarAdicionarCategoria();

            Assert.AreEqual(messageErroExpected, manageProjCatAddPage.RetornarMensagemDeErro(), "A mensagem retornada não é a esperada.");

            projectsDBSteps.DeletarCategoriaDB(categoryName);
        }

        [Test]
        public void ApagarCategoriaGlobalComSucesso()
        {
            #region Inserindo uma nova categoria      
            string categoryName = "Categoria_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            projectsDBSteps.InserirCategoriaDB(categoryName);
            #endregion

            #region Parameters
            //Resultado esperado
            string messageSucessExpected = "Operação realizada com sucesso";
            #endregion

            mainPage.ClicarMenu(menu);
            manageProjPage.ClicarApagarCategoria(categoryName);
            manageProjCatDeletePage.ClicarApagarCategoria(categoryName);

            StringAssert.Contains(messageSucessExpected, manageProjCatDeletePage.RetornarMensagemDeSucesso(), "A mensagem retornada não é o esperada.");

            var consultarCategoriaDB = projectsDBSteps.ConsultarCategoriaDB(categoryName);
            Assert.IsNull(consultarCategoriaDB, "A categoria não foi apagada.");
        }
    }
}