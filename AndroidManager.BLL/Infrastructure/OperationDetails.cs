using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidManager.BLL.Infrastructure {

    public class OperationDetails {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public string Property { get; set; }

        public OperationDetails(bool succeeded, string message, string property) {
            this.Succeeded = succeeded;
            this.Message = message;
            this.Property = property;
        }
    }
}
