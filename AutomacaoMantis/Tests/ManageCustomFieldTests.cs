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
        MyViewPage myViewPage;
        ManageCustomFieldPage manageCustomFieldPage;
        ManageCustomFieldEditPage manageCustomFieldEditPage;           

        CustomFieldDBSteps customFieldDBSteps;

        LoginFlows loginFlows;
        #endregion

        #region Parameters
        string menu = "menuGerenciarCamposPersonalizados";
        #endregion

        [SetUp]
        public void Setup()
        {
            myViewPage = new MyViewPage();
            manageCustomFieldPage = new ManageCustomFieldPage();  
            manageCustomFieldEditPage = new ManageCustomFieldEditPage();            

            customFieldDBSteps = new CustomFieldDBSteps();

            loginFlows = new LoginFlows();

            loginFlows.EfetuarLogin(BuilderJson.ReturnParameterAppSettings("USER_LOGIN_PADRAO"), BuilderJson.ReturnParameterAppSettings("PASSWORD_LOGIN_PADRAO"));
        }

        [Test]
        public void CriarCampoPersonalizadoComSucesso()
        {
            #region Parameters
            string customFieldName = "Custom_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);

            //Resultado esperado
            string messageSucessExpected = "Operação realizada com sucesso.";
            #endregion

            #region Actions
            myViewPage.ClicarMenu(menu);
            manageCustomFieldPage.PreencherNomeCampoPersonalizado(customFieldName);
            manageCustomFieldPage.ClicarNovoCampoPersonalizado();
            #endregion

            #region Validations
            Assert.AreEqual(messageSucessExpected, manageCustomFieldPage.RetornaMensagemDeSucesso(), "A mensagem retornada não é a esperada.");
           
            var campoCriadoDB = customFieldDBSteps.ConsultarCampoDB(customFieldName);
            Assert.IsNotNull(campoCriadoDB, "O campo não foi criado.");
            #endregion

            customFieldDBSteps.DeletarCampoDB(customFieldName);
        }

        [Test]
        public void CriarCampoPersonalizadoNomeEmBranco()
        {
            #region Parameters
            //Resultado esperado
            string messageErrorExpected = "Um campo necessário 'name' estava vazio.";
            #endregion

            #region Actions
            myViewPage.ClicarMenu(menu); 
            manageCustomFieldPage.ClicarNovoCampoPersonalizado();
            #endregion

            #region Validations
            StringAssert.Contains(messageErrorExpected, manageCustomFieldPage.RetornaMensagemDeErro(), "A mensagem retornada não é o esperada.");
            #endregion
        }

        [Test]
        public void ApagarCampoPersonalizadoComSucesso()
        {
            #region Inserindo um novo campo personalizado
            string customFieldName = "Custom_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            customFieldDBSteps.InserirCampoDB(customFieldName);
            #endregion

            #region Parameters          
            //Resultado esperado
            string messageSucessExpected = "Operação realizada com sucesso.";
            #endregion

            #region Actions
            myViewPage.ClicarMenu(menu);
            manageCustomFieldPage.ClicarCampoPersonalizadoLink(customFieldName);
            manageCustomFieldEditPage.ClicarApagarCampoPersonalizado();
            manageCustomFieldEditPage.ClicarConfirmacaoApagarCampo(customFieldName);
            #endregion

            #region Validations
            Assert.AreEqual(messageSucessExpected, manageCustomFieldPage.RetornaMensagemDeSucesso(), "A mensagem retornada não é a esperada.");
            
            var campoCriadoDB = customFieldDBSteps.ConsultarCampoDB(customFieldName);
            Assert.IsNull(campoCriadoDB, "O campo não foi excluído.");
            #endregion
        }

        [Test]
        public void EditarNomeCampoPersonalizadoComSucesso()
        {
            #region Inserindo um novo campo personalizado
            string customFieldName = "Custom_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            customFieldDBSteps.InserirCampoDB(customFieldName);
            #endregion

            #region Parameters
            string newCustomFieldName = "Custom_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);

            //Resultado esperado
            string messageSucessExpected = "Operação realizada com sucesso.";
            #endregion

            #region Actions
            myViewPage.ClicarMenu(menu);
            manageCustomFieldPage.ClicarCampoPersonalizadoLink(customFieldName);
            manageCustomFieldEditPage.PreencherNomeCampoPersonalizado(newCustomFieldName);
            manageCustomFieldEditPage.ClicarAtualizarCampoPersonalizado();
            #endregion

            #region Validations
            Assert.AreEqual(messageSucessExpected, manageCustomFieldPage.RetornaMensagemDeSucesso(), "A mensagem retornada não é a esperada.");

            var campoCriadoDB = customFieldDBSteps.ConsultarCampoDB(newCustomFieldName);
            Assert.IsNotNull(campoCriadoDB, "O nome do campo não foi alterado.");
            #endregion

            customFieldDBSteps.DeletarCampoDB(newCustomFieldName);
        }

        [Test]
        public void EditarCampoPersonalizadoNomeJaExiste()
        {
            #region Inserindo um novo campo personalizado
            string customFieldNameOne = "Custom_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            customFieldDBSteps.InserirCampoDB(customFieldNameOne);

            string customFieldNameTwo = "Custom_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            customFieldDBSteps.InserirCampoDB(customFieldNameTwo);
            #endregion

            #region Parameters
            //Resultado esperado
            string messageErrorExpected = "Este é um nome duplicado.";
            #endregion

            #region Actions
            myViewPage.ClicarMenu(menu);
            manageCustomFieldPage.ClicarCampoPersonalizadoLink(customFieldNameOne);
            manageCustomFieldEditPage.PreencherNomeCampoPersonalizado(customFieldNameTwo);
            manageCustomFieldEditPage.ClicarAtualizarCampoPersonalizado();
            #endregion

            #region Validations
            StringAssert.Contains(messageErrorExpected, manageCustomFieldPage.RetornaMensagemDeErro(), "A mensagem retornada não é o esperada.");
            #endregion

            customFieldDBSteps.DeletarCampoDB(customFieldNameOne);
            customFieldDBSteps.DeletarCampoDB(customFieldNameTwo);
        }

        [Test]
        public void EditarCampoPersonalizadoNomeEmBranco()
        {
            #region Inserindo um novo campo personalizado
            string customFieldName= "Custom_" + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            customFieldDBSteps.InserirCampoDB(customFieldName);
            #endregion

            #region Parameters
            string newCustomFieldName = string.Empty;
            //Resultado esperado
            string messageErrorExpected = "Um campo necessário 'name' estava vazio.";
            #endregion

            #region Actions
            myViewPage.ClicarMenu(menu);
            manageCustomFieldPage.ClicarCampoPersonalizadoLink(customFieldName);
            manageCustomFieldEditPage.PreencherNomeCampoPersonalizado(newCustomFieldName);
            manageCustomFieldEditPage.ClicarAtualizarCampoPersonalizado();
            #endregion

            #region Validations
            StringAssert.Contains(messageErrorExpected, manageCustomFieldPage.RetornaMensagemDeErro(), "A mensagem retornada não é o esperada.");
            #endregion

            customFieldDBSteps.DeletarCampoDB(customFieldName);
        }
    }
}