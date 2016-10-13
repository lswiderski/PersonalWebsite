using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PersonalWebsite.Services.Models
{
    public class EditAdventureViewModel
    {
        public int Id { get; set; }

        public string Country { get; set; }

        public string Map { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int PostId { get; set; }

        public List<SelectListItem> Posts { get; set; }
    }
}
