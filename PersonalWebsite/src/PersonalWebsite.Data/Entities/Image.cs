using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalWebsite.Data.Entities
{
    public class Image
    {
        public int ImageId { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int FileId { get; set; }

        [ForeignKey("FileId")]
        public File File { get; set; }

        public int? ThumbnailId { get; set; }

        [ForeignKey("ThumbnailId")]
        public File Thumbnail { get; set; }
    }
}