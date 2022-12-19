using System.ComponentModel.DataAnnotations.Schema;

namespace TestTaskMVC.Models
{
    public class UserProduct
    {
        public long Id { get; set; }

        public long UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public long ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
