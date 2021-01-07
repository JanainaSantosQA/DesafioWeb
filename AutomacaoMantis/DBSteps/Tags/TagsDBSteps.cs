using System.IO;
using System.Text;
using AutomacaoMantis.Domain;
using AutomacaoMantis.Helpers;

namespace AutomacaoMantis.DBSteps.Tags
{
    public class TagsDBSteps
    {
        public TagDomain ConsultarTagDB(string tagName)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Tags/consultaTag.sql", Encoding.UTF8);
            query = query.Replace("$tagName", tagName);

            ExtentReportHelpers.AddTestInfoDB(2, "PARAMETERS: Nome da tag = " + tagName);

            return DataBaseHelpers.ObtemRegistroUnico<TagDomain>(query);
        }
        public void DeletarTagDB(string tagName)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Tags/deletaTag.sql", Encoding.UTF8);
            query = query.Replace("$tagName", tagName);

            ExtentReportHelpers.AddTestInfoDB(2, "PARAMETERS: Nome da tag = " + tagName);

            DataBaseHelpers.ExecuteQuery(query);
        }
        public TagDomain InserirTagDB(string tagName, string tagDescription)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Tags/inseriTag.sql", Encoding.UTF8);
            query = query.Replace("$tagName", tagName).Replace("$tagDescription", tagDescription);

            ExtentReportHelpers.AddTestInfoDB(2, "PARAMETERS: Nome da tag = " + tagName + " | Descrição da tag = " + tagDescription);

            return DataBaseHelpers.ObtemRegistroUnico<TagDomain>(query);
        }
    }
}