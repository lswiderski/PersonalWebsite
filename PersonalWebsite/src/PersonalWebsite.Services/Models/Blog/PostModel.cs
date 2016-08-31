using PersonalWebsite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Services.Models
{
    public class PostModel : IPostModel
    {
        private readonly DataContext db;
        public PostModel(DataContext db)
        {
            this.db = db;
        }
    }
}
