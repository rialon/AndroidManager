using AndroidManager.BLL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidManager.BLL.Interfaces {

    public interface IJobService : IDisposable {
        void Create(JobDto jobDto);
        void Update(JobDto jobDto);
        void Remove(int id);
        JobDto Get(int? id);
        IEnumerable<JobDto> GetAll();
        IEnumerable<JobDto> GetIf(Func<JobDto,bool> predicate);
    }
}
