
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace BankingEvaluation.Extensions
{
    internal static class IConfigurationExtensions
    {
        internal static string[] RecipientIdentifier(this IConfiguration conf)
        {
            return conf
                .GetSection("RecipientIdentifier")
                .Get<string[]>();
        }

        internal static string ReportFiles(this IConfiguration conf)
        {
            return conf.GetSection("ReportFiles").Value;
        }
    }
}
