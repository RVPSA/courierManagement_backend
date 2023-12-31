using Microsoft.AspNetCore.Mvc;
using System;

namespace courierManagement_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController { 

        [HttpGet]
        public String Get() {
            return "Courier Management Backend server start";
        }
    }
}
