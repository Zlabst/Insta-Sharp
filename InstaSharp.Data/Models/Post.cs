using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaSharp.Data.Models
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        public int Id { get; set; }

        public virtual ApplicationUser User { get; set; }

        public DateTime Timestamp { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public virtual List<Like> Likes { get; set; }
    }
}
