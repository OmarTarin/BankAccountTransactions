
namespace BankingEvaluation.DbContext.Models
{
    public class Account
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public string IntendedUse { get; set; }
    }
}
