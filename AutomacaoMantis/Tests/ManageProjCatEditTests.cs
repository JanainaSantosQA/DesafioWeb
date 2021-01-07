using NUnit.Framework;
using AutomacaoMantis.Pages;
using AutomacaoMantis.Bases;
using AutomacaoMantis.Flows;
using AutomacaoMantis.Helpers;
using AutomacaoMantis.DBSteps.Projects;

namespace AutomacaoMantis.Tests
{
    public class ManageProjCatEditTests : TestBase
    {
        #region Pages, DBSteps and Flows Objects
        LoginFlows loginFlows;
        MainPage mainPage;
        ManageProjPage manageProjPage;
        ManageProjCatEditPage manageProjCatEditPage;
        ManageProjCatUpdatePage manageProjCatUpdatePage;
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
            manageProjCatEditPage = new ManageProjCatEditPage();
            manageProjCatUpdatePage = new ManageProjCatUpdatePage();
            projectsDBSteps = new ProjectsDBSteps();

            loginFlows.EfetuarLogin(BuilderJson.ReturnParameterAppSettings("USER_LOGIN_PADRAO"), BuilderJson.ReturnParameterAppSettings("PASSWORD_LOGIN_PADRAO"));
        }

        [Test]
        public void EditarCategoriaGlobalComSucesso()
        {
            #region Inserindo uma nova categoria      
            string categoryName = "Categoria_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            projectsDBSteps.InserirCategoriaDB(categoryName);
            #endregion

            #region Parameters
            string newCategoryName = "Categoria_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);

            //Resultado esperado
            string messageSucessExpected = "Operação realizada com sucesso";
            #endregion

            mainPage.ClicarMenu(menu);
            manageProjPage.ClicarEditarCategoria(categoryName);
            manageProjCatEditPage.LimparCategoryName();
            manageProjCatEditPage.PreencherCategoryName(newCategoryName);
            manageProjCatEditPage.ClicarAtualizarCategoria();

            StringAssert.Contains(messageSucessExpected, manageProjCatEditPage.RetornarMensagemDeSucesso(), "A mensagem retornada não é o esperada.");

            var consultarCategoriaDB = projectsDBSteps.ConsultarCategoriaDB(newCategoryName);
            Assert.IsNotNull(consultarCategoriaDB, "O nome da categoria não foi alterado.");

            projectsDBSteps.DeletarCategoriaDB(newCategoryName);
        }

        [Test]
        public void EditarCategoriaNomeJaExiste()
        {
            #region Inserindo uma nova categoria      
            string categoryNameOne = "Categoria_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            projectsDBSteps.InserirCategoriaDB(categoryNameOne);

            string categoryNameTwo = "Categoria_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            projectsDBSteps.InserirCategoriaDB(categoryNameTwo);
            #endregion

            #region Parameters            
            //Resultado esperado
            string messageErroExpected = "Uma categoria com este nome já existe";
            #endregion

            mainPage.ClicarMenu(menu);
            manageProjPage.ClicarEditarCategoria(categoryNameOne);
            manageProjCatEditPage.LimparCategoryName();
            manageProjCatEditPage.PreencherCategoryName(categoryNameTwo);
            manageProjCatEditPage.ClicarAtualizarCategoria();

            StringAssert.Contains(messageErroExpected, manageProjCatUpdatePage.RetornarMensagemDeErro(), "A mensagem retornada não é o esperada.");

            projectsDBSteps.DeletarCategoriaDB(categoryNameOne);
            projectsDBSteps.DeletarCategoriaDB(categoryNameTwo);
        }

        [Test]
        public void EditarCategoriaNomeEmBranco()
        {
            #region Inserindo uma nova categoria      
            string categoryName = "Categoria_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            projectsDBSteps.InserirCategoriaDB(categoryName);
            #endregion

            #region Parameters            
            //Resultado esperado
            string messageErroExpected = "Um campo necessário '' estava vazio. Por favor, verifique novamente suas entradas.";
            #endregion

            mainPage.ClicarMenu(menu);
            manageProjPage.ClicarEditarCategoria(categoryName);
            manageProjCatEditPage.LimparCategoryName();
            manageProjCatEditPage.ClicarAtualizarCategoria();

            StringAssert.Contains(messageErroExpected, manageProjCatUpdatePage.RetornarMensagemDeErro(), "A mensagem retornada não é o esperada.");

            projectsDBSteps.DeletarCategoriaDB(categoryName);
        }
    }
}