using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaSharp.Data.Models
{
    [Table("Likes")]
    public class Like
    {
        [Key]
        public int Id { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Post Post { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
