
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingEvaluation.DbContext.Models
{
    [Table("Accounts")]
    public class Account : BaseEntity
    {
        [Required]
        [DisplayName("Date")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Referenz { get; set; }

        [Required]
        public double Value { get; set; }

        public Guid? ReportFileInfoId { get; set; }
        public ReportFileInfo ReportFileInfo { get; set; }
    }
}
