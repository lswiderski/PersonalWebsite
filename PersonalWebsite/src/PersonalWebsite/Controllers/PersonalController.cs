using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design;
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