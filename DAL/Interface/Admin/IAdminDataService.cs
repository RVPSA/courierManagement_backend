using BusinessObjects.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interface.Admin
{
    public interface IAdminDataService
    {
        int AddNewAdmin(NewAdmin newAdmin,int addedBy);

        int AddNewCompany(NewCompany newCompany, int addedBy);
    }
}
