using BDS.Data.EF;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Services.User
{
    using Data.Entities;
    public class UserServiceAbstractFactory : IUserServiceAbstractFactory
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly BdsDbContext _context;
        public UserServiceAbstractFactory(UserManager<User> userManager, SignInManager<User> signInManager, BdsDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public IUserService CreateUserService()
        {
            return new UserService(_userManager, _signInManager, _context);
        }
    }
}
