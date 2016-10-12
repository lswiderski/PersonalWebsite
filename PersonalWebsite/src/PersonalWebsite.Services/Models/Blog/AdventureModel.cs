using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalWebsite.Data;
using PersonalWebsite.Data.Entities;

namespace PersonalWebsite.Services.Models
{
    public class AdventureModel : IAdventureModel
    {
        private readonly DataContext db;

        public AdventureModel(DataContext db)
        {
            this.db = db;
        }

        public void Add(AddAdventureViewModel model)
        {
            var adventure = new Adventure
            {
                Country = model.Country,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Map = model.Map,
                PostId = model.PostId
            };
            db.Adventures.Add(adventure);
            db.SaveChanges();
        }

        public void Edit(EditAdventureViewModel model)
        {
            var adventure = db.Adventures.FirstOrDefault(x => x.Id == model.Id);

            adventure.Country = model.Country;
            adventure.Map = model.Map;
            adventure.Latitude = model.Latitude;
            adventure.Longitude = model.Longitude;
            adventure.PostId = model.PostId;

            db.SaveChanges();
        }

        public EditAdventureViewModel GetEdit(int id)
        {
            var adventure = db.Adventures.Where(x => x.Id == id).Select(x => new EditAdventureViewModel
            {
                Country = x.Country,
                Map = x.Map,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                PostId = x.PostId
            }).FirstOrDefault();

            return adventure;
        }

        public List<AdventureViewModel> GetListForAdmin()
        {
            var adventures = (from adventure in db.Adventures
                              join post in db.Posts on adventure.PostId equals post.PostId
                select new AdventureViewModel
                {
                    Country = adventure.Country,
                    Map = adventure.Map,
                    Latitude = adventure.Latitude,
                    Longitude = adventure.Longitude,
                    PostId = adventure.PostId,
                    Title = post.Title
                }).ToList();

            return adventures;
        }
        
        public List<AdventureMapPointerDTO> GetListForMap()
        {
            var adventures = (from adventure in db.Adventures
                              join post in db.Posts on adventure.PostId equals post.PostId
                              select new AdventureMapPointerDTO
                              {
                                  Latitude = adventure.Latitude,
                                  Longitude = adventure.Longitude,
                                  Url = post.Name,
                                  Title = post.Title,
                                  CustomData = db.Set<Image>()
                                        .Where(x => x.ImageId == post.HeaderImageId)
                                        .Select(x => x.Thumbnail.Path)
                                        .FirstOrDefault(),
                              }).ToList();

            return adventures;
        }
    }
}
