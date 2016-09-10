using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SuperShopProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BundleTable.EnableOptimizations = false;

        }

        /*protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {

            var usersManager = DependencyResolver.Current.GetService<UsersManager>();
            var identity = Context.User.Identity;

            if (identity.IsAuthenticated)
            {

                var userInfo = Session[Consts.CurrentUserInfo] as UserInfo;
                if (userInfo == null)
                {
                    userInfo = usersManager.GetUserInfo(Guid.Parse(User.Identity.GetUserId()));
                    if (userInfo == null)
                    {
                        var authManager = DependencyResolver.Current.GetService<IAuthenticationManager>();
                        authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                        throw new Exception();
                    }
                    Session.Add(Consts.CurrentUserInfo, userInfo);
                }
                else if (usersManager.NeedReload(userInfo.Id))
                {
                    userInfo = usersManager.GetUserInfo(userInfo.Id);
                    Session[Consts.CurrentUserInfo] = userInfo;
                }
                else
                {
                    var notifications = usersManager.GetNotificationsCount(userInfo.Id);
                    userInfo.NewNotficationsCount = notifications;
                    Session[Consts.CurrentUserInfo] = userInfo;
                }
                var language = userInfo.Language ?? "en";
                Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
                if (userInfo.NeedLogout)
                {

                    var urlHelper = new UrlHelper(Request.RequestContext);
                    if (!userInfo.Fake)
                    {
                        var authManager = DependencyResolver.Current.GetService<IAuthenticationManager>();
                        authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                        usersManager.SetNeedLogout(userInfo.Id, false);
                        Response.Redirect(urlHelper.Action("Login", "Account"));
                    }
                    else
                    {
                        var guardianManager = DependencyResolver.Current.GetService<GuardianManager>();
                        Session.Remove(Consts.CurrentUserInfo);
                        guardianManager.Exit(Guid.Parse(User.Identity.GetUserId()));
                        Response.Redirect(urlHelper.Action("Fail", "Guardian"));
                    }
                }
                else
                {
                    Session[Consts.CurrentUserRights] = userInfo.Rights;
                }
            }
            else
            {
                Context.Items[Consts.CurrentUserRights] = new List<Rights>(0);
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            }
        }*/
    }
}
