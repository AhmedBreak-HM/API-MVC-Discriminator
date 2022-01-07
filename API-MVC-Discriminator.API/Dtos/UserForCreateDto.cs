using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.netCore_Identity.Dtos
{
    public class UserForCreateDto
    {

        [Required]
        public string UserName { get; set; }
        [StringLength(8, MinimumLength = 4, ErrorMessage = " The password must be no less than four characters and no more than eight  ")]
        [Required]
        public string Password { get; set; }
    }
}
