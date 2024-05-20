using BusinessObjects.Admin;
using DAL.Interface.Admin;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DAL.Admin
{
    public class AdminDataService : IAdminDataService
    {
        IDataService _dataService;
        public AdminDataService(IDataService dataService) {
            _dataService = dataService;
        }

        public int AddNewAdmin(NewAdmin newAdmin,int addedBy) {
            try {

                DbParameter[] dbParameters = new DbParameter[5];

                dbParameters[0] = DataServiceBuilder.CreateDBParameter("@userName",System.Data.DbType.String,System.Data.ParameterDirection.Input,newAdmin.UserName);
                dbParameters[1] = DataServiceBuilder.CreateDBParameter("@password", System.Data.DbType.String, System.Data.ParameterDirection.Input, newAdmin.Password);
                dbParameters[2] = DataServiceBuilder.CreateDBParameter("@email", System.Data.DbType.String, System.Data.ParameterDirection.Input, newAdmin.Email);
                dbParameters[3] = DataServiceBuilder.CreateDBParameter("@addedBy", System.Data.DbType.Int32,System.Data.ParameterDirection.Input, addedBy);
                dbParameters[4] = DataServiceBuilder.CreateDBParameter("@newAdminId", System.Data.DbType.Int32, System.Data.ParameterDirection.Output, 0);

                _dataService.ExecuteNonQuery("[Admin].[AddNewAdmin]",dbParameters,60);

                return Convert.ToInt32(dbParameters[4].Value);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public int AddNewCompany(NewCompany newCompany, int addedBy) {
            try {
                DbParameter[] dbParameters = new DbParameter[12];

                dbParameters[0] = DataServiceBuilder.CreateDBParameter("@companyName",System.Data.DbType.String,System.Data.ParameterDirection.Input,newCompany.CompanyName);
                dbParameters[1] = DataServiceBuilder.CreateDBParameter("@registerNumber", System.Data.DbType.String, System.Data.ParameterDirection.Input, newCompany.RegisteredNumber);
                dbParameters[2] = DataServiceBuilder.CreateDBParameter("@province", System.Data.DbType.String, System.Data.ParameterDirection.Input, newCompany.Province);
                dbParameters[3] = DataServiceBuilder.CreateDBParameter("@streetNumber", System.Data.DbType.String, System.Data.ParameterDirection.Input, newCompany.StreetNumber);
                dbParameters[4] = DataServiceBuilder.CreateDBParameter("@streetName", System.Data.DbType.String, System.Data.ParameterDirection.Input, newCompany.StreetName);

                dbParameters[5] = DataServiceBuilder.CreateDBParameter("@city", System.Data.DbType.String, System.Data.ParameterDirection.Input, newCompany.City);
                dbParameters[6] = DataServiceBuilder.CreateDBParameter("@contactNumber", System.Data.DbType.String, System.Data.ParameterDirection.Input, newCompany.ContactNumber);
                dbParameters[7] = DataServiceBuilder.CreateDBParameter("@email", System.Data.DbType.String, System.Data.ParameterDirection.Input, newCompany.Email);
                dbParameters[8] = DataServiceBuilder.CreateDBParameter("@lat", System.Data.DbType.Decimal, System.Data.ParameterDirection.Input, newCompany.Lat);
                dbParameters[9] = DataServiceBuilder.CreateDBParameter("@lan", System.Data.DbType.Decimal, System.Data.ParameterDirection.Input, newCompany.Lan);

                dbParameters[10] = DataServiceBuilder.CreateDBParameter("@addedBy", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, addedBy);
                dbParameters[11] = DataServiceBuilder.CreateDBParameter("@companyId", System.Data.DbType.Int32, System.Data.ParameterDirection.Output, 0);

                _dataService.ExecuteNonQuery("[Admin].[AddNewCompany]",dbParameters,60);

                return Convert.ToInt32(dbParameters[11].Value);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
