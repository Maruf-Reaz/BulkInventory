﻿
using Microsoft.AspNetCore.Identity;


namespace InquiadTradingApp.Models.Common.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public int UserTypeId { get; set; }
        public UserType UserType { get; set; }

        public byte Status { get; set; }
        
    }
}
