using AndroidManager.DAL.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidManager.DAL.Identity {

    public class ApplicationOperatorManager : UserManager<ApplicationOperator> {

        public ApplicationOperatorManager(IUserStore<ApplicationOperator> store) : base(store) {
        }
    }
}
