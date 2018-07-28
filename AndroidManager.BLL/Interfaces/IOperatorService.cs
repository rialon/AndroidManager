using AndroidManager.BLL.Dto;
using AndroidManager.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AndroidManager.BLL.Interfaces {

    public interface IOperatorService : IDisposable {
        Task<OperationDetails> Create(OperatorDto operatorDto);
        Task<ClaimsIdentity> Authenticate(OperatorDto operatorDto);
        Task SetInitialData(List<string> roles);
    }
}
