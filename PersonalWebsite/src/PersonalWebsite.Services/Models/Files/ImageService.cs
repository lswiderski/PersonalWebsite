using Microsoft.EntityFrameworkCore;
using PersonalWebsite.Data;
using PersonalWebsite.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalWebsite.Services.Models
{
    public class ImageService : IImageService
    {
        private readonly DataContext db;
        private readonly IFileService fileService;

        private readonly string host;

        public ImageService(DataContext db, IFileService fileService)
        {
            this.db = db;
            this.fileService = fileService;
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

        public ImageViewModel GetImageViewModel(int id, string host)
        {
            var image = db.Images.Include(x => x.File).Where(x => x.ImageId == id)
                .Select(x => new ImageViewModel
                {
                    Path = string.Format("{0}/{1}", host, x.File.Path),
                    Name = x.Name,
                    Height = x.Height,
                    Width = x.Width,
                    Title = x.Title,
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
        //TODO: order by uploadeddate desc
        public IEnumerable<ImageViewModel> GetImageViewModels(string host)
        {
             
            var images = db.Images.Select(x => new ImageViewModel
            { 
                    Path = string.Format("{0}/{1}", host,x.File.Path),
                    Name = x.Name,
                    Height = x.Height,
                    Width = x.Width,
                    Title = x.Title,
            }).AsEnumerable();

            return images;
        }

        public IEnumerable<ImageViewModel> GetImageViewModels(List<int>ids,string host)
        {

            var images = db.Images.Where(x => ids.Any(y =>y == x.ImageId)).Select(x => new ImageViewModel
            {
                Path = string.Format("{0}/{1}", host, x.File.Path),
                Name = x.Name,
                Height = x.Height,
                Width = x.Width,
                Title = x.Title,
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

        public int AddImage(UploadImageDto img)
        {
            var fileDto = new FileDto
            {
                Extension = img.Extension,
                Guid = Guid.NewGuid(),
                Name = img.Name,
                Path = img.Path,
                Type = img.Type,
                NameInStorage = img.Name,
                Length = img.Length
            };
            fileDto.FileId = fileService.AddFile(fileDto);

            var imageDto = new ImageDto
            {
                FileId = fileDto.FileId,
                Name = fileDto.Name,
                Height = img.Height,
                Width = img.Width,
                Title = img.NameWithoutExtension,
            };
            return this.AddImage(imageDto);
        }
    }
}