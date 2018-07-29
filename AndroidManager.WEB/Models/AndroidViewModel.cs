using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AndroidManager.WEB.Models {

    public class AndroidViewModel {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Name of the android")]
        [RegularExpression("([a-zA-Z0-9\\-]+)", ErrorMessage = "Only alphanumeric with hyphen symbols are allowed for android name")]
        [StringLength(24, MinimumLength = 5, ErrorMessage = "From 5 to 24 symbols are allowed for android name")]
        public string Name { get; set; }

        public string Skills { get; set; }

        [Display(Name = "Ready")]
        public bool IsOk { get; set; }

        public int Reliability { get; set; }

        public int? JobId { get; set; }

        [Display(Name = "Job Name")]
        public string JobName { get; set; }

        public IEnumerable<SelectListItem> JobsList { get; set; }

        [Display(Name = "Avatar")]
        public byte[] AvatarImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}