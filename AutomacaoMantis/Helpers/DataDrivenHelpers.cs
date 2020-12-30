using System.Linq;
using NUnit.Framework;
using AutomacaoMantis.Domain;
using System.Collections.Generic;

namespace AutomacaoMantis.Helpers
{
    public class DataDrivenHelpers
    {
        public static IEnumerable<UserDomain> CriarUsuarioComSucessoTestData
        {
            get
            {
                var testCases = new List<TestCaseData>();
                var userDomains = new List<UserDomain>();
                testCases = new ExcelHelpers().ReadExcelData(GeneralHelpers.GetProjectPath() + @"Resources\DataDriven\CriarUsuarioComSucessoTestData.xlsx");

                userDomains = testCases.Select(t =>
                                   new UserDomain
                                   {
                                       Username = t.Arguments[0].ToString(),
                                       RealName = t.Arguments[1].ToString(),
                                       Email = t.Arguments[2].ToString(),
                                       AccessLevel = t.Arguments[3].ToString(),
                                   }).ToList();

                if (testCases != null)
                {
                    foreach (UserDomain testCaseData in userDomains)
                        yield return testCaseData;
                }
            }
        }

        public static IEnumerable<UserDomain> CriarUsuarioEmailInvalidoTestData
        {
            get
            {
                var testCases = new List<TestCaseData>();
                var userDomains = new List<UserDomain>();
                testCases = new ExcelHelpers().ReadExcelData(GeneralHelpers.GetProjectPath() + @"Resources\DataDriven\CriarUsuarioEmailInvalidoTestData.xlsx");

                userDomains = testCases.Select(t =>
                                   new UserDomain
                                   {
                                       Username = t.Arguments[0].ToString(),
                                       RealName = t.Arguments[1].ToString(),
                                       Email = t.Arguments[2].ToString(),
                                   }).ToList();

                if (testCases != null)
                {
                    foreach (UserDomain testCaseData in userDomains)
                        yield return testCaseData;
                }
            }
        }

        public static IEnumerable<UserDomain> CriarUsuarioSemInformarUsernameTestData
        {
            get
            {
                var testCases = new List<TestCaseData>();
                var userDomains = new List<UserDomain>();
                testCases = new ExcelHelpers().ReadExcelData(GeneralHelpers.GetProjectPath() + @"Resources\DataDriven\CriarUsuarioSemInformarUsernameTestData.xlsx");

                userDomains = testCases.Select(t =>
                                   new UserDomain
                                   {
                                       RealName = t.Arguments[0].ToString(),
                                       Email = t.Arguments[1].ToString(),

                                   }).ToList();

                if (testCases != null)
                {
                    foreach (UserDomain testCaseData in userDomains)
                        yield return testCaseData;
                }
            }
        }
    }
}