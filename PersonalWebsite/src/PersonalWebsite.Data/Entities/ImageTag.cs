using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Data.Entities
{
    public class ImageTag
    {
        public int ImageId { get; set; }
        public virtual Image Image { get; set; }
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
