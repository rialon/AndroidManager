using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AndroidManager.WEB.Models {

    public class JobViewModel {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Name of the job")]
        [RegularExpression("([a-zA-Z0-9\\-]+)", ErrorMessage = "Only alphanumeric with hyphen symbols are allowed for job name")]
        [StringLength(16, MinimumLength = 2, ErrorMessage = "From 2 to 16 symbols are allowed for job name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter Description of the job")]
        [StringLength(255, ErrorMessage = "More than 255 symbols are not allowed for job description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter Complexity of the job")]
        [Range(0, 10, ErrorMessage = "Values from 0 to 10 are allowed for complexity of the job")]
        public int Complexity { get; set; }
    }
}