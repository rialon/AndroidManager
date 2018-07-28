using AndroidManager.DAL.Interfaces;
using AndroidManager.DAL.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidManager.BLL.Infrastructure {

    public class NinjectBLLServiceModule : NinjectModule {
        private string _connectionString;

        public NinjectBLLServiceModule(string connectionString) {
            this._connectionString = connectionString;
        }

        public override void Load() {
            this.Bind<IAndroidUnitOfWork>().To<AndroidUnitOfWork>().WithConstructorArgument(this._connectionString);
        }
    }
}
