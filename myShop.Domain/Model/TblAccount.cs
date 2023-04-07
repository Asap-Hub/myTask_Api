using System;
using System.Collections.Generic;

namespace myShop.Domain.Model;

public partial class TblAccount
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string SecondName { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string email { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string CountryName { get; set; } = null!;

    public string FirstPassword { get; set; } = null!;

    public string ConfirmPassword { get; set; } = null!; 
}
