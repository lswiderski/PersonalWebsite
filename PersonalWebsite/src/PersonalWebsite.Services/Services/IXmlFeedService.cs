
using Microsoft.AspNetCore.Mvc;

namespace PersonalWebsite.Services
{
    public interface IXmlFeedService
    {
        string BuildXmlFeed(ControllerContext context);
    }
}