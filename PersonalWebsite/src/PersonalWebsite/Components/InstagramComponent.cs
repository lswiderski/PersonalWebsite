using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebsite.Common;
using PersonalWebsite.Services.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using NLog;

namespace PersonalWebsite.Components
{
    public class InstagramComponent : ViewComponent
    {
        private readonly ISettingModel settingModel;
        private readonly ICacheService _cacheService;
        private string cacheKey = "InstagramData";
        private Logger _logger = LogManager.GetCurrentClassLogger();

        public InstagramComponent(ISettingModel settingModel, ICacheService cacheService)
        {
            this.settingModel = settingModel;
            _cacheService = cacheService;
        }

        public IViewComponentResult Invoke()
        {
            var instagramData = GetImages();

            var ig = ParseResponse(instagramData);

            return View(ig);
        }

        private string GetImages()
        {
            var instagramData = _cacheService.Get<string>(cacheKey);

            if (string.IsNullOrEmpty(instagramData))
            {
                var userId = settingModel.GetString("Instagram.UserID");
                var token = settingModel.GetString("Instagram.AccessToken");

                instagramData = GetImagesFromServer(token, userId, 0);
            }
            return instagramData;
        }

        private string GetImagesFromServer(string accessToken, string userId, int count)
        {
            var url = string.Format("https://api.instagram.com/v1/users/{0}/media/recent?access_token={1}&count={2}",
                 userId, accessToken, 9);

            string instagramData = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "jsonp";

                HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;
                Stream streamResponse = response.GetResponseStream();
                StreamReader streamRead = new StreamReader(streamResponse);

                instagramData = streamRead.ReadToEnd();

                streamRead.Dispose();
                streamResponse.Dispose();
                response.Dispose();

                _cacheService.Store(cacheKey, instagramData);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return instagramData;
        }

        public List<InstagramDTO> ParseResponse(string response)
        {
            var ig = new List<InstagramDTO>();
            try
            {
                var instagram = JsonConvert.DeserializeObject<dynamic>(response);
                foreach (var image in instagram.data)
                {
                    ig.Add(new InstagramDTO
                    {
                        ImgURL = image.images.standard_resolution.url,
                        ThumbnailURL = image.images.low_resolution.url,
                        Link = image.link,
                        Text = image.caption.test
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            return ig;
        }
    }
}