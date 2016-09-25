using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Common;
using PersonalWebsite.Services.Models;
using System;
using System.IO;
using System.Net;
using System.Threading;

namespace PersonalWebsite.Components
{
    public class InstagramComponent : ViewComponent
    {
        private readonly ISettingModel settingModel;
        private readonly ICacheService _cacheService;
        private static ManualResetEvent allDone = new ManualResetEvent(false);
        private string cacheKey = "InstagramData";
        private string instagramData;

        public InstagramComponent(ISettingModel settingModel, ICacheService cacheService)
        {
            this.settingModel = settingModel;
            _cacheService = cacheService;
        }

        public IViewComponentResult Invoke()
        {
            var instagramData = _cacheService.Get<string>(cacheKey);

            if (string.IsNullOrEmpty(instagramData))
            {
                var settings = settingModel.GetDictionary();
                var url = string.Format("https://api.instagram.com/v1/users/{0}/media/recent?access_token={1}&count={2}",
                   settings["Instagram.UserID"].Value,
                   settings["Instagram.AccessToken"].Value,
                   9);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "GET";
                request.ContentType = "jsonp";
                request.BeginGetResponse(new AsyncCallback(GetResponseCallback), request);
                allDone.WaitOne();
            }
            return View(new ContentResult { Content = instagramData, ContentType = "application/json" });
        }

        private void GetResponseCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
            Stream streamResponse = response.GetResponseStream();
            StreamReader streamRead = new StreamReader(streamResponse);
            instagramData = streamRead.ReadToEnd();

            streamRead.Dispose();
            streamResponse.Dispose();

            response.Dispose();
            _cacheService.Store(cacheKey, instagramData);

            allDone.Set();
        }
    }
}