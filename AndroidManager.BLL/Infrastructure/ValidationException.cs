
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidManager.BLL.Infrastructure {

    public class ValidationException : Exception {
        public string Property { get; private set; }

        public ValidationException(string message, string property) : base(message) {
            this.Property = property;
        }
    }
}
