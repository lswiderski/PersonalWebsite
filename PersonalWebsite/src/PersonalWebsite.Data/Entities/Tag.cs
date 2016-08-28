using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Data.Entities
{
    public class Tag
    {
        public int TagId { get; set; }

        public string Name { get; set; }

        public int Uses { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}
