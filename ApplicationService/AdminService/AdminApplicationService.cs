using BusinessObjects.Admin;
using BusinessObjects.Common;
using DAL;
using DAL.Admin;
using DAL.Interface.Admin;
using System;

namespace ApplicationService.AdminService
{
    public class AdminApplicationService
    {
        private Session session = new Session();

        public AdminApplicationService()
        { }

        public AdminApplicationService(Session userSession)
        {
            session = userSession;
        }

        public int AddNewAdmin(NewAdmin newAdmin)
        {
            IDataService dataService = DataServiceBuilder.CreateDataService();

            try
            {
                IAdminDataService adminData = new AdminDataService(dataService);

                string encryptPassword = BCrypt.Net.BCrypt.HashPassword(newAdmin.Password);
                newAdmin.Password = encryptPassword;

                int isAddedAdmin = adminData.AddNewAdmin(newAdmin, session.UserId);
                return isAddedAdmin;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataService.CloseConnection();
            }
        }

        public int AddNewCompany(NewCompany newCompany)
        {
            IDataService dataService = DataServiceBuilder.CreateDataService();

            try
            {
                IAdminDataService adminData = new AdminDataService(dataService);

                dataService.BeginTransaction();
                int resullt = adminData.AddNewCompany(newCompany, session.UserId);

                if (resullt > 0)
                {
                    dataService.CommitTransaction();
                }
                else
                {
                    dataService.RollbackTransaction();
                }
                return resullt;
            }
            catch (Exception ex)
            {
                dataService.RollbackTransaction();
                throw ex;
            }
            finally
            {
                dataService.CloseConnection();
            }
        }
    }
}