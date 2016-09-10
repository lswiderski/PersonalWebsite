using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Data.Entities
{
    public class Image
    {
        public int ImageId { get; set; }
        public int FileId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        [ForeignKey("FileId")]
        public File File { get; set; }

    }
}
