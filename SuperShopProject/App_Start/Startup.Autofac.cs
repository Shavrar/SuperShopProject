using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using SuperShopProject.Managers;
using SuperShopProject.Models;

namespace SuperShopProject
{

        public partial class Startup
        {
            private void ConfigureAutofac(IAppBuilder app)
            {
                var builder = new ContainerBuilder();
                builder.RegisterControllers(typeof(MvcApplication).Assembly);

                builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerRequest();
                builder.RegisterType<UsersStore>().AsSelf().InstancePerRequest();
                builder.RegisterType<UsersManager>().AsSelf().InstancePerRequest();
                builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
                builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
                builder.Register(c => app.GetDataProtectionProvider()).InstancePerRequest();

                var container = builder.Build();
                DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

                app.UseAutofacMiddleware(container);
                app.UseAutofacMvc();
            }        
    }
}