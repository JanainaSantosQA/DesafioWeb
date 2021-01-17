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
        MyViewPage myViewPage;
        ManageProjPage manageProjPage;
        ManageProjCatEditPage manageProjCatEditPage;

        ProjectsDBSteps projectsDBSteps;

        LoginFlows loginFlows;
        #endregion

        #region Parameters
        string menu = "menuGerenciarProjetos";
        #endregion

        [SetUp]
        public void Setup()
        {
            myViewPage = new MyViewPage();
            manageProjPage = new ManageProjPage();
            manageProjCatEditPage = new ManageProjCatEditPage();

            projectsDBSteps = new ProjectsDBSteps();

            loginFlows = new LoginFlows();

            loginFlows.EfetuarLogin(BuilderJson.ReturnParameterAppSettings("USER_LOGIN_PADRAO"), BuilderJson.ReturnParameterAppSettings("PASSWORD_LOGIN_PADRAO"));
        }

        [Test]
        public void EditarCategoriaGlobalComSucesso()
        {
           #region Inserindo uma nova categoria      
            string categoryName = "Category_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            projectsDBSteps.InserirCategoriaDB(categoryName);
            #endregion

            #region Parameters
            string newCategoryName = "Category_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);

            //Resultado esperado
            string messageSucessExpected = "Operação realizada com sucesso.";
            #endregion

            #region Actions
            myViewPage.ClicarMenu(menu);
            manageProjPage.ClicarAlterarCategoria(categoryName);
            manageProjCatEditPage.PreencherNomeCategoria(newCategoryName);
            manageProjCatEditPage.ClicarAtualizarCategoria();
            #endregion

            #region Validations
            Assert.AreEqual(messageSucessExpected, manageProjCatEditPage.RetornarMensagemDeSucesso(), "A mensagem retornada não é a esperada.");

            var categoriaCriadaDB = projectsDBSteps.ConsultarCategoriaDB(newCategoryName);
            Assert.IsNotNull(categoriaCriadaDB, "O nome da categoria não foi alterado.");
            #endregion

            projectsDBSteps.DeletarCategoriaDB(newCategoryName);
        }

        [Test]
        public void EditarCategoriaNomeJaExiste()
        {
            #region Inserindo uma nova categoria      
            string categoryNameOne = "Category_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            projectsDBSteps.InserirCategoriaDB(categoryNameOne);

            string categoryNameTwo = "Category_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            projectsDBSteps.InserirCategoriaDB(categoryNameTwo);
            #endregion

            #region Parameters            
            //Resultado esperado
            string messageErrorExpected = "Uma categoria com este nome já existe";
            #endregion

            #region Actions
            myViewPage.ClicarMenu(menu);
            manageProjPage.ClicarAlterarCategoria(categoryNameOne);
            manageProjCatEditPage.PreencherNomeCategoria(categoryNameTwo);
            manageProjCatEditPage.ClicarAtualizarCategoria();
            #endregion

            #region Validations
            StringAssert.Contains(messageErrorExpected, manageProjCatEditPage.RetornarMensagemDeErro(), "A mensagem retornada não é o esperada.");
            #endregion

            projectsDBSteps.DeletarCategoriaDB(categoryNameOne);
            projectsDBSteps.DeletarCategoriaDB(categoryNameTwo);
        }

        [Test]
        public void EditarCategoriaNomeEmBranco()
        {
            #region Inserindo uma nova categoria      
            string categoryName = "Category_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            projectsDBSteps.InserirCategoriaDB(categoryName);
            #endregion

            #region Parameters            
            //Resultado esperado
            string messageErrorExpected = "Um campo necessário '' estava vazio. Por favor, verifique novamente suas entradas.";
            #endregion

            #region Actions
            myViewPage.ClicarMenu(menu);
            manageProjPage.ClicarAlterarCategoria(categoryName);
            manageProjCatEditPage.LimparNomeCategoria();
            manageProjCatEditPage.ClicarAtualizarCategoria();
            #endregion

            #region Validations
            StringAssert.Contains(messageErrorExpected, manageProjCatEditPage.RetornarMensagemDeErro(), "A mensagem retornada não é o esperada.");
            #endregion

            projectsDBSteps.DeletarCategoriaDB(categoryName);
        }
    }
}