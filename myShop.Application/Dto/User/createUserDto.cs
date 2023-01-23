using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myShop.Application.Dto.User
{
    public class CreateUserDto
    {
        public string FirstName { get; set; } = null!;

        public string SecondName { get; set; } = null!;

        public string UserName { get; set; } = null!;
        public string email { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public string CountryName { get; set; } = null!;

        public string FirstPassword { get; set; } = null!;

        public string ConfirmPassword { get; set; } = null!; 
    }
}
