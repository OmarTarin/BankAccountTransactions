namespace BankingEvaluation.Models
{
    internal class AccountViewModel : IViewModel
    {
        public DateTime Date { get; set; }

        public string Referenz { get; set; }

        public string Recipient { get; set; }

        public string RecipientId { get; set; }

        public double Value { get; set; }
    }
}
