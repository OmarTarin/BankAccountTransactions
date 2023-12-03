using BankingEvaluation.DbContext.Models;
using BankingEvaluation.Models;

namespace BankingEvaluation.Extensions
{
    internal static class ReportFileInfoExtensions
    {

        internal static ImportInfoViewModel ToImportInfo(this ReportFileInfo info)
        {
            return new ImportInfoViewModel()
            {
                Name = info.Name,
                NumberOfPages = info.NumberOfPages,
                NumberOfTransactions = info.NumberOfTransactions
            };
        }

    }
}
