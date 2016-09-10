using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace SuperShopProject.Managers
{
    public abstract class BaseManager
    {
        protected void AddErrorsToModelState(ModelStateDictionary modelState, IdentityResult result)
        {
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    modelState.AddModelError(string.Empty, error);
                }
            }
        }
    }
}