using AndroidManager.BLL.Dto;
using AndroidManager.BLL.Infrastructure;
using AndroidManager.BLL.Interfaces;
using AndroidManager.DAL.Entities;
using AndroidManager.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AndroidManager.BLL.Services {

    public class OperatorService : IOperatorService {
        private IAndroidUnitOfWork _auow;

        public OperatorService(IAndroidUnitOfWork auow) {
            this._auow = auow;
        }

        public async Task<OperationDetails> Create(OperatorDto operatorDto) {
            var _operator = await this._auow.OperatorManager.FindByEmailAsync(operatorDto.Email);
            if (_operator == null) {
                _operator = new ApplicationOperator { Email = operatorDto.Email, UserName = operatorDto.Email };
                var _result = await this._auow.OperatorManager.CreateAsync(_operator, operatorDto.Password);
                if (_result.Errors.Count() > 0) {
                    return new OperationDetails(false, _result.Errors.FirstOrDefault(), "");
                }
                await this._auow.OperatorManager.AddToRoleAsync(_operator.Id, operatorDto.Role);
                await this._auow.SaveAsync();
                return new OperationDetails(true, "Successfull registration", "");
            } else {
                return new OperationDetails(false, "Email is not unique", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(OperatorDto operatorDto) {
            ClaimsIdentity _claim = null;
            var _operator = await this._auow.OperatorManager.FindAsync(operatorDto.Email, operatorDto.Password);
            if (_operator != null) {
                _claim = await this._auow.OperatorManager.CreateIdentityAsync(_operator, DefaultAuthenticationTypes.ApplicationCookie);
            }
            return _claim;
        }

        public void Dispose() {
            this._auow.Dispose();
        }

        public async Task SetInitialData(List<string> roles) {
            foreach (var _roleName in roles) {
                var _role = await this._auow.RoleManager.FindByNameAsync(_roleName);
                if (_role == null) {
                    await this._auow.RoleManager.CreateAsync(new ApplicationRole { Name = _roleName });
                }
            }
        }
    }
}
