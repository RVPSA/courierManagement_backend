using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects.Common
{
    public class GeneralResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public object Result { get; set; }
    }
}
