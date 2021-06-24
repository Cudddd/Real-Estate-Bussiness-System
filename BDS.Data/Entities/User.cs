using System;
using Microsoft.AspNetCore.Identity;

namespace BDS.Data.Entities
{
    public class User : IdentityUser<long>
    {
        public  DateTime DoB { get; set; }
    }
}