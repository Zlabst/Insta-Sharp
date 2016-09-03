using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstaSharp.Data.Models
{
    [Table("Notifications")]
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        public virtual ApplicationUser ToUser { get; set; }

        public virtual ApplicationUser FromUser { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Notification must be less than 255 characters")]
        public string Message { get; set; }

        public bool Viewed { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
