using System.IO;
using System.Text;
using AutomacaoMantis.Domain;
using AutomacaoMantis.Helpers;
using System.Collections.Generic;

namespace AutomacaoMantis.DBSteps.Projects
{
    public class ProjectsDBSteps
    {
        public ProjectDomain ConsultarProjetoDB(string projectName)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Projects/consultaProjeto.sql", Encoding.UTF8);
            query = query.Replace("$projectName", projectName);

            ExtentReportHelpers.AddTestInfoDB(2, "PARAMETERS: Nome do projeto = " + projectName);

            return DataBaseHelpers.ObtemRegistroUnico<ProjectDomain>(query);
        }
        public void DeletarProjetoDB(int projectId)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Projects/deletaProjeto.sql", Encoding.UTF8);
            query = query.Replace("$projectId", projectId.ToString());

            ExtentReportHelpers.AddTestInfoDB(2, "PARAMETERS: ID do projeto = " + projectId);

            DataBaseHelpers.ExecuteQuery(query);
        }
        public ProjectDomain InserirProjetoDB(string projectName)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Projects/inseriProjeto.sql", Encoding.UTF8);
            query = query.Replace("$projectName", projectName);

            ExtentReportHelpers.AddTestInfoDB(2, "PARAMETERS: Nome do projeto = " + projectName);

            return DataBaseHelpers.ObtemRegistroUnico<ProjectDomain>(query);
        }
        public ProjectDomain ConsultarVersaoProjetoDB(string versionName)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Projects/consultaVersaoProjeto.sql", Encoding.UTF8);
            query = query.Replace("$versionName", versionName);

            ExtentReportHelpers.AddTestInfoDB(2, "PARAMETERS: Nome da versão criada = " + versionName);

            return DataBaseHelpers.ObtemRegistroUnico<ProjectDomain>(query);
        }
        public ProjectDomain InserirVersaoProjetoDB(int projectId, string versionName)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Projects/inseriVersaoProjeto.sql", Encoding.UTF8);
            query = query.Replace("$projectId", projectId.ToString())
                         .Replace("$versionName", versionName);


            ExtentReportHelpers.AddTestInfoDB(2, "PARAMETERS: ID do projeto = " + projectId + " | Nome da versão criada = " + versionName);

            return DataBaseHelpers.ObtemRegistroUnico<ProjectDomain>(query);
        }
        public void DeletarVersaoProjetoDB(string versionName)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Projects/deletaVersaoProjeto.sql", Encoding.UTF8);
            query = query.Replace("$versionName", versionName);

            ExtentReportHelpers.AddTestInfoDB(2, "PARAMETERS: Nome da versão criada = " + versionName);

            DataBaseHelpers.ExecuteQuery(query);
        }
        public ProjectDomain ConsultarSubProjetoDB(int childId, int parentId)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Projects/consultaSubProjeto.sql", Encoding.UTF8);
            query = query.Replace("$childId", childId.ToString())
                         .Replace("$parentId", parentId.ToString());

            ExtentReportHelpers.AddTestInfoDB(2, "PARAMETERS: Child ID = " + childId + " Parent ID = " + parentId);

            return DataBaseHelpers.ObtemRegistroUnico<ProjectDomain>(query);
        }
        public void DeletarSubProjetoDB(int childId, int parentId)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Projects/deletaSubProjeto.sql", Encoding.UTF8);
            query = query.Replace("$childId", childId.ToString())
                         .Replace("$parentId", parentId.ToString());

            ExtentReportHelpers.AddTestInfoDB(2, "PARAMETERS: Child ID = " + childId + " Parent ID = " + parentId);

            DataBaseHelpers.ExecuteQuery(query);
        }
        public void InserirSubProjetoDB(int childId, int parentId, string inheritParent)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Projects/inseriSubProjeto.sql", Encoding.UTF8);
            query = query.Replace("$childId", childId.ToString())
                         .Replace("$parentId", parentId.ToString())
                         .Replace("$inheritParent", inheritParent);

            ExtentReportHelpers.AddTestInfoDB(2, "PARAMETERS: ID do projeto pai = " + parentId + " ID do projeto filho = " + childId);

            DataBaseHelpers.ExecuteQuery(query);
        }
        public ProjectDomain InserirCategoriaDB(string categoryName)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Projects/inseriCategoria.sql", Encoding.UTF8);
            query = query.Replace("$categoryName", categoryName);

            ExtentReportHelpers.AddTestInfoDB(2, "PARAMETERS: Nome da categoria = " + categoryName);

            return DataBaseHelpers.ObtemRegistroUnico<ProjectDomain>(query);
        }
        public ProjectDomain ConsultarCategoriaDB(string categoryName)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Projects/consultaCategoria.sql", Encoding.UTF8);
            query = query.Replace("$categoryName", categoryName);

            ExtentReportHelpers.AddTestInfoDB(2, "PARAMETERS: Nome da categoria = " + categoryName);

            return DataBaseHelpers.ObtemRegistroUnico<ProjectDomain>(query);
        }
        public void DeletarCategoriaDB(string categoryName)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Projects/deletaCategoria.sql", Encoding.UTF8);
            query = query.Replace("$categoryName", categoryName);

            ExtentReportHelpers.AddTestInfoDB(2, "PARAMETERS: Nome da categoria = " + categoryName);

            DataBaseHelpers.ExecuteQuery(query);
        }
        public List<string> ConsultarProjetoAtribuidoAoUsuarioDB(int projectId, string userId)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Projects/consultaProjetoAtribUser.sql", Encoding.UTF8);
            query = query.Replace("$projectId", projectId.ToString())
                         .Replace("$userId", userId);

            ExtentReportHelpers.AddTestInfoDB(2, "PARAMETERS: Nome do projeto = " + projectId + "Nome do usuário = " + userId);

            return DataBaseHelpers.ObtemDados(query);
        }
        public void DeletarProjetoAtribuidoAoUsuarioDB(int projectId, string userId)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Projects/deletaProjetoAtribUser.sql", Encoding.UTF8);
            query = query.Replace("$projectId", projectId.ToString())
                         .Replace("$userId", userId);

            ExtentReportHelpers.AddTestInfoDB(2, "PARAMETERS: Nome do projeto = " + projectId + "Nome do usuário = " + userId);

            DataBaseHelpers.ExecuteQuery(query);
        }
        public void InserirProjetoAtribuidoAoUsuarioDB(int projectId, string userId, string accessLevel)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Projects/inseriProjetoAtribUser.sql", Encoding.UTF8);
            query = query.Replace("$projectId", projectId.ToString())
                         .Replace("$userId", userId)
                         .Replace("$accessLevel", accessLevel);

            ExtentReportHelpers.AddTestInfoDB(2, "PARAMETERS: Nome do projeto = " + projectId + "Nome do usuário = " + userId);

            DataBaseHelpers.ExecuteQuery(query);
        }
    }
}