using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Services.Models
{
    public class FileDto
    {
        public int FileId { get; set; }
        public string Name { get; set; }
        public string NameInStorage { get; set; }
        public string Type { get; set; }
        public string Extension { get; set; }
        public string Path { get; set; }
        public Guid Guid { get; set; }
    }
}
