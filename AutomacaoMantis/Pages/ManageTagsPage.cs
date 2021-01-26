using OpenQA.Selenium;
using AutomacaoMantis.Bases;

namespace AutomacaoMantis.Pages
{
    public class ManageTagsPage : PageBase
    {
        #region Mapping
        By createTagButton = By.Name("config_set");
        private By TagNameLinkBy(string tagName)
        {
            return By.XPath("//table[@class='table table-striped table-bordered table-condensed table-hover']//a[contains(., '" + tagName + "')]");
        }
        By tagNameField = By.Id("tag-name");
        By tagDescriptionField = By.Id("tag-description");
        #endregion

        #region Actions
        public void ClicarCriarMarcador()
        {
            Click(createTagButton);
        }

        public void PreencherNomeMarcador(string tagName)
        {
            SendKeys(tagNameField, tagName);
        }

        public void PreencherDescricaoMarcador(string tagDescription)
        {
            SendKeys(tagDescriptionField, tagDescription);
        }

        public bool RetornaSeATagCriadaEstaSendoExibidaNaTela(string tagName)
        {
            return IsElementExists(TagNameLinkBy(tagName));
        }

        public void ClicarNomeMarcador(string tagName)
        {
            Click(TagNameLinkBy(tagName));
        }
        #endregion
    }
}