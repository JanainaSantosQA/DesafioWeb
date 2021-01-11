using NUnit.Framework;
using AutomacaoMantis.Pages;
using AutomacaoMantis.Bases;
using AutomacaoMantis.Flows;
using AutomacaoMantis.Helpers;
using AutomacaoMantis.DBSteps.CustomField;

namespace AutomacaoMantis.Tests
{
    public class ManageCustomFieldTests : TestBase
    {
        #region Pages, DBSteps and Flows Objects
        LoginFlows loginFlows;
        MainPage mainPage;
        ManageCustomFieldPage manageCustomFieldPage;
        ManageCustomFieldCreatePage manageCustomFieldCreatePage;
        ManageCustomFieldEditPage manageCustomFieldEditPage;
        ManageCustomFieldDeletePage manageCustomFieldDeletePage;
        ManageCustomFieldUpdatePage manageCustomFieldUpdatePage;

        CustomFieldDBSteps customFieldDBSteps;
        #endregion

        #region Parameters
        string menu = "menuGerenciarCamposPersonalizados";
        #endregion

        [SetUp]
        public void Setup()
        {
            loginFlows = new LoginFlows();
            mainPage = new MainPage();
            manageCustomFieldPage = new ManageCustomFieldPage();
            manageCustomFieldCreatePage = new ManageCustomFieldCreatePage();
            manageCustomFieldEditPage = new ManageCustomFieldEditPage();
            manageCustomFieldDeletePage = new ManageCustomFieldDeletePage();
            manageCustomFieldUpdatePage = new ManageCustomFieldUpdatePage();

            customFieldDBSteps = new CustomFieldDBSteps();

            loginFlows.EfetuarLogin(BuilderJson.ReturnParameterAppSettings("USER_LOGIN_PADRAO"), BuilderJson.ReturnParameterAppSettings("PASSWORD_LOGIN_PADRAO"));
        }

        [Test]
        public void CriarCampoPersonalizadoComSucesso()
        {
            #region Parameters
            string customFieldName = "Custom_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);

            //Resultado esperado
            string messageSucessExpected = "Operação realizada com sucesso";
            #endregion

            mainPage.ClicarMenu(menu);
            manageCustomFieldPage.PreencherCustomField(customFieldName);
            manageCustomFieldPage.ClicarNewCustomField();

            StringAssert.Contains(messageSucessExpected, manageCustomFieldCreatePage.RetornarMensagemDeSucesso(), "A mensagem retornada não é o esperada.");

            var consultarCampoDB = customFieldDBSteps.ConsultarCampoDB(customFieldName);
            Assert.IsNotNull(consultarCampoDB, "O campo não foi criado.");

            customFieldDBSteps.DeletarCampoDB(customFieldName);
        }

        [Test]
        public void CriarCampoPersonalizadoNomeEmBranco()
        {
            #region Parameters
            //Resultado esperado
            string messageErrorExpected = "Um campo necessário 'name' estava vazio.";
            #endregion

            mainPage.ClicarMenu(menu); 
            manageCustomFieldPage.ClicarNewCustomField();

            StringAssert.Contains(messageErrorExpected, manageCustomFieldCreatePage.RetornarMensagemDeErro(), "A mensagem retornada não é o esperada.");
        }

        [Test]
        public void ApagarCampoPersonalizadoComSucesso()
        {
            #region Inserindo um novo campo personalizado
            string customFieldName = "Custom_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);

            customFieldDBSteps.InserirTagDB(customFieldName);
            #endregion

            #region Parameters          
            //Resultado esperado
            string messageSucessExpected = "Operação realizada com sucesso";
            #endregion

            mainPage.ClicarMenu(menu);
            manageCustomFieldPage.ClicarCustomFieldLink(customFieldName);
            manageCustomFieldEditPage.ClicarApagarCustomField();
            manageCustomFieldDeletePage.ClicarApagarCustomField(customFieldName);
     
            StringAssert.Contains(messageSucessExpected, manageCustomFieldDeletePage.RetornarMensagemDeSucesso(), "A mensagem retornada não é o esperada.");

            var consultarCampoDB = customFieldDBSteps.ConsultarCampoDB(customFieldName);
            Assert.IsNull(consultarCampoDB, "O campo não foi excluído.");
        }

        [Test]
        public void EditarCampoPersonalizadoComSucesso()
        {
            #region Inserindo um novo campo personalizado
            string customFieldName = "Custom_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);

            customFieldDBSteps.InserirTagDB(customFieldName);
            #endregion

            #region Parameters
            string newCustomFieldName = "Custom_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);

            //Resultado esperado
            string messageSucessExpected = "Operação realizada com sucesso";
            #endregion

            mainPage.ClicarMenu(menu);
            manageCustomFieldPage.ClicarCustomFieldLink(customFieldName);
            manageCustomFieldEditPage.LimparCustomFieldName();
            manageCustomFieldEditPage.PreencherCustomFieldName(newCustomFieldName);
            manageCustomFieldEditPage.ClicarAtualizarCustomField();

            StringAssert.Contains(messageSucessExpected, manageCustomFieldPage.RetornarMensagemDeSucesso(), "A mensagem retornada não é o esperada.");

            var consultarCampoDB = customFieldDBSteps.ConsultarCampoDB(newCustomFieldName);
            Assert.IsNotNull(consultarCampoDB, "O nome do campo não foi alterado.");

            customFieldDBSteps.DeletarCampoDB(newCustomFieldName);
        }

        [Test]
        public void EditarCampoPersonalizadoNomeJaExiste()
        {
            #region Inserindo um novo campo personalizado
            string customFieldNameOne = "Custom_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);

            customFieldDBSteps.InserirTagDB(customFieldNameOne);

            string customFieldNameTwo = "Custom_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);

            customFieldDBSteps.InserirTagDB(customFieldNameTwo);
            #endregion

            #region Parameters
            //Resultado esperado
            string messageErrorExpected = "Este é um nome duplicado.";
            #endregion

            mainPage.ClicarMenu(menu);
            manageCustomFieldPage.ClicarCustomFieldLink(customFieldNameOne);
            manageCustomFieldEditPage.LimparCustomFieldName();
            manageCustomFieldEditPage.PreencherCustomFieldName(customFieldNameTwo);
            manageCustomFieldEditPage.ClicarAtualizarCustomField();

            StringAssert.Contains(messageErrorExpected, manageCustomFieldUpdatePage.RetornarMensagemDeErro(), "A mensagem retornada não é o esperada.");

            customFieldDBSteps.DeletarCampoDB(customFieldNameOne);
            customFieldDBSteps.DeletarCampoDB(customFieldNameTwo);
        }

        [Test]
        public void EditarCampoPersonalizadoNomeEmBranco()
        {
            #region Inserindo um novo campo personalizado
            string customFieldName= "Custom_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);

            customFieldDBSteps.InserirTagDB(customFieldName);
            #endregion

            #region Parameters
            //Resultado esperado
            string messageErrorExpected = "Um campo necessário 'name' estava vazio.";
            #endregion

            mainPage.ClicarMenu(menu);
            manageCustomFieldPage.ClicarCustomFieldLink(customFieldName);
            manageCustomFieldEditPage.LimparCustomFieldName();
            manageCustomFieldEditPage.ClicarAtualizarCustomField();

            StringAssert.Contains(messageErrorExpected, manageCustomFieldUpdatePage.RetornarMensagemDeErro(), "A mensagem retornada não é o esperada.");

            customFieldDBSteps.DeletarCampoDB(customFieldName);
        }
    }
}