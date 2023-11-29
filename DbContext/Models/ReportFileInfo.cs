

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingEvaluation.DbContext.Models
{
    [Table("ReportFileInfos")]
    public class ReportFileInfo : BaseEntity
    {
        [Required]
        public int NumberOfPages { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Required]
        public int NumberOfTransactions { get; set; }

        public IEnumerable<Account> Accounts { get; set; }
    }
}
