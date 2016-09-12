using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Services.Models
{
    public class UploadImageDto
    {
        public string Path { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Type { get; set; }
        public long Length { get; set; }
        public string Extension { get; set; }
        public string Name { get; set; }
        public string NameWithoutExtension { get; set; }
        public DateTime UploadedOn { get; set; }
    }
}
