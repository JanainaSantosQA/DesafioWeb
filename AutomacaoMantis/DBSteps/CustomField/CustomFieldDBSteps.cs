using System.IO;
using System.Text;
using AutomacaoMantis.Helpers;
using System.Collections.Generic;

namespace AutomacaoMantis.DBSteps.CustomField
{
    public class CustomFieldDBSteps
    {
        public List<string> ConsultarCampoDB(string customFieldName)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/CustomField/consultaCampo.sql", Encoding.UTF8);
            query = query.Replace("$customFieldName", customFieldName);

            ExtentReportHelpers.AddTestInfoDB(2, "PARAMETERS: Nome do campo personalizado = " + customFieldName);

            return DataBaseHelpers.ObtemDados(query);
        }

        public void DeletarCampoDB(string customFieldName)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/CustomField/deletaCampo.sql", Encoding.UTF8);
            query = query.Replace("$customFieldName", customFieldName);

            ExtentReportHelpers.AddTestInfoDB(2, "PARAMETERS: Nome do campo personalizado = " + customFieldName);

            DataBaseHelpers.ExecuteQuery(query);
        }

        public string InserirTagDB(string customFieldName)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/CustomField/inseriCampo.sql", Encoding.UTF8);
            query = query.Replace("$customFieldName", customFieldName);

            ExtentReportHelpers.AddTestInfoDB(2, "PARAMETERS: Nome do campo personalizado = " + customFieldName);

            return DataBaseHelpers.ObtemRegistroUnico<string>(query);
        }
    }
}