using PersonalWebsite.Data;
using PersonalWebsite.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalWebsite.Services.Models
{
    public class ImageService
    {
        private readonly DataContext db;

        public ImageService(DataContext db)
        {
            this.db = db;
        }

        public ImageDto GetImage(int id)
        {
            var image = db.Images.Where(x => x.ImageId == id)
                .Select(x => new ImageDto
                {
                    FileId = x.FileId,
                    Name = x.Name,
                    Height = x.Height,
                    Width = x.Width,
                    Title = x.Title,
                    ImageId = x.ImageId
                }).FirstOrDefault();

            return image;
        }

        public ImageDto GetImage(string name)
        {
            var image = db.Images.Where(x => x.Name == name)
                .Select(x => new ImageDto
                {
                    FileId = x.FileId,
                    Name = x.Name,
                    Height = x.Height,
                    Width = x.Width,
                    Title = x.Title,
                    ImageId = x.ImageId
                }).FirstOrDefault();

            return image;
        }

        public IEnumerable<ImageDto> GetImages()
        {
            var images = db.Images.Select(x => new ImageDto
            {
                FileId = x.FileId,
                Name = x.Name,
                Height = x.Height,
                Width = x.Width,
                Title = x.Title,
                ImageId = x.ImageId
            }).AsEnumerable();

            return images;
        }

        public void DeleteImage(int id)
        {
            var image = db.Images.Where(x => x.ImageId == id).FirstOrDefault();

            db.Images.Remove(image);
            db.SaveChanges();
        }

        public int AddImage(ImageDto imageDto)
        {
            var image = new Image
            {
                FileId = imageDto.FileId,
                Name = imageDto.Name,
                Height = imageDto.Height,
                Width = imageDto.Width,
                Title = imageDto.Title,
            };
            db.Images.Add(image);
            db.SaveChanges();

            return image.ImageId;
        }
    }
}