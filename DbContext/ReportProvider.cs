using BankingEvaluation.DbContext.Models;
using BankingEvaluation.Extensions;
using BankingEvaluation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace BankingEvaluation.DbContext
{
    internal class ReportProvider : IReportProvider
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _configuration;

        public ReportProvider(IUnitOfWork unitOfWork, IConfiguration conf)
        {
            _unitOfWork = unitOfWork;
            _configuration = conf;
        }

        public IEnumerable<Account> LoadAccounts(int skip, int take)
        {
            return _unitOfWork.Accounts.Skip(skip).Take(take).ToList();
        }

        public IEnumerable<AccountViewModel> LoadAccounts(DateTime from, DateTime to, IEnumerable<string>? searchItems = null)
        {
            var baseQuery = _unitOfWork.Accounts
                .Where(p => p.Date >= from && p.Date <= to)
                .OrderBy(p => p.Date)
                .Include(p => p.Text.OrderBy(q => q.Id))
                .ToList();

            var identifier = _configuration.RecipientIdentifier();

            if (searchItems != null)
            {
                return baseQuery.Where(p => p.Text.Any(q => 
                        searchItems.Any(z => q.Item.ToUpper().Contains(z.ToUpper()))))
                    .Select(p => p.ToAccountViewModel(identifier))
                    .ToList();
            }
            else
                return baseQuery.Select(p => p.ToAccountViewModel(identifier)).ToList();
        }
        
    }
}
