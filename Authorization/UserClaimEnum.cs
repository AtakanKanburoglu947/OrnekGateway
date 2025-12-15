using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization
{
    public enum UserClaimEnum
    {
        StandardUser = 1,
        Admin = 2,
        CanViewUsers = 3,
        CanAddUsers = 4,
        Seller = 5,
       
    }
}
