using BankingEvaluation.DbContext.Models;
using BankingEvaluation.Models;

namespace BankingEvaluation.DbContext
{
    internal interface IReportProvider
    {
        IEnumerable<Account> LoadAccounts(int skip, int take);

        IEnumerable<AccountViewModel> LoadAccounts(DateTime from, DateTime to, IEnumerable<string>? searchItems = null);
    }
}
