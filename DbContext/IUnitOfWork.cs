
using BankingEvaluation.DbContext.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingEvaluation.DbContext
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();

        Task AddAsync(Account account);

        void EnsureCreated();

        DbSet<Account> Accounts { get; set; }
    }
}
