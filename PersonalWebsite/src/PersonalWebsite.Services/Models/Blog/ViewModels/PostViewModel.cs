using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Services.Models
{
    public class PostViewModel
    {
        public int PostId { get; set; }

        public string Title { get; set; }

        public string Name { get; set; }

        public string Excerpt { get; set; }

        public int? HeaderImageId { get; set; }

        [DisplayFormat(DataFormatString = "{MM-dd-yy}")]
        public DateTime? PublishedOn { get; set; }

        public HtmlString Content { get; set; }

        public List<CategoryViewModel> Categories { get; set; }

        public List<TagViewModel> Tags { get; set; }

        public string HeaderPath { get; set; }

        public string GUID { get; set; }
    }
}
