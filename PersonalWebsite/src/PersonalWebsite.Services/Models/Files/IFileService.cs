using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Services.Models
{
    public interface IFileService
    {
        FileDto GetFile(int id);
        FileDto GetFile(Guid guid);
        FileDto GetFile(string name);
        IEnumerable<FileDto> GetFiles();
        void DeleteFile(int id);
        int AddFile(FileDto filedto);
    }
}
