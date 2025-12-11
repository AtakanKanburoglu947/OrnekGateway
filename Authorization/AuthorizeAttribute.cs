using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization
{
    public class AuthorizeAttribute : Microsoft.AspNetCore.Authorization.AuthorizeAttribute
    {
        public UserClaimEnum[] UserClaimEnums { get; set; }
        public AuthorizeAttribute(params UserClaimEnum[] userClaimEnums)
        {
            UserClaimEnums = userClaimEnums;

        }
    }
}
