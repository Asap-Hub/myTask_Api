using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myShop.Application.Dto.User
{
    public class loginDto
    { 
        public string UserName { get; set; } = null!; 

        public string FirstPassword { get; set; } = null!;

        public string ConfirmPassword { get; set; } = null!;
    }
}
