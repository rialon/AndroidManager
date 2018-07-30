using AndroidManager.BLL.Interfaces;
using AndroidManager.BLL.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AndroidManager.WEB.Infrastructure {

    public class NinjectWEBServiceModule : NinjectModule {

        public override void Load() {
            this.Bind<IOperatorService>().To<OperatorService>();
            this.Bind<IAndroidService>().To<AndroidService>();
            this.Bind<IJobService>().To<JobService>();
        }
    }
}