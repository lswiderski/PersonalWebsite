﻿using System;
using System.Collections.Generic;
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

        public DateTime CreatedOn { get; set; }

        public string Content { get; set; }

        public List<CategoryViewModel> Categories { get; set; }

        public List<TagViewModel> Tags { get; set; }
    }
}