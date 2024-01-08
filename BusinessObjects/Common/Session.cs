using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects.Common
{
    public class Session
    {
        public int UserId { get; set; }
        public int UserRoleId { get; set; }
        public string UserName { get; set; }
        public string Cookie { get; set; }
    }
}
