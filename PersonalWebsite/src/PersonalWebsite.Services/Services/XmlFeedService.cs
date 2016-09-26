using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using PersonalWebsite.Common;
using PersonalWebsite.Services.Models;

namespace PersonalWebsite.Services
{
    public class XmlFeedService : IXmlFeedService
    {
        private readonly ICacheService _cacheService;
        private readonly ISettingModel _settingModel;
        private readonly IPostModel _postModel;
        public XmlFeedService(ISettingModel settingModel, ICacheService cacheService, IPostModel postModel)
        {
            _cacheService = cacheService;
            _postModel = postModel;
            _settingModel = settingModel;
            
        }
        public string BuildXmlFeed(ControllerContext context)
        {
            var url = new UrlHelper(context);
            var key = "RssFeed";
            var posts = _cacheService.Get<List<SimplifiedPostViewModel>>(key) ?? _postModel.GetPublishedSimplifiedPostsForFeed(10).ToList();

            _cacheService.Store(key, posts);

            StringWriter parent = new StringWriter();
            using (XmlWriter writer = XmlWriter.Create(parent))
            {
                writer.WriteProcessingInstruction("xml-stylesheet", "title=\"XSL_formatting\" type=\"text/xsl\" href=\"/skins/default/controls/rss.xsl\"");

                writer.WriteStartElement("rss");
                writer.WriteAttributeString("version", "2.0");

                writer.WriteAttributeString("xmlns", "atom", null, "http://www.w3.org/2005/Atom");

                // write out 
                writer.WriteStartElement("channel");
                
                // write out -level elements
                writer.WriteElementString("title", _settingModel.GetString("Website.Name"));
                writer.WriteElementString("link", context.HttpContext.Request.Host.Value);
                writer.WriteElementString("description", _settingModel.GetString("Website.Description"));
                writer.WriteElementString("ttl", "60");

                writer.WriteStartElement("atom", "link");
                writer.WriteAttributeString("href", context.HttpContext.Request.Host.Value + context.HttpContext.Request.Path.Value);
                writer.WriteAttributeString("rel", "self");
                writer.WriteAttributeString("type", "application/rss+xml");
                writer.WriteEndElement();

                if (posts != null)
                {
                    foreach (var post in posts)
                    {
                        writer.WriteStartElement("item");

                        writer.WriteElementString("title", post.Title);
                        writer.WriteElementString("link", context.HttpContext.Request.Host.Value + url.Action("Blog", "Home", new { id = post.Name }));
                        writer.WriteElementString("description", post.Excerpt);

                        writer.WriteEndElement();
                    }
                }

                // write out 
                writer.WriteEndElement();

                // write out 
                writer.WriteEndElement();
            }

            return parent.ToString();
        }
    }
}
