using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class ManageTagsPage : PageBase
    {
        #region Mapping
        By criarMarcadorButton = By.Name("config_set");

        By marcadoresExistentesTable = By.XPath("//table[@class='table table-striped table-bordered table-condensed table-hover']/tbody");

        By tagNameField = By.Id("tag-name");
        By tagDescriptionField = By.Id("tag-description");
        #endregion

        #region Actions
        public void ClicarCriarMarcador()
        {
            Click(criarMarcadorButton);
        }

        public void PreencherNomeMarcador(string tagName)
        {
            SendKeys(tagNameField, tagName);
        }

        public void PreencherDescricaoMarcador(string tagDescription)
        {
            SendKeys(tagDescriptionField, tagDescription);
        }

        public void VerificarSeATagCriadaEstaSendoExibidaNaTela(string tagName)
        {
            VerifyTextElement(marcadoresExistentesTable, tagName);
        }

        public void ClicarMarcador(string tagName)
        {
            Click(By.XPath("//a[text()='" + tagName + "']"));
        }
        #endregion
    }
}