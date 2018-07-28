using AndroidManager.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidManager.DAL.Entities;
using AndroidManager.DAL.Identity;
using AndroidManager.DAL.EF;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AndroidManager.DAL.Repositories {

    public class AndroidUnitOfWork : IAndroidUnitOfWork {
        private ApplicationContext _db;
        public IRepository<Android> Androids { get; }
        public IRepository<Job> Jobs { get; }
        public ApplicationOperatorManager OperatorManager { get; }
        public ApplicationRoleManager RoleManager { get; }

        private bool _disposed = false;

        public AndroidUnitOfWork(string connectionString) {
            this._db = new ApplicationContext(connectionString);
            this.Androids = new AndroidRepository(this._db);
            this.Jobs = new JobRepository(this._db);
            this.OperatorManager = new ApplicationOperatorManager(new UserStore<ApplicationOperator>(this._db));
            this.RoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(this._db));
        }

        public async Task SaveAsync() {
            await this._db.SaveChangesAsync();
        }

        private void _Dispose(bool disposing) {
            if (!this._disposed) {
                if (disposing) {
                    this._db.Dispose();
                }
                this._disposed = true;
            }
        }

        public void Dispose() {
            this._Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
