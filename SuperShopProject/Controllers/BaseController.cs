using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SuperShopProject.Constants;
using SuperShopProject.Helpers;

namespace SuperShopProject.Controllers
{
    public abstract class BaseController : Controller
    {
        protected const string ContentTypeJson = "application/json";

        protected ActionResult JsonNet(object data)
        {
            return new ContentResult
            {
                ContentType = ContentTypeJson,
                Content = data.ToJsonCamelCase()
            };
        }
        
        private Guid? _usreId;
        protected Guid UserId => _usreId ?? (_usreId = User.Identity.IsAuthenticated ? Guid.Parse(User.Identity.GetUserId()) : Guid.Empty).Value;        
    }
}