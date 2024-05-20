using ApplicationService.AdminService;
using BusinessObjects.Admin;
using BusinessObjects.Common;
using Microsoft.AspNetCore.Mvc;
using System;

namespace courierManagement_backend.Controllers.AdminController
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AdminController : BaseController
    {
        [HttpPost]
        public GeneralResponse AddNewAdmin([FromBody] NewAdmin newAdmin)
        {
            try
            {
                Session session = this.GetSession();

                AdminApplicationService adminApplication = new AdminApplicationService(session);
                int id = adminApplication.AddNewAdmin(newAdmin);

                if (id > 0)
                {
                    return this.ResponseMessage(200, "New Admin successfully added", id);
                }
                else
                {
                    return this.ResponseMessage(200, "Fail", null);
                }
            }
            catch (Exception ex)
            {
                return this.ResponseMessage(500, "Exception", ex);
            }
        }

        [HttpPost]
        public GeneralResponse AddNewCompany([FromBody] NewCompany newCompany)
        {
            try
            {
                Session session = this.GetSession();

                AdminApplicationService adminApplication = new AdminApplicationService(session);
                int id = adminApplication.AddNewCompany(newCompany);

                if (id > 0)
                {
                    return this.ResponseMessage(200, "New Company successfully added", id);
                }
                else
                {
                    return this.ResponseMessage(200, "Fail", null);
                }
            }
            catch (Exception ex)
            {
                return this.ResponseMessage(500, "Exception", ex);
            }
        }
    }
}