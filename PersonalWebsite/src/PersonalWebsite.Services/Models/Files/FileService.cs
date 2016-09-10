using PersonalWebsite.Data;
using PersonalWebsite.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Services.Models
{
    public class FileService : IFileService
    {
        private readonly DataContext db;

        public FileService(DataContext db)
        {
            this.db = db;
        }
        public FileDto GetFile(int id)
        {
            var file = db.Files.Where(x => x.FileId == id)
                .Select(x => new FileDto
                {
                    FileId = x.FileId,
                    Name = x.Name,
                    NameInStorage = x.NameInStorage,
                    Path = x.Path,
                    Type = x.Type,
                    Extension = x.Extension,
                    Guid = x.Guid
                }).FirstOrDefault();

            return file;
        }
        public FileDto GetFile(Guid guid)
        {
            var file = db.Files.Where(x => x.Guid == guid)
                .Select(x => new FileDto
                {
                    FileId = x.FileId,
                    Name = x.Name,
                    NameInStorage = x.NameInStorage,
                    Path = x.Path,
                    Type = x.Type,
                    Extension = x.Extension,
                    Guid = x.Guid
                }).FirstOrDefault();

            return file;
        }
        public FileDto GetFile(string name)
        {
            var file = db.Files.Where(x => x.Name == name)
                .Select(x => new FileDto
                {
                    FileId = x.FileId,
                    Name = x.Name,
                    NameInStorage = x.NameInStorage,
                    Path = x.Path,
                    Type = x.Type,
                    Extension = x.Extension,
                    Guid = x.Guid
                }).FirstOrDefault();

            return file;
        }
        public IEnumerable<FileDto> GetFiles()
        {
            var files = db.Files.Select(x => new FileDto
            {
                FileId = x.FileId,
                Name = x.Name,
                NameInStorage = x.NameInStorage,
                Path = x.Path,
                Type = x.Type,
                Extension = x.Extension,
                Guid = x.Guid
            }).AsEnumerable();

            return files;
        }

        public void DeleteFile(int id)
        {
            var file = db.Files.Where(x => x.FileId == id).FirstOrDefault();

            db.Files.Remove(file);
            db.SaveChanges();
        }
        public int AddFile(FileDto filedto)
        {
            var file = new File
            {
                Name = filedto.Name,
                NameInStorage = filedto.NameInStorage,
                Path = filedto.Path,
                Type = filedto.Type,
                Extension = filedto.Extension,
                Guid = Guid.NewGuid(),
            };
            db.Files.Add(file);
            db.SaveChanges();

            return file.FileId;
        }
    }
}
