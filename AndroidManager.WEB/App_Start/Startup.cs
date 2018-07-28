using AndroidManager.BLL.Infrastructure;
using AndroidManager.BLL.Interfaces;
using AndroidManager.WEB.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Ninject;
using Ninject.Web.Mvc;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(AndroidManager.WEB.App_Start.Startup))]

namespace AndroidManager.WEB.App_Start {

    public class Startup {
        private static IKernel _kernel;

        static Startup() {
            Startup._UseNinject();
        }

        public void Configuration(IAppBuilder app) {
            app.CreatePerOwinContext<IOperatorService>(() => (Startup._kernel.Get(typeof(IOperatorService)) as IOperatorService));
            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private static void _UseNinject() {
            var _bllModule = new NinjectBLLServiceModule("AndroidManagerDB");
            var _webModule = new NinjectWEBServiceModule();
            Startup._kernel = new StandardKernel(_bllModule, _webModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(Startup._kernel));
        }
    }
}