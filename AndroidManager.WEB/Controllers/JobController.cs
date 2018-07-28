using AndroidManager.BLL.Dto;
using AndroidManager.BLL.Infrastructure;
using AndroidManager.BLL.Interfaces;
using AndroidManager.WEB.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AndroidManager.WEB.Controllers {

    public class JobController : Controller {
        private IJobService _jobService;

        public JobController(IJobService jobService) {
            this._jobService = jobService;
        }

        [Authorize]
        public ActionResult Index() {
            var _jobDtos = this._jobService.GetAll();
            var _mapper = new MapperConfiguration(cfg => cfg.CreateMap<JobDto, JobViewModel>()).CreateMapper();
            var _jobs = _mapper.Map<IEnumerable<JobDto>, List<JobViewModel>>(_jobDtos);
            return View(_jobs);
        }

        [Authorize]
        public ActionResult Create() {
            return View();
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(JobViewModel job) {
            if (ModelState.IsValid) {
                var _jobDto = new JobDto {
                    Name = job.Name,
                    Description = job.Description,
                    Complexity = job.Complexity
                };
                try {
                    this._jobService.Create(_jobDto);
                    return RedirectToAction("Index", "Job");
                } catch (ValidationException ex) {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            } else {
                var _duplicatedModelState = ModelState.Values.SingleOrDefault(ms => ms.Errors.Count > 1 && ms.Errors[0].ErrorMessage == ms.Errors[1].ErrorMessage);
                if (_duplicatedModelState != null) {
                    _duplicatedModelState.Errors.Remove(_duplicatedModelState.Errors[1]);
                }
            }
            return View(job);
        }

        [Authorize]
        public ActionResult Edit(int jobId) {
            var _jobDto = this._jobService.Get(jobId);
            var _job = new JobViewModel {
                Id = _jobDto.Id,
                Name = _jobDto.Name,
                Description = _jobDto.Description,
                Complexity = _jobDto.Complexity
            };
            return View(_job);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(JobViewModel job) {
            if (ModelState.IsValid) {
                var _jobDto = new JobDto {
                    Id = job.Id,
                    Name = job.Name,
                    Description = job.Description,
                    Complexity = job.Complexity
                };
                try {
                    this._jobService.Update(_jobDto);
                    return RedirectToAction("Index", "Job");
                } catch (ValidationException ex) {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            } else {
                var _duplicatedModelState = ModelState.Values.SingleOrDefault(ms => ms.Errors.Count > 1 && ms.Errors[0].ErrorMessage == ms.Errors[1].ErrorMessage);
                if (_duplicatedModelState != null) {
                    _duplicatedModelState.Errors.Remove(_duplicatedModelState.Errors[1]);
                }
            }
            return View(job);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Remove(int jobId) {
            try {
                this._jobService.Remove(jobId);
            } catch (ValidationException ex) {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return RedirectToAction("Index", "Job");
        }
    }
}