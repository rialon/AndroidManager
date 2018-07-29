using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidManager.DAL.Entities {

    public class Android {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Skills { get; set; }
        public int Reliability { get; set; }
        public int Status { get; set; }

        public int? JobId { get; set; }
        public virtual Job Job { get; set; }

        public byte[] AvatarImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
