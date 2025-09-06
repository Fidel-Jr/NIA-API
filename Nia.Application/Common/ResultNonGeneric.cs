using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nia.Application.Common
{
    public class ResultNonGeneric
    {
        public bool Succeeded { get; set; }
        public string? Message { get; set; }

        public static ResultNonGeneric Success(string? message = null)
        {
            return new ResultNonGeneric { Succeeded = true, Message = message };
        }

        public static ResultNonGeneric Failure(string message)
        {
            return new ResultNonGeneric { Succeeded = false, Message = message };
        }
    }
}
