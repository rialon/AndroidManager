using AndroidManager.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AndroidManager.BLL.Dto;
using AndroidManager.WEB.Models;
using AndroidManager.BLL.Infrastructure;

namespace AndroidManager.WEB.Controllers {

    public class AndroidController : Controller {
        private IAndroidService _androidService;
        private IJobService _jobService;

        public AndroidController(IAndroidService androidService, IJobService jobService) {
            this._androidService = androidService;
            this._jobService = jobService;
        }

        [Authorize]
        public ActionResult Index() {
            var _androidDtos = this._androidService.GetAll();
            var _mapper = new MapperConfiguration(cfg => cfg.CreateMap<AndroidDto, AndroidViewModel>()
                                .ForMember("JobName", opt => opt.MapFrom(a => a.JobId == null ? null : this._jobService.Get(a.JobId).Name))).CreateMapper();
            var _androids = _mapper.Map<IEnumerable<AndroidDto>, List<AndroidViewModel>>(_androidDtos);
            return View(_androids);
        }

        [Authorize]
        public ActionResult Create() {
            var _model = new AndroidViewModel();
            _model.JobsList = new SelectList(this._jobService.GetAll(), "Id", "Name");
            return View(_model);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(AndroidViewModel android, HttpPostedFileBase image = null) {
            if (ModelState.IsValid) {
                var _androidDto = new AndroidDto {
                    Name = android.Name,
                    Skills = android.Skills,
                    JobId = android.JobId
                };
                if (image != null) {
                    _androidDto.ImageMimeType = image.ContentType;
                    _androidDto.AvatarImageData = new byte[image.ContentLength];
                    image.InputStream.Read(_androidDto.AvatarImageData, 0, image.ContentLength);
                }
                try {
                    this._androidService.Create(_androidDto);
                    return RedirectToAction("Index", "Android");
                } catch (ValidationException ex) {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            } else {
                var _duplicatedModelState = ModelState.Values.SingleOrDefault(ms => ms.Errors.Count > 1 && ms.Errors[0].ErrorMessage == ms.Errors[1].ErrorMessage);
                if (_duplicatedModelState != null) {
                    _duplicatedModelState.Errors.Remove(_duplicatedModelState.Errors[1]);
                }
                android.JobsList = new SelectList(this._jobService.GetAll(), "Id", "Name");
            }
            return View(android);
        }

        [Authorize]
        public ActionResult Edit(int androidId) {
            var _androidDto = this._androidService.Get(androidId);
            var _adroid = new AndroidViewModel {
                Id = _androidDto.Id,
                Name = _androidDto.Name,
                Skills = _androidDto.Skills,
                JobId = _androidDto.JobId,
                IsOk = _androidDto.IsOk,
                JobsList = new SelectList(this._jobService.GetAll(), "Id", "Name"),
                Reliability = _androidDto.Reliability,
                AvatarImageData = _androidDto.AvatarImageData,
                ImageMimeType = _androidDto.ImageMimeType
            };
            return View(_adroid);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(AndroidViewModel android, HttpPostedFileBase image = null) {
            if (ModelState.IsValid) {
                var _androidDto = new AndroidDto {
                    Id = android.Id,
                    Name = android.Name,
                    Skills = android.Skills,
                    JobId = android.JobId,
                    IsOk = android.IsOk,
                    Reliability = android.Reliability,
                };
                if (image != null) {
                    _androidDto.ImageMimeType = image.ContentType;
                    _androidDto.AvatarImageData = new byte[image.ContentLength];
                    image.InputStream.Read(_androidDto.AvatarImageData, 0, image.ContentLength);
                } else if (!android.ImageDisabled) {
                    var _image = this.GetImage(android.Id);
                    _androidDto.ImageMimeType = _image?.ContentType;
                    _androidDto.AvatarImageData = _image?.FileContents;
                }
                if (!_androidDto.IsOk) {
                    _androidDto.JobId = this._androidService.Get(_androidDto.Id).JobId;
                }
                try {
                    this._androidService.Update(_androidDto);
                    return RedirectToAction("Index", "Android");
                } catch (ValidationException ex) {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            } else {
                var _duplicatedModelState = ModelState.Values.SingleOrDefault(ms => ms.Errors.Count > 1 && ms.Errors[0].ErrorMessage == ms.Errors[1].ErrorMessage);
                if (_duplicatedModelState != null) {
                    _duplicatedModelState.Errors.Remove(_duplicatedModelState.Errors[1]);
                }
            }
            return View(android);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Remove(int androidId) {
            try {
                this._androidService.Remove(androidId);
            } catch (ValidationException ex) {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return RedirectToAction("Index", "Android");
        }

        [Authorize]
        public FileContentResult GetImage(int androidId) {
            var _androidDto = this._androidService.Get(androidId);
            if (_androidDto != null && _androidDto.AvatarImageData != null) {
                return File(_androidDto.AvatarImageData, _androidDto.ImageMimeType);
            } else {
                return null;
            }
        }
    }
}