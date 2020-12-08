using System.IO;
using System.Text;
using AutomacaoMantis.Domain;
using AutomacaoMantis.Helpers;

namespace AutomacaoMantis.DBSteps.Users
{
    public class UsersDBSteps
    {
        public UserDomain ConsultaUsuarioDB(string username)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Users/consultaUsuario.sql", Encoding.UTF8);
            query = query.Replace("$username", username);

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: Username = " + username);

            return DataBaseHelpers.ObtemRegistroUnico<UserDomain>(query);
        }

        public void DeletaUsuarioDB(string userId)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Users/deletaUsuario.sql", Encoding.UTF8);
            query = query.Replace("$userId", userId);

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: ID do usuário = " + userId);

            DataBaseHelpers.ExecuteQuery(query);
        }

        public void InseriUsuarioDB(string username)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Users/inseriUsuario.sql", Encoding.UTF8);
            query = query.Replace("$username", username);

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: Username = " + username);

            DataBaseHelpers.ExecuteQuery(query);
        }

        public void DeletaEmailUsuarioDB(string userEmail)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Users/deletaEmailUsuario.sql", Encoding.UTF8);
            query = query.Replace("$userEmail", userEmail);

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: E-mail do usuário = " + userEmail);

            DataBaseHelpers.ExecuteQuery(query);
        }
    }
}