using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidManager.DAL.Entities {

    public class Job {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Complexity { get; set; }

        public virtual ICollection<Android> Androids { get; set; }
    }
}
