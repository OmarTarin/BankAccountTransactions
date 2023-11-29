using System.Linq.Expressions;
using BankingEvaluation.DbContext.Models;
using Microsoft.Extensions.Configuration;

namespace BankingEvaluation.DbContext
{
    internal class ReportProvider : IReportProvider
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Account> LoadAccounts(int skip, int take)
        {
            return _unitOfWork.Accounts.Skip(skip).Take(take).ToList();
        }

        public IEnumerable<Account> LoadAccounts(DateTime from, DateTime to, IEnumerable<string>? searchItems = null)
        {
            IQueryable<Account> baseQuery = _unitOfWork.Accounts
                .Where(p => p.Date >= from && p.Date <= to)
                .OrderBy(p => p.Date);

            //contains any item from searchItems
            IQueryable<Account> finalQuery = baseQuery;

            if (searchItems != null && searchItems.Any())
            {
                ParameterExpression parameter = Expression.Parameter(typeof(Account), "p");
                Expression body = Expression.Constant(false);

                foreach (var searchItem in searchItems)
                {
                    Expression property = Expression.Property(parameter, "Referenz");
                    Expression containsMethod = Expression.Call(property, "Contains", null, Expression.Constant(searchItem));
                    body = Expression.OrElse(body, containsMethod);
                }

                Expression<Func<Account, bool>> predicate = Expression.Lambda<Func<Account, bool>>(body, parameter);
                finalQuery = finalQuery.Where(predicate);
            }

            return finalQuery.ToList();
        }
        
    }
}
