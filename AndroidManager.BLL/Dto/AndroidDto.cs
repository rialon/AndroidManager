﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidManager.BLL.Dto {

    public class AndroidDto {
        public int Id { get; set; }
        public string Name { get; set; }
#warning Image
        public string Avatar { get; set; }
        public string Skills { get; set; }
        public int Reliability { get; set; }
        public bool IsOk { get; set; }
        public int? JobId { get; set; }
    }
}
