
using System.ComponentModel.DataAnnotations;

namespace BankingEvaluation.DbContext.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
    }
}
