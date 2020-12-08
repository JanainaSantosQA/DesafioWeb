using System.IO;
using System.Text;
using AutomacaoMantis.Domain;
using AutomacaoMantis.Helpers;
using System.Collections.Generic;

namespace AutomacaoMantis.DBSteps.Issues
{
    public class IssuesDBSteps
    {
        public IssuesDomain ConsultaBugDB(int projectId, string summary)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Issues/consultaBug.sql", Encoding.UTF8);
            query = query.Replace("$projectId", projectId.ToString()).Replace("$summary", summary);

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: ID do projeto = " + projectId + " Resumo = " + summary);

            return DataBaseHelpers.ObtemRegistroUnico<IssuesDomain>(query);
        }
        public void DeletaBugDB(int bugId)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Issues/deletaBug.sql", Encoding.UTF8);
            query = query.Replace("$bugId", bugId.ToString());

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: ID do bug = " + bugId);

            DataBaseHelpers.ExecuteQuery(query);
        }
        public IssuesDomain InseriBugDB(int projectId, string summary)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Issues/inseriBug.sql", Encoding.UTF8);
            query = query.Replace("$projectId", projectId.ToString()).Replace("$summary", summary);

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: ID do projeto = " + projectId + " Resumo = " + summary);

            return DataBaseHelpers.ObtemRegistroUnico<IssuesDomain>(query);
        }
        public void DeletaTextoBugDB(int bugId)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Issues/deletaBugText.sql", Encoding.UTF8);
            query = query.Replace("$bugId", bugId.ToString());

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: ID do bug = " + bugId);

            DataBaseHelpers.ExecuteQuery(query);
        }
        public List<string> ConsultaNotaBugDB(int bugId, string note)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Issues/consultaNotaBug.sql", Encoding.UTF8);
            query = query.Replace("$bugId", bugId.ToString()).Replace("$note", note);

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: ID do bug = " + bugId + " Descrição da nota = " + note);

            return DataBaseHelpers.ObtemDados(query);
        }
        public string InserirNotaBugDB(int bugId, string note)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Issues/inseriNotaBug.sql", Encoding.UTF8);
            query = query.Replace("$bugId", bugId.ToString()).Replace("$note", note);

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: ID do bug = " + bugId + " Descrição da nota = " + note);

            return DataBaseHelpers.ObtemRegistroUnico<string>(query);
        }
        public void DeletaTextoNotaBugDB(string bugNoteId, string note)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Issues/deletaTextoNotaBug.sql", Encoding.UTF8);
            query = query.Replace("$bugNoteId", bugNoteId).Replace("$note", note);

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: ID da nota = " + bugNoteId + " Descrição da nota = " + note);

            DataBaseHelpers.ExecuteQuery(query);
        }
        public void DeletaNotaBugDB(int bugId, string bugNoteId)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Issues/deletaNotaBug.sql", Encoding.UTF8);
            query = query.Replace("$bugId", bugId.ToString()).Replace("$bugNoteId", bugNoteId);

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: ID do bug = " + bugId + " ID da nota bug = " + bugNoteId);

            DataBaseHelpers.ExecuteQuery(query);
        }
        public void DeletaHistoricoBugDB(int bugId)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Issues/deletaHistoricoBug.sql", Encoding.UTF8);
            query = query.Replace("$bugId", bugId.ToString());

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: ID do bug = " + bugId);

            DataBaseHelpers.ExecuteQuery(query);
        }
        public void DeletaTagBugDB(int bugId)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Issues/deletaBugTag.sql", Encoding.UTF8);
            query = query.Replace("$bugId", bugId.ToString());

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: ID do bug = " + bugId);

            DataBaseHelpers.ExecuteQuery(query);
        }
    }
}