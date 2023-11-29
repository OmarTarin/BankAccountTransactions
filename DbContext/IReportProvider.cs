using BankingEvaluation.DbContext.Models;
using Microsoft.Extensions.Configuration;

namespace BankingEvaluation.DbContext
{
    internal interface IReportProvider
    {
        IEnumerable<Account> LoadAccounts(int skip, int take);

        IEnumerable<Account> LoadAccounts(DateTime from, DateTime to, IEnumerable<string>? searchItems = null);
    }
}
