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
        MyViewPage myViewPage;
        ManageProjPage manageProjPage;
        ManageProjCreatePage manageProjCreatePage;
        ManageProjEditPage manageProjEditPage;
        ManageProjVerEditPage manageProjVerEditPage;

        ProjectsDBSteps projectsDBSteps;

        LoginFlows loginFlows;
        ManageProjFlows manageProjFlows;
        #endregion

        #region Parameters
        string menu = "menuGerenciarProjetos";
        #endregion

        [SetUp]
        public void Setup()
        {
            myViewPage = new MyViewPage();
            manageProjPage = new ManageProjPage();
            manageProjCreatePage = new ManageProjCreatePage();
            manageProjEditPage = new ManageProjEditPage();
            manageProjVerEditPage = new ManageProjVerEditPage();

            projectsDBSteps = new ProjectsDBSteps();

            loginFlows = new LoginFlows();
            manageProjFlows = new ManageProjFlows();

            loginFlows.EfetuarLogin(BuilderJson.ReturnParameterAppSettings("USER_LOGIN_PADRAO"), BuilderJson.ReturnParameterAppSettings("PASSWORD_LOGIN_PADRAO"));
        }

        [Test]
        public void CriarProjetoComSucesso()
        {
            #region Parameters       
            string projectName = "Project_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string status = "release";
            string viewState = "privado";
            string description = "Criando um novo projeto.";

            //Resultado esperado
            int statusExpected = 30;
            int viewStateExpected = 50;
            string messageSucessExpected = "Operação realizada com sucesso.";
            #endregion

            #region Actions
            myViewPage.ClicarMenu(menu);
            manageProjPage.ClicarCriarNovoProjeto();
            manageProjCreatePage.PreencherNomeProjeto(projectName);
            manageProjCreatePage.SelecionarEstadoProjeto(status);
            manageProjCreatePage.SelecionarVisibilidade(viewState);
            manageProjCreatePage.PreencherDescricao(description);
            manageProjCreatePage.ClicarAdicionarProjeto();
            #endregion

            #region Validations
            Assert.AreEqual(messageSucessExpected, manageProjCreatePage.RetornarMensagemDeSucesso(), "A mensagem retornada não é a esperada.");

            var projetoCriadoDB = projectsDBSteps.ConsultarProjetoDB(projectName);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(manageProjPage.RetornarSeONomeDoProjetoEstaSendoExibidoNaTela(projectName), "O projeto não está sendo exibido na tela.");
                Assert.AreEqual(projetoCriadoDB.ProjectName, projectName, projectName, "O nome do projeto não é o esperado.");
                Assert.AreEqual(projetoCriadoDB.ProjectStatusId, statusExpected, "O status não é o esperado.");
                Assert.AreEqual(projetoCriadoDB.ViewState, viewStateExpected, "A visualização do estado não é a esperada.");
                Assert.AreEqual(projetoCriadoDB.Description, description, "A descrição retornada não é a esperada.");
            });
            #endregion

            projectsDBSteps.DeletarProjetoDB(projetoCriadoDB.ProjectId);
        }

        [Test]
        public void CriarProjetoNomeJaExiste()
        {
            #region Inserindo um novo projeto
            string projectName = "Project_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            var projetoCriadoDB = projectsDBSteps.InserirProjetoDB(projectName);
            #endregion

            #region Parameters     
            string status = "release";
            string viewState = "privado";
            string description = "Criando um novo projeto.";

            //Resultado esperado
            string messageErrorExpected = "Um projeto com este nome já existe. Por favor, volte e entre um nome diferente.";
            #endregion

            #region Actions
            myViewPage.ClicarMenu(menu);
            manageProjPage.ClicarCriarNovoProjeto();
            manageProjCreatePage.PreencherNomeProjeto(projectName);
            manageProjCreatePage.SelecionarEstadoProjeto(status);
            manageProjCreatePage.SelecionarVisibilidade(viewState);
            manageProjCreatePage.PreencherDescricao(description);
            manageProjCreatePage.ClicarAdicionarProjeto();
            #endregion

            #region Validations
            Assert.AreEqual(messageErrorExpected, manageProjCreatePage.RetornarMensagemDeErro(), "A mensagem retornada não é a esperada.");
            #endregion

            projectsDBSteps.DeletarProjetoDB(projetoCriadoDB.ProjectId);
        }

        [Test]
        public void AlterarNomeProjetoComSucesso()
        {
            #region Inserindo um novo projeto
            string projectName = "Project_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            var projetoCriadoOneDB = projectsDBSteps.InserirProjetoDB(projectName);
            #endregion

            #region Parameters  
            //Resultado esperado
            string newProjectName = "Project_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            #endregion

            #region Actions
            manageProjFlows.AcessarProjetoCriado(menu, projectName);
            manageProjEditPage.PreencherNomeProjeto(newProjectName);
            manageProjEditPage.ClicarAtualizarProjeto();
            #endregion

            #region Validations
            var projetoCriadoDB = projectsDBSteps.ConsultarProjetoDB(newProjectName);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(projetoCriadoDB, "O nome do projeto não foi alterado.");
                Assert.IsTrue(manageProjPage.RetornarSeONomeDoProjetoEstaSendoExibidoNaTela(newProjectName), "O projeto não está sendo exibido na tela.");
            });
            #endregion

            projectsDBSteps.DeletarProjetoDB(projetoCriadoDB.ProjectId);
        }

        [Test]
        public void AlterarProjetoNomeJaExiste()
        {
            #region Inserindo um novo projeto
            string projectNameOne = "Project_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            var projetoCriadoOneDB = projectsDBSteps.InserirProjetoDB(projectNameOne);

            string projectNameTwo = "Project_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            var projetoCriadoTwoDB = projectsDBSteps.InserirProjetoDB(projectNameTwo);
            #endregion

            #region Parameters       
            //Resultado esperado
            string messageErrorExpected = "Um projeto com este nome já existe. Por favor, volte e entre um nome diferente.";
            #endregion

            #region Actions
            manageProjFlows.AcessarProjetoCriado(menu, projectNameOne);
            manageProjEditPage.PreencherNomeProjeto(projectNameTwo);
            manageProjEditPage.ClicarAtualizarProjeto();
            #endregion

            #region Validations
            StringAssert.Contains(messageErrorExpected, manageProjEditPage.RetornarMensagemDeErro(), "A mensagem retornada não é a esperada.");
            #endregion

            projectsDBSteps.DeletarProjetoDB(projetoCriadoOneDB.ProjectId);
            projectsDBSteps.DeletarProjetoDB(projetoCriadoTwoDB.ProjectId);
        }

        [Test]
        public void CriarSubProjetoComSucesso()
        {
            #region Inserindo um novo projeto
            string projectNameOne = "Project_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            var projetoCriadoOneDB = projectsDBSteps.InserirProjetoDB(projectNameOne);

            string projectNameTwo = "Project_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            var projetoCriadoTwoDB = projectsDBSteps.InserirProjetoDB(projectNameTwo);
            #endregion

            #region Parameters  
            //Resultado esperado
            string messageSucessExpected = "Operação realizada com sucesso.";
            #endregion

            #region Actions
            manageProjFlows.AcessarProjetoCriado(menu, projectNameOne);
            manageProjEditPage.SelecionarNomeProjeto(projectNameTwo);
            manageProjEditPage.ClicarAdicionarComoSubProjeto();
            #endregion

            #region Validations
            Assert.AreEqual(messageSucessExpected, manageProjEditPage.RetornarMensagemDeSucesso(), "A mensagem retornada não é a esperada.");

            var subProjetoCriadoDB = projectsDBSteps.ConsultarSubProjetoDB(projetoCriadoTwoDB.ProjectId, projetoCriadoOneDB.ProjectId);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(manageProjEditPage.RetornarSeOSubProjetoEstaSendoExibidoNaTela(projectNameTwo), "O subprojeto criado não está sendo exibido na tela.");
                Assert.IsNotNull(subProjetoCriadoDB, "O subprojeto não foi adicionado.");
            });
            #endregion

            projectsDBSteps.DeletarProjetoDB(projetoCriadoOneDB.ProjectId);
            projectsDBSteps.DeletarProjetoDB(projetoCriadoTwoDB.ProjectId);
            projectsDBSteps.DeletarSubProjetoDB(projetoCriadoTwoDB.ProjectId, projetoCriadoOneDB.ProjectId);
        }

        [Test]
        public void DesvincularSubProjetoComSucesso()
        {
            #region Inserindo um subprojeto
            string projectNameOne = "Project_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            var projetoCriadoOneDB = projectsDBSteps.InserirProjetoDB(projectNameOne);

            string projectNameTwo = "Project_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            var projetoCriadoTwoDB = projectsDBSteps.InserirProjetoDB(projectNameTwo);

            projectsDBSteps.InserirSubProjetoDB(projetoCriadoTwoDB.ProjectId, projetoCriadoOneDB.ProjectId, "1");
            #endregion

            #region Parameters  
            //Resultado esperado
            string messageSucessExpected = "Operação realizada com sucesso.";
            #endregion

            #region Actions
            manageProjFlows.AcessarProjetoCriado(menu, projectNameOne);
            manageProjEditPage.ClicarDesvincular(projetoCriadoTwoDB.ProjectId, projetoCriadoOneDB.ProjectId);
            #endregion

            #region Validations
            Assert.AreEqual(messageSucessExpected, manageProjEditPage.RetornarMensagemDeSucesso(), "A mensagem retornada não é a esperada.");

            var subProjetoCriadoDB = projectsDBSteps.ConsultarSubProjetoDB(projetoCriadoTwoDB.ProjectId, projetoCriadoOneDB.ProjectId);
            Assert.IsNull(subProjetoCriadoDB, "O subprojeto não foi desvinculado.");
            #endregion

            projectsDBSteps.DeletarProjetoDB(projetoCriadoOneDB.ProjectId);
            projectsDBSteps.DeletarProjetoDB(projetoCriadoTwoDB.ProjectId);
        }

        [Test]
        public void CriarCategoriaGlobalComSucesso()
        {
            #region Parameters       
            string categoryName = "Category_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            #endregion

            #region Actions
            myViewPage.ClicarMenu(menu);
            manageProjPage.PreencherNomeCategoria(categoryName);
            manageProjPage.ClicarAdicionarCategoria();
            #endregion

            #region Validations
            var categoriaCriadaDB = projectsDBSteps.ConsultarCategoriaDB(categoryName);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(manageProjPage.RetornarSeONomeDaCategoriaEstaSendoExibidoNaTela(categoryName), "A categoria não está sendo exibida na tela.");
                Assert.IsNotNull(categoriaCriadaDB, "A nova categoria não foi registrada.");
            });
            #endregion

            projectsDBSteps.DeletarCategoriaDB(categoryName);
        }

        [Test]
        public void CriarCategoriaNomeJaExiste()
        {
            #region Inserindo uma nova categoria      
            string categoryName = "Category_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            projectsDBSteps.InserirCategoriaDB(categoryName);
            #endregion

            #region Parameters    
            //Resultado esperado
            string messageErrorExpected = "Uma categoria com este nome já existe.";
            #endregion

            #region Actions
            myViewPage.ClicarMenu(menu);
            manageProjPage.PreencherNomeCategoria(categoryName);
            manageProjPage.ClicarAdicionarCategoria();
            #endregion

            #region Validations
            Assert.AreEqual(messageErrorExpected, manageProjPage.RetornarMensagemDeErro(), "A mensagem retornada não é a esperada.");
            #endregion

            projectsDBSteps.DeletarCategoriaDB(categoryName);
        }

        [Test]
        public void ApagarCategoriaGlobalComSucesso()
        {
            #region Inserindo uma nova categoria      
            string categoryName = "Category_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            projectsDBSteps.InserirCategoriaDB(categoryName);
            #endregion

            #region Parameters
            //Resultado esperado
            string messageSucessExpected = "Operação realizada com sucesso.";
            #endregion

            #region Actions
            myViewPage.ClicarMenu(menu);
            manageProjPage.ClicarApagarCategoria(categoryName);
            manageProjPage.ClicarApagarCategoriaConfirmacao(categoryName);
            #endregion

            #region Validations
            Assert.AreEqual(messageSucessExpected, manageProjPage.RetornarMensagemDeSucesso(), "A mensagem retornada não é a esperada.");

            var categoriaCriadaDB = projectsDBSteps.ConsultarCategoriaDB(categoryName);
            Assert.IsNull(categoriaCriadaDB, "A categoria não foi apagada.");
            #endregion
        }

        [Test]
        public void CriarVersaoProjetoComSucesso()
        {
            #region Inserindo um novo projeto
            string projectName = "Project_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            var projetoCriadoDB = projectsDBSteps.InserirProjetoDB(projectName);
            #endregion

            #region Parameters       
            string versionName = "Version_" + GeneralHelpers.ReturnStringWithRandomNumbers(2);

            //Resultado esperado
            string messageSucessExpected = "Operação realizada com sucesso.";
            #endregion

            #region Actions
            manageProjFlows.AcessarProjetoCriado(menu, projectName);
            manageProjEditPage.PreencherNomeVersao(versionName);
            manageProjEditPage.ClicarAdicionarVersao();
            #endregion

            #region Validations
            Assert.AreEqual(messageSucessExpected, manageProjEditPage.RetornarMensagemDeSucesso(), "A mensagem retornada não é a esperada.");

            var versaoProjetoCriadaDB = projectsDBSteps.ConsultarVersaoProjetoDB(versionName);
            Assert.IsNotNull(versaoProjetoCriadaDB, "A nova versão do projeto não foi criada.");
            #endregion

            projectsDBSteps.DeletarVersaoProjetoDB(versionName);
            projectsDBSteps.DeletarProjetoDB(projetoCriadoDB.ProjectId);
        }

        [Test]
        public void CriarVersaoDuplicadaProjeto()
        {
            #region Inserindo um novo projeto
            string projectName = "Project_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            var projetoCriadoDB = projectsDBSteps.InserirProjetoDB(projectName);
            #endregion

            #region Criando uma nova versão para o projeto
            string versionName = "Version_" + GeneralHelpers.ReturnStringWithRandomNumbers(2);
            projectsDBSteps.InserirVersaoProjetoDB(projetoCriadoDB.ProjectId, versionName);
            #endregion

            #region Parameters     
            //Resultado esperado
            string messageErrorExpected = "Uma versão com este nome já existe.";
            #endregion

            #region Actions
            manageProjFlows.AcessarProjetoCriado(menu, projectName);
            manageProjEditPage.PreencherNomeVersao(versionName);
            manageProjEditPage.ClicarAdicionarVersao();
            #endregion

            #region Validations
            Assert.AreEqual(messageErrorExpected, manageProjEditPage.RetornarMensagemDeErro(), "A mensagem retornada não é a esperada.");
            #endregion

            projectsDBSteps.DeletarVersaoProjetoDB(versionName);
            projectsDBSteps.DeletarProjetoDB(projetoCriadoDB.ProjectId);
        }

        [Test]
        public void AlterarNomeVersaoComSucesso()
        {
            #region Inserindo um novo projeto
            string projectName = "Project_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            var projetoCriadoDB = projectsDBSteps.InserirProjetoDB(projectName);
            #endregion

            #region Criando uma nova versão para o projeto
            string versionName = "Version_" + GeneralHelpers.ReturnStringWithRandomNumbers(2);
            projectsDBSteps.InserirVersaoProjetoDB(projetoCriadoDB.ProjectId, versionName);
            #endregion

            #region Parameters     
            string newVersionName = "Version_" + GeneralHelpers.ReturnStringWithRandomNumbers(2);

            //Resultado esperado
            string messageSucessExpected = "Operação realizada com sucesso.";
            #endregion

            #region Actions
            manageProjFlows.AcessarProjetoCriado(menu, projectName);
            manageProjEditPage.ClicarAlterarVersao(versionName);
            manageProjVerEditPage.PreencherNomeVersao(newVersionName);
            manageProjVerEditPage.ClicarAtualizarVersao();
            #endregion

            #region Validations
            Assert.AreEqual(messageSucessExpected, manageProjVerEditPage.RetornarMensagemDeSucesso(), "A mensagem retornada não é a esperada.");
            #endregion

            projectsDBSteps.DeletarVersaoProjetoDB(newVersionName);
            projectsDBSteps.DeletarProjetoDB(projetoCriadoDB.ProjectId);
        }

        [Test]
        public void AlterarVersaoNomeVazio()
        {
            #region Inserindo um novo projeto
            string projectName = "Project_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            var projetoCriadoDB = projectsDBSteps.InserirProjetoDB(projectName);
            #endregion

            #region Criando uma nova versão para o projeto
            string versionName = "Version_" + GeneralHelpers.ReturnStringWithRandomNumbers(2);
            projectsDBSteps.InserirVersaoProjetoDB(projetoCriadoDB.ProjectId, versionName);
            #endregion

            #region Parameters  
            string newVersionName = string.Empty;

            //Resultado esperado
            string messageErrorExpected = "Um campo necessário '' estava vazio.";
            #endregion

            #region Actions
            manageProjFlows.AcessarProjetoCriado(menu, projectName);
            manageProjEditPage.ClicarAlterarVersao(versionName);
            manageProjVerEditPage.PreencherNomeVersao(newVersionName);
            manageProjVerEditPage.ClicarAtualizarVersao();
            #endregion

            #region Validations
            StringAssert.Contains(messageErrorExpected, manageProjVerEditPage.RetornarMensagemDeErro(), "A mensagem retornada não é o esperada.");
            #endregion

            projectsDBSteps.DeletarVersaoProjetoDB(versionName);
            projectsDBSteps.DeletarProjetoDB(projetoCriadoDB.ProjectId);
        }

        [Test]
        public void AlterarVersaoNomeJaExiste()
        {
            #region Inserindo um novo projeto
            string projectName = "Project_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            var projetoCriadoDB = projectsDBSteps.InserirProjetoDB(projectName);
            #endregion

            #region Criando duas novas versões para o projeto
            string versionNameOne = "Version_" + GeneralHelpers.ReturnStringWithRandomNumbers(2);
            projectsDBSteps.InserirVersaoProjetoDB(projetoCriadoDB.ProjectId, versionNameOne);

            string versionNameTwo = "Version_" + GeneralHelpers.ReturnStringWithRandomNumbers(2);
            projectsDBSteps.InserirVersaoProjetoDB(projetoCriadoDB.ProjectId, versionNameTwo);
            #endregion

            #region Parameters     
            //Resultado esperado
            string messageErrorExpected = "Uma versão com este nome já existe.";
            #endregion

            #region Actions
            manageProjFlows.AcessarProjetoCriado(menu, projectName);
            manageProjEditPage.ClicarAlterarVersao(versionNameOne);
            manageProjVerEditPage.PreencherNomeVersao(versionNameTwo);
            manageProjVerEditPage.ClicarAtualizarVersao();
            #endregion

            #region Validations
            Assert.AreEqual(messageErrorExpected, manageProjVerEditPage.RetornarMensagemDeErro(), "A mensagem retornada não é a esperada.");
            #endregion

            projectsDBSteps.DeletarVersaoProjetoDB(versionNameOne);
            projectsDBSteps.DeletarVersaoProjetoDB(versionNameTwo);
            projectsDBSteps.DeletarProjetoDB(projetoCriadoDB.ProjectId);
        }

        [Test]
        public void ApagarVersaoComSucesso()
        {
            #region Inserindo um novo projeto
            string projectName = "Project_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            var projetoCriadoDB = projectsDBSteps.InserirProjetoDB(projectName);
            #endregion

            #region Criando uma nova versão para o projeto
            string versionName = "Version_" + GeneralHelpers.ReturnStringWithRandomNumbers(2);
            projectsDBSteps.InserirVersaoProjetoDB(projetoCriadoDB.ProjectId, versionName);
            #endregion

            #region Parameters   
            //Resultado esperado
            string messageSucessExpected = "Operação realizada com sucesso.";
            #endregion

            #region Actions
            manageProjFlows.AcessarProjetoCriado(menu, projectName);
            manageProjEditPage.ClicarApagarVersao(versionName);
            manageProjEditPage.ClicarApagarVersaoConfirmacao(versionName);
            #endregion

            #region Validations
            Assert.AreEqual(messageSucessExpected, manageProjEditPage.RetornarMensagemDeSucesso(), "A mensagem retornada não é a esperada.");

            var versaoProjetoCriadaDB = projectsDBSteps.ConsultarVersaoProjetoDB(versionName);
            Assert.IsNull(versaoProjetoCriadaDB, "A versão não foi excluída.");
            #endregion

            projectsDBSteps.DeletarProjetoDB(projetoCriadoDB.ProjectId);
        }
    }
}