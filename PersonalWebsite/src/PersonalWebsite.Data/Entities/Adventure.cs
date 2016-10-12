using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace PersonalWebsite.Data.Entities
{
    public class Adventure
    {
        public int Id { get; set; }

        public string Country { get; set; }

        public string Map { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
