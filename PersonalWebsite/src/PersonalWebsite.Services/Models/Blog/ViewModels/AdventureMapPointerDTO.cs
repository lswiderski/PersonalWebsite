using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Services.Models
{
    public class AdventureMapPointerDTO
    {

       public int ZoomLevel { get; set; }
       public double Scale { get; set; }
       public string Title { get; set; }
       public double Latitude { get; set; }
       public double Longitude {get; set; } 
       public string Url { get; set; }
       public string CustomData { get; set; }

    }
}
