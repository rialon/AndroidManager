using AndroidManager.DAL.Entities;
using AndroidManager.DAL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidManager.DAL.Interfaces {

    public interface IAndroidUnitOfWork : IDisposable {
        IRepository<Android> Androids { get; }
        IRepository<Job> Jobs { get; }
        ApplicationOperatorManager OperatorManager { get; }
        ApplicationRoleManager RoleManager { get; }

        Task SaveAsync();
    }
}
