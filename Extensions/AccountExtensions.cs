using BankingEvaluation.DbContext.Models;
using BankingEvaluation.Models;


namespace BankingEvaluation.Extensions
{
    internal static class AccountExtensions
    {
        internal static AccountViewModel ToAccountViewModel(this Account account, IEnumerable<string> Identifier)
        {

            var Recipient = "";
            var RecipientId = "";
            if (Identifier.Any(p => p.Equals(account.Text.ElementAt(0).Item)))
            {
                Recipient = string.Join(" ", account.Text.Skip(4).Take(5).Select(p => p.Item));
                RecipientId = string.Join(" ", account.Text.Skip(4).Take(1).Select(p => p.Item));
            }
            else
            {
                Recipient = string.Join(" ", account.Text.Skip(3).Take(5).Select(p => p.Item));
                RecipientId = string.Join(" ", account.Text.Skip(3).Take(1).Select(p => p.Item));
            }
            return new AccountViewModel()
            {
                Date = account.Date,
                Recipient = Recipient,
                Referenz = string.Join(" ", account.Text.Take(1).Select(p => p.Item)),
                Value = account.Value,
                RecipientId = RecipientId
            };
        }

    }
}
