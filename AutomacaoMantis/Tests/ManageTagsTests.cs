using NUnit.Framework;
using AutomacaoMantis.Pages;
using AutomacaoMantis.Bases;
using AutomacaoMantis.Flows;
using AutomacaoMantis.Helpers;
using AutomacaoMantis.DBSteps.Tags;

namespace AutomacaoMantis.Tests
{
    public class ManageTagsTests : TestBase
    {
        #region Pages, DBSteps and Flows Objects
        LoginFlows loginFlows;
        MainPage mainPage;
        ManageTagsPage manageTagsPage;
        TagViewPage tagViewPage;
        TagDeletePage tagDeletePage;
        TagUpdatePage tagUpdatePage;

        TagsDBSteps tagsDBSteps;
        #endregion

        #region Parameters
        string menu = "menuGerenciarMarcadores";
        #endregion

        [SetUp]
        public void Setup()
        {
            loginFlows = new LoginFlows();
            mainPage = new MainPage();
            manageTagsPage = new ManageTagsPage();
            tagDeletePage = new TagDeletePage();
            tagViewPage = new TagViewPage();
            tagUpdatePage = new TagUpdatePage();

            tagsDBSteps = new TagsDBSteps();

            loginFlows.EfetuarLogin(BuilderJson.ReturnParameterAppSettings("USER_LOGIN_PADRAO"), BuilderJson.ReturnParameterAppSettings("PASSWORD_LOGIN_PADRAO"));
        }

        [Test]
        public void CriarTagComSucesso()
        {
            #region Parameters
            string tagName = "Tag_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string tagDescription = GeneralHelpers.ReturnStringWithRandomCharacters(5);
            #endregion

            mainPage.ClicarMenu(menu);
            manageTagsPage.PreencherNomeMarcador(tagName);
            manageTagsPage.PreencherDescricaoMarcador(tagDescription);
            manageTagsPage.ClicarCriarMarcador();
            manageTagsPage.VerificarSeATagCriadaEstaSendoExibidaNaTela(tagName); 

            var consultarTagDB = tagsDBSteps.ConsultarTagDB(tagName);
            Assert.AreEqual(tagDescription, consultarTagDB.TagDescription, "A descrição da tag não está correta.");

            tagsDBSteps.DeletarTagDB(tagName);            
        }

        [Test]
        public void ApagarTagComSucesso()
        {
            #region Inserindo uma nova tag
            string tagName = "Tag_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string tagDescription = GeneralHelpers.ReturnStringWithRandomCharacters(5);

            tagsDBSteps.InserirTagDB(tagName, tagDescription);
            #endregion

            mainPage.ClicarMenu(menu);
            manageTagsPage.ClicarMarcador(tagName);
            tagViewPage.ClicarApagarMarcador();
            tagDeletePage.ClicarApagarMarcador();

            var consultarTagDB = tagsDBSteps.ConsultarTagDB(tagName);
            Assert.IsNull(consultarTagDB, "A tag não foi excluída.");
        }

        [Test]
        public void EditarTagComSucesso()
        {
            #region Inserindo uma nova tag
            string tagName = "Tag_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string tagDescription = GeneralHelpers.ReturnStringWithRandomCharacters(5);

            tagsDBSteps.InserirTagDB(tagName, tagDescription);
            #endregion

            #region Parameters
            string newTagName = "Tag_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            #endregion

            mainPage.ClicarMenu(menu);
            manageTagsPage.ClicarMarcador(tagName);
            tagViewPage.ClicarAtualizarMarcador();
            tagUpdatePage.LimparTagName();
            tagUpdatePage.PreencherNomeMarcador(newTagName);
            tagUpdatePage.ClicarAtualizarMarcador();

            var consultarTagDB = tagsDBSteps.ConsultarTagDB(newTagName);
            Assert.IsNotNull(consultarTagDB, "O nome da tag não foi alterado.");

            tagsDBSteps.DeletarTagDB(newTagName);
        }

        [Test]
        public void EditarTagNomeJaExiste()
        {
            #region Inserindo uma nova tag
            string tagNameOne = "Tag_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string tagDescriptionOne = GeneralHelpers.ReturnStringWithRandomCharacters(5);

            tagsDBSteps.InserirTagDB(tagNameOne, tagDescriptionOne);

            string tagNameTwo = "Tag_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string tagDescriptionTwo = GeneralHelpers.ReturnStringWithRandomCharacters(5);

            tagsDBSteps.InserirTagDB(tagNameTwo, tagDescriptionTwo);
            #endregion

            #region Parameters            
            //Resultado esperado
            string messageErroExpected = "Um marcador com esse nome já existe.";
            #endregion

            mainPage.ClicarMenu(menu);
            manageTagsPage.ClicarMarcador(tagNameOne);
            tagViewPage.ClicarAtualizarMarcador();
            tagUpdatePage.LimparTagName();
            tagUpdatePage.PreencherNomeMarcador(tagNameTwo);
            tagUpdatePage.ClicarAtualizarMarcador();

            StringAssert.Contains(messageErroExpected, tagUpdatePage.RetornarMensagemDeErro(), "A mensagem retornada não é o esperada.");

            tagsDBSteps.DeletarTagDB(tagNameOne);
            tagsDBSteps.DeletarTagDB(tagNameTwo);
        }

        [Test]
        public void EditarTagNomeEmBranco()
        {
            #region Inserindo uma nova tag
            string tagName = "Tag_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string tagDescription = GeneralHelpers.ReturnStringWithRandomCharacters(5);

            tagsDBSteps.InserirTagDB(tagName, tagDescription);
            #endregion

            #region Parameters            
            //Resultado esperado
            string messageErroExpected = "Nome de marcador não é válido.";
            #endregion

            mainPage.ClicarMenu(menu);
            manageTagsPage.ClicarMarcador(tagName);
            tagViewPage.ClicarAtualizarMarcador();
            tagUpdatePage.LimparTagName();
            tagUpdatePage.ClicarAtualizarMarcador();

            StringAssert.Contains(messageErroExpected, tagUpdatePage.RetornarMensagemDeErro(), "A mensagem retornada não é o esperada.");

            tagsDBSteps.DeletarTagDB(tagName);
        }
    }
}