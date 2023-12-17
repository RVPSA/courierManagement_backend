using BusinessObjects.Common;
using courierManagement_backend.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace courierManagement_backend
{
    [EnableCors("AllowSpecificOrigin")]
    [ActionFilter]
    public class BaseController: Controller
    {
        public GeneralResponse ResponseMessage(int statusCode, string message, object result) {

            GeneralResponse general = new GeneralResponse()
            {
                StatusCode = statusCode,
                Message = message,
                Result = result,
            };
            return general;
        }
    }
}
