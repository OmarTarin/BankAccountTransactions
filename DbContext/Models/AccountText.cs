
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankingEvaluation.DbContext.Models
{
    internal class AccountText
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(15)")]
        public string Item { get; set; }

        public Guid AccountId { get; set; }
        public Account Account { get; set; }
    }
}
