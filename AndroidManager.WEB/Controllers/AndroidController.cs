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
                                .ForMember("JobName", opt => opt.MapFrom(a => this._jobService.Get(a.JobId).Name))).CreateMapper();
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
        public ActionResult Create(AndroidViewModel android) {
            if (ModelState.IsValid) {
                var _androidDto = new AndroidDto {
                    Name = android.Name,
                    Avatar = android.Avatar,
                    Skills = android.Skills,
                    JobId = android.JobId
                };
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
                Avatar = _androidDto.Avatar,
                Skills = _androidDto.Skills,
                JobId = _androidDto.JobId,
                IsOk = _androidDto.IsOk,
                JobsList = new SelectList(this._jobService.GetAll(), "Id", "Name"),
                Reliability = _androidDto.Reliability
            };
            return View(_adroid);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(AndroidViewModel android) {
            if (ModelState.IsValid) {
                var _androidDto = new AndroidDto {
                    Id = android.Id,
                    Name = android.Name,
                    Avatar = android.Avatar,
                    Skills = android.Skills,
                    JobId = android.JobId,
                    IsOk = android.IsOk,
                    Reliability = android.Reliability
                };
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
        public ActionResult Remove(int adnroidId) {
            try {
                this._androidService.Remove(adnroidId);
            } catch (ValidationException ex) {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return RedirectToAction("Index", "Android");
        }
    }
}