using System.IO;
using System.Text;
using AutomacaoMantis.Domain;
using AutomacaoMantis.Helpers;

namespace AutomacaoMantis.DBSteps.Users
{
    public class UsersDBSteps
    {
        public UserDomain ConsultarUsuarioDB(string username)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Users/consultaUsuario.sql", Encoding.UTF8);
            query = query.Replace("$username", username);

            ExtentReportHelpers.AddTestInfoDB(2, "PARAMETERS: Username = " + username);

            return DataBaseHelpers.ObtemRegistroUnico<UserDomain>(query);
        }
        public void DeletarUsuarioDB(string userId)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Users/deletaUsuario.sql", Encoding.UTF8);
            query = query.Replace("$userId", userId);

            ExtentReportHelpers.AddTestInfoDB(2, "PARAMETERS: ID do usuário = " + userId);

            DataBaseHelpers.ExecuteQuery(query);
        }
        //public void InserirUsuarioDB(string username, string realname, string enabled, string cookie, string email)
        //{
        //    string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Users/inseriUsuario.sql", Encoding.UTF8);
        //    query = query.Replace("$username", username).Replace("$realname", realname).Replace("$enabled", enabled).Replace("$cookie", cookie).Replace("$email", email);

        //    ExtentReportHelpers.AddTestInfoDB(2, "PARAMETERS: Username = " + username + " | Realname = " + realname + " | Enabled = " + enabled + " | E-mail = " + email);

        //    DataBaseHelpers.ExecuteQuery(query);
        //}
        public UserDomain InserirUsuarioDB(string username, string realname, string enabled, string cookie, string email)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Users/inseriUsuario.sql", Encoding.UTF8);
            query = query.Replace("$username", username).Replace("$realname", realname).Replace("$enabled", enabled).Replace("$cookie", cookie).Replace("$email", email);

            ExtentReportHelpers.AddTestInfoDB(2, "PARAMETERS: Username = " + username + " | Realname = " + realname + " | Enabled = " + enabled + " | E-mail = " + email);

            return DataBaseHelpers.ObtemRegistroUnico<UserDomain>(query);
        }
        public void DeletarEmailUsuarioDB(string userEmail)
        {
            string query = File.ReadAllText(GeneralHelpers.GetProjectPath() + "Queries/Users/deletaEmailUsuario.sql", Encoding.UTF8);
            query = query.Replace("$userEmail", userEmail);

            ExtentReportHelpers.AddTestInfoDB(2, "PARAMETERS: E-mail do usuário = " + userEmail);

            DataBaseHelpers.ExecuteQuery(query);
        }
    }
}