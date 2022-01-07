using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.netCore_Identity.Dtos
{
    public class UserForReturnDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public List<string> RolesString { get; set; }
        public List<RoleForUserDto> Roles { get; set; }
        public string Token { get; set; }

    }
}
