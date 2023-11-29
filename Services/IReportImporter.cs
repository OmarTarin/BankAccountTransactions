using BankingEvaluation.Models;

namespace BankingEvaluation.Services
{
    internal interface IReportImporter
    {
        Task<IEnumerable<ImportInfo>> Import();
    }
}
