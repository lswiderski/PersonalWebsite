using Microsoft.AspNetCore.Mvc;
using NLog;

namespace PersonalWebsite.Controllers
{
    public abstract class PersonalController : Controller
    {
        protected Logger _logger;

        protected virtual Logger Logger
        {
            get { return _logger ?? (_logger = LogManager.GetLogger(GetType().Name)); }
        }
    }
}