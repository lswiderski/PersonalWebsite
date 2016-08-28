using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Data.Entities
{
    public class PostCategory
    {
        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
