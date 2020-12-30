using System.IO;
using System.Text;
using AutomacaoMantis.Domain;
using AutomacaoMantis.Helpers;

namespace AutomacaoMantis.DBSteps.Filters
{
    public class FiltersDBSteps
    {
        public FilterDomain InserirFiltroPublicoDB(string filterName)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Filters/inseriFiltroPublico.sql", Encoding.UTF8);
            query = query.Replace("$filterName", filterName);

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: Nome do filtro = " + filterName);

            return DataBaseHelpers.ObtemRegistroUnico<FilterDomain>(query);
        }
        public FilterDomain InserirFiltroPrivadoDB(string filterName)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Filters/inseriFiltroPrivado.sql", Encoding.UTF8);
            query = query.Replace("$filterName", filterName);

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: Nome do filtro = " + filterName);

            return DataBaseHelpers.ObtemRegistroUnico<FilterDomain>(query);
        }
        public FilterDomain ConsultarFiltroDB(int filterId)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Filters/consultaFiltro.sql", Encoding.UTF8);
            query = query.Replace("$filterId", filterId.ToString());

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: ID do filtro = " + filterId);

            return DataBaseHelpers.ObtemRegistroUnico<FilterDomain>(query);
        }
        public void DeletarFiltroDB(int filterId)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Filters/deletaFiltro.sql", Encoding.UTF8);
            query = query.Replace("$filterId", filterId.ToString());

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: ID do filtro = " + filterId);

            DataBaseHelpers.ExecuteQuery(query);
        }
    }
}