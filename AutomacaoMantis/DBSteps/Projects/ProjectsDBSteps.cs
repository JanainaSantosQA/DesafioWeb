using System.IO;
using System.Text;
using AutomacaoMantis.Domain;
using AutomacaoMantis.Helpers;

namespace AutomacaoMantis.DBSteps.Projects
{
    public class ProjectsDBSteps
    {
        public ProjectDomain ConsultarProjetoDB(int projectId)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Projects/consultaProjeto.sql", Encoding.UTF8);
            query = query.Replace("$projectId", projectId.ToString());

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: ID do projeto = " + projectId);

            return DataBaseHelpers.ObtemRegistroUnico<ProjectDomain>(query);
        }
        public void DeletarProjetoDB(int projectId)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Projects/deletaProjeto.sql", Encoding.UTF8);
            query = query.Replace("$projectId", projectId.ToString());

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: ID do projeto = " + projectId);

            DataBaseHelpers.ExecuteQuery(query);
        }
        public ProjectDomain InserirProjetoDB(string projectName)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Projects/inseriProjeto.sql", Encoding.UTF8);
            query = query.Replace("$projectName", projectName);

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: Nome do projeto = " + projectName);

            return DataBaseHelpers.ObtemRegistroUnico<ProjectDomain>(query);
        }
        public ProjectDomain ConsultarVersaoProjetoDB(string versionId)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Projects/consultaVersaoProjeto.sql", Encoding.UTF8);
            query = query.Replace("$versionId", versionId);

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: ID da versão criada = " + versionId);

            return DataBaseHelpers.ObtemRegistroUnico<ProjectDomain>(query);
        }
        public ProjectDomain InserirVersaoProjetoDB(int projectId)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Projects/inseriVersaoProjeto.sql", Encoding.UTF8);
            query = query.Replace("$projectId", projectId.ToString());

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: ID do projeto = " + projectId);

            return DataBaseHelpers.ObtemRegistroUnico<ProjectDomain>(query);
        }
        public void DeletarVersaoProjetoDB(int versionId)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Projects/deletaVersaoProjeto.sql", Encoding.UTF8);
            query = query.Replace("$versionId", versionId.ToString());

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: ID da versão do projeto = " + versionId);

            DataBaseHelpers.ExecuteQuery(query);
        }
        public ProjectDomain ConsultarSubProjetoDB(int childId, int parentId)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Projects/consultaSubProjeto.sql", Encoding.UTF8);
            query = query.Replace("$childId", childId.ToString())
                         .Replace("$parentId", parentId.ToString());

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: Child ID = " + childId + " Parent ID = " + parentId);

            return DataBaseHelpers.ObtemRegistroUnico<ProjectDomain>(query);
        }
        public void DeletarSubProjetoDB(int childId, int parentId)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Projects/deletaSubProjeto.sql", Encoding.UTF8);
            query = query.Replace("$childId", childId.ToString())
                         .Replace("$parentId", parentId.ToString());

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: Child ID = " + childId + " Parent ID = " + parentId);

            DataBaseHelpers.ExecuteQuery(query);
        }
        public void InserirSubProjetoDB(int childId, int parentId, string inheritParent)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Projects/inseriSubProjeto.sql", Encoding.UTF8);
            query = query.Replace("$childId", childId.ToString())
                         .Replace("$parentId", parentId.ToString())
                         .Replace("$inheritParent", inheritParent);

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: ID do projeto pai = " + parentId + " ID do projeto filho = " + childId);

            DataBaseHelpers.ExecuteQuery(query);
        }
    }
}