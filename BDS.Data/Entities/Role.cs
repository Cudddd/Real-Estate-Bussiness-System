using Microsoft.AspNetCore.Identity;

namespace BDS.Data.Entities
{
    public class Role : IdentityRole<long>
    {
        public  string Description { get; set; }
    }
}