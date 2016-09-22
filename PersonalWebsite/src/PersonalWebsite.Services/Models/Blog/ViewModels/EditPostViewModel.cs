﻿using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalWebsite.Common;
using PersonalWebsite.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Services.Models
{
    public class EditPostViewModel
    {
        public EditPostViewModel()
        {
            this.Categories = new List<CheckBoxListItem>();
            this.Tags = new List<string>();
        }
        public int PostId { get; set; }

        public string Title { get; set; }

        public string Name { get; set; }

        public string Excerpt { get; set; }

        public string Content { get; set; }

        public int? HeaderImageId { get; set; }

        public PostStatusType Status { get; set; }

        public List<CheckBoxListItem> Categories { get; set; }

        public List<string> Tags { get; set; }

    }
}
