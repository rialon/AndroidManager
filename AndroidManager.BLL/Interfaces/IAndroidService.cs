using AndroidManager.BLL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidManager.BLL.Interfaces {

    public interface IAndroidService : IDisposable {
        void Create(AndroidDto androidDto);
        void Update(AndroidDto androidDto);
        void Remove(int id);
        AndroidDto Get(int id);
        IEnumerable<AndroidDto> GetAll();
        IEnumerable<AndroidDto> GetIf(Func<AndroidDto, bool> predicate);
    }
}
