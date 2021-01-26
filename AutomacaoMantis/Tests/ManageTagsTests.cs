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
        MyViewPage mainPage;
        ManageTagsPage manageTagsPage;
        TagUpdatePage tagUpdatePage;
        TagViewPage tagViewPage;

        TagsDBSteps tagsDBSteps;

        LoginFlows loginFlows;
        ManageTagsFlows manageTagsFlows;
        #endregion

        #region Parameters
        string menu = "menuGerenciarMarcadores";
        #endregion

        [SetUp]
        public void Setup()
        {
            mainPage = new MyViewPage();
            manageTagsPage = new ManageTagsPage();
            tagUpdatePage = new TagUpdatePage();
            tagViewPage = new TagViewPage();

            tagsDBSteps = new TagsDBSteps();

            loginFlows = new LoginFlows();
            manageTagsFlows = new ManageTagsFlows();

            loginFlows.EfetuarLogin(BuilderJson.ReturnParameterAppSettings("USER_LOGIN_PADRAO"), BuilderJson.ReturnParameterAppSettings("PASSWORD_LOGIN_PADRAO"));
        }

        [Test]
        public void CriarTagComSucesso()
        {
            #region Parameters
            string tagName = "Tag_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string tagDescription = GeneralHelpers.ReturnStringWithRandomCharacters(5);
            #endregion

            #region Actions
            mainPage.ClicarMenu(menu);
            manageTagsPage.PreencherNomeMarcador(tagName);
            manageTagsPage.PreencherDescricaoMarcador(tagDescription);
            manageTagsPage.ClicarCriarMarcador();
            #endregion

            #region Validations
            var consultarTagCriadaDB = tagsDBSteps.ConsultarTagDB(tagName);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(manageTagsPage.RetornaSeATagCriadaEstaSendoExibidaNaTela(tagName), "A tag criada não está sendo exibida na tela.");
                Assert.AreEqual(tagDescription, consultarTagCriadaDB.TagDescription, "A descrição da tag não está correta.");
            });
            #endregion

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

            #region Actions
            manageTagsFlows.AcessarMarcadorCriado(menu, tagName);
            tagViewPage.ClicarApagarMarcador();
            tagViewPage.ClicarApagarMarcadorConfirmacao();
            #endregion

            #region Validations
            var consultarTagCriadaDB = tagsDBSteps.ConsultarTagDB(tagName);
            Assert.IsNull(consultarTagCriadaDB, "A tag não foi excluída.");
            #endregion
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

            #region Actions
            manageTagsFlows.AcessarMarcadorCriado(menu, tagName);
            tagViewPage.ClicarAtualizarMarcador();
            tagUpdatePage.PreencherNomeMarcador(newTagName);
            tagUpdatePage.ClicarAtualizarMarcador();
            #endregion

            #region Validations
            var consultarTagCriadaDB = tagsDBSteps.ConsultarTagDB(newTagName);
            Assert.IsNotNull(consultarTagCriadaDB, "O nome da tag não foi alterado.");
            #endregion

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
            string messageErrorExpected = "Um marcador com esse nome já existe.";
            #endregion

            #region Actions
            manageTagsFlows.AcessarMarcadorCriado(menu, tagNameOne);
            tagViewPage.ClicarAtualizarMarcador();
            tagUpdatePage.PreencherNomeMarcador(tagNameTwo);
            tagUpdatePage.ClicarAtualizarMarcador();
            #endregion

            #region Validations
            StringAssert.Contains(messageErrorExpected, tagUpdatePage.RetornaMensagemDeErro(), "A mensagem retornada não é o esperada.");
            #endregion

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
            string newTagName = string.Empty;

            //Resultado esperado
            string messageErrorExpected = "Nome de marcador não é válido.";
            #endregion

            #region Actions
            manageTagsFlows.AcessarMarcadorCriado(menu, tagName);
            tagViewPage.ClicarAtualizarMarcador();
            tagUpdatePage.PreencherNomeMarcador(newTagName);
            tagUpdatePage.ClicarAtualizarMarcador();
            #endregion

            #region Validations
            StringAssert.Contains(messageErrorExpected, tagUpdatePage.RetornaMensagemDeErro(), "A mensagem retornada não é o esperada.");
            #endregion

            tagsDBSteps.DeletarTagDB(tagName);
        }
    }
}