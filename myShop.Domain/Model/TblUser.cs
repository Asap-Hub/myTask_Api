using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myShop.Domain.Model
{
    public class TblUser
    {
        public string? Email {  get; set; }  
        public string? PassWord {  get; set; }  
        public bool RememberMe {  get; set; }  

    }

    public class Login
    {
        public string? Email { get; set; }
        public string? PassWord { get; set; }

        public bool isPesistent = true;


    }
}


