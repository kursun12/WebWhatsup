using System.ComponentModel.DataAnnotations;

namespace WebWhatsUp.Models
{
    public partial class GroupUsers
    {
        [Key]
        public string FromGroup { get; set; }
        public int AccountId { get; set; }
    }
}