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
        CanViewUsers = 2,
        CanAddUsers = 3,
        Seller = 4,
        Admin = 5,
    }
}
