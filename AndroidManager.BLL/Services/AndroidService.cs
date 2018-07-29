using AndroidManager.BLL.Dto;
using AndroidManager.BLL.Interfaces;
using AndroidManager.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AndroidManager.DAL.Entities;
using AndroidManager.BLL.Infrastructure;

namespace AndroidManager.BLL.Services {

    public class AndroidService : IAndroidService {
        private IAndroidUnitOfWork _auow;

        public AndroidService(IAndroidUnitOfWork auow) {
            this._auow = auow;
        }

        public void Create(AndroidDto androidDto) {
            var _job = this._auow.Jobs.Get(androidDto.JobId);
            if (_job == null) {
                throw new ValidationException("Job is not found", "");
            }
            var _android = new Android {
                Name = androidDto.Name,
                Skills = androidDto.Skills,
                JobId = androidDto.JobId,
                AvatarImageData = androidDto.AvatarImageData,
                ImageMimeType = androidDto.ImageMimeType          
            };
            this._auow.Androids.Create(_android);
            this._auow.SaveAsync();
        }

        public void Update(AndroidDto androidDto) {
            var _originalAndroid = this._auow.Androids.Get(androidDto.Id);
            if (_originalAndroid == null) {
                throw new ValidationException("Android is not found", "");
            }
            if (_originalAndroid.JobId != androidDto.JobId && androidDto.Reliability != 0) {
                androidDto.Reliability--;
                if (androidDto.Reliability == 0) {
                    androidDto.IsOk = false;
                }
            }
            var _android = new Android {
                Id = androidDto.Id,
                Name = androidDto.Name,
                Skills = androidDto.Skills,
                Reliability = androidDto.Reliability,
                Status = androidDto.IsOk ? 1 : 0,
                JobId = androidDto.JobId,
                AvatarImageData = androidDto.AvatarImageData,
                ImageMimeType = androidDto.ImageMimeType
            };
            this._auow.Androids.Update(_android);
            this._auow.SaveAsync();
        }

        public void Remove(int id) {
            var _android = this._auow.Androids.Get(id);
            if (_android == null) {
                throw new ValidationException("Android is not found", "");
            }
            this._auow.Androids.Remove(_android);
            this._auow.SaveAsync();
        }

        public AndroidDto Get(int id) {
            var _android = this._auow.Androids.Get(id);
            if (_android == null) {
                throw new ValidationException("Android is not found", "");
            }
            var _androidDto = new AndroidDto {
                Id = _android.Id,
                Name = _android.Name,
                IsOk = _android.Status != 0 ? true : false,
                JobId = _android.JobId,
                Skills = _android.Skills,
                Reliability = _android.Reliability,
                AvatarImageData = _android.AvatarImageData,
                ImageMimeType = _android.ImageMimeType
            };
            return _androidDto;
        }

        public IEnumerable<AndroidDto> GetAll() {
            var _androids = this._auow.Androids.GetAll();
            var _mapper = new MapperConfiguration(cfg => cfg.CreateMap<Android, AndroidDto>()
                                .ForMember("IsOk", opt => opt.MapFrom(a => a.Status == 1))).CreateMapper();
            return _mapper.Map<IEnumerable<Android>, List<AndroidDto>>(_androids).ToList();
        }

        public IEnumerable<AndroidDto> GetIf(Func<AndroidDto, bool> predicate) {
            return this.GetAll().Where(predicate).ToList();
        }

        public void Dispose() {
            this._auow.Dispose();
        }
    }
}
