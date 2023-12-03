
using BankingEvaluation.DbContext.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingEvaluation.DbContext
{
    internal interface IUnitOfWork
    {
        Task<int> CommitAsync();

        Task AddAsync(Account account);

        Task AddAsync(ReportFileInfo account);

        void EnsureCreated();

        DbSet<Account> Accounts { get; set; }

        DbSet<AccountText> AccountTexts { get; set; }

        DbSet<ReportFileInfo> ReportFileInfos { get; set; }
    }
}
