using AndroidManager.BLL.Dto;
using AndroidManager.BLL.Infrastructure;
using AndroidManager.BLL.Interfaces;
using AndroidManager.DAL.Entities;
using AndroidManager.DAL.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidManager.BLL.Services {

    public class JobService : IJobService {
        private IAndroidUnitOfWork _auow;

        public JobService(IAndroidUnitOfWork auow) {
            this._auow = auow;
        }

        public void Create(JobDto jobDto) {
            var _job = new Job {
                Name = jobDto.Name,
                Description = jobDto.Description,
                Complexity = jobDto.Complexity
            };
            this._auow.Jobs.Create(_job);
            this._auow.SaveAsync();
        }

        public void Update(JobDto jobDto) {
            if (this._auow.Jobs.Get(jobDto.Id) == null) {
                throw new ValidationException("Job is not found", "");
            }
            var _job = new Job {
                Id = jobDto.Id,
                Name = jobDto.Name,
                Description = jobDto.Description,
                Complexity = jobDto.Complexity
            };
            this._auow.Jobs.Update(_job);
            this._auow.SaveAsync();
        }

        public void Remove(int id) {
            var _job = this._auow.Jobs.Get(id);
            if (_job == null) {
                throw new ValidationException("Job is not found", "");
            }
            this._auow.Jobs.Remove(_job);
            this._auow.SaveAsync();
        }

        public JobDto Get(int? id) {
            var _job = this._auow.Jobs.Get(id);
            if (_job == null) {
                throw new ValidationException("Job is not found", "");
            }
            var _jobDto = new JobDto {
                Id = _job.Id,
                Name = _job.Name,
                Description = _job.Description,
                Complexity = _job.Complexity
            };
            return _jobDto;
        }

        public IEnumerable<JobDto> GetAll() {
            var _jobs = this._auow.Jobs.GetAll();
            var _mapper = new MapperConfiguration(cfg => cfg.CreateMap<Job, JobDto>()).CreateMapper();
            return _mapper.Map<IEnumerable<Job>, List<JobDto>>(_jobs).ToList();
        }

        public IEnumerable<JobDto> GetIf(Func<JobDto, bool> predicate) {
            return this.GetAll().Where(predicate).ToList();
        }

        public void Dispose() {
            this._auow.Dispose();
        }
    }
}
