using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstaSharp.Data.Models
{
    [Table("Following")]
    public class Following
    {
        [Key]
        public int Id { get; set; }

        public virtual ApplicationUser UserFollowing { get; set; }

        public virtual ApplicationUser UserFollowed { get; set; }

        public DateTime Timestamp { get; set; }

        public bool Accepted { get; set; }
    }
}
