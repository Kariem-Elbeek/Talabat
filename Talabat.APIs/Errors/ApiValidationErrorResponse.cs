using System.Collections;
using System.Collections.Generic;

namespace Talabat.APIs.Errors
{
    public class ApiValidationErrorResponse:ApiResponses
    {
        public ApiValidationErrorResponse():base(400)
        {
        }

        public IEnumerable<string> Errors { get; set; }
    }
}
