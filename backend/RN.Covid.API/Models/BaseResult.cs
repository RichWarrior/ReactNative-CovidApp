using System.Collections.Generic;
using System.Net;

namespace RN.Covid.API.Models
{
    public class BaseResult
    {
        public object Data { get; set; }
        public List<string> Messages { get; set; }
        public HttpStatusCode Status { get; set; }

        public BaseResult()
        {
            Messages = new List<string>();
            Status = HttpStatusCode.OK;
        }
    }
}
