using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Services.Models
{
    public interface IImageService
    {
        ImageDto GetImage(int id);
        ImageDto GetImage(string name);
        IEnumerable<ImageDto> GetImages();
        void DeleteImage(int id);
        int AddImage(ImageDto imageDto);
        int AddImage(UploadImageDto img);
        ImageViewModel GetImageViewModel(int id, string host);
        IEnumerable<ImageViewModel> GetImageViewModels(string host);
    }
}
