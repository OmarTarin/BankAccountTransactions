using BankingEvaluation.DbContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace BankingEvaluation.DbContext
{
    internal class SqliteContext : Microsoft.EntityFrameworkCore.DbContext, IUnitOfWork
    {
        private readonly IConfiguration _conf;


        public SqliteContext(IConfiguration conf, DbContextOptions<SqliteContext> options) : base(options)
        {
            _conf = conf;
        }


        public DbSet<Account> Accounts { get; set; }


        public async Task AddAsync(Account account)
        {
            await Accounts.AddAsync(account);
        }

        public void EnsureCreated()
        {
            Database.EnsureCreated();
        }


        public async Task<int> CommitAsync()
        {
            return await SaveChangesAsync();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(_conf.GetConnectionString("Sqlite"));
        }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Accounts");
        }

    }
    
}
