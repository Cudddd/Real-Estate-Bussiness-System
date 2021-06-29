using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BDS.Data.EF;
using BDS.Services.Model;
using BDS.Services.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BDS.Services.User
{
    using Data.Entities;
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly BdsDbContext _context;
        public UserService(UserManager<User> userManager, SignInManager<User> signInManager,BdsDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public async Task<bool> Authenticate(string userName, string password, bool rememberMe)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return false;

            
            
            var result = await _signInManager.PasswordSignInAsync(user, password, rememberMe, false);

            if (!result.Succeeded) return false;

            return true;

            /*var roles = _userManager.GetRolesAsync(user);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role,string.Join(";",roles))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("0123456789ABCDEF"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken("123456789ABCDEF",
                "123456789ABCDEF",
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);*/
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> Register(UserCreateRequest request)
        {
            var user = new User()
            {
                Id = Utilities.UtilitiesService.GenerateID(),
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
            };
            IdentityResult result;
            if(request.Password == request.ConfirmPassword)
            {
                result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded) return true;
            }

            return false;
        }

        public async Task<User> GetCurrentUser(ClaimsPrincipal User)
        {
            User user = await _userManager.GetUserAsync(User);
            return user;
        }

        public long GetUserId(ClaimsPrincipal User)
        {
            string userId =  _userManager.GetUserId(User);
            return long.Parse(userId);
        }

        public async Task<List<UserRealEstateModel>> GetAllUserRealEstate(long userId)
        {
            var data = await _context.UserRealEstate.ToListAsync();

            List<UserRealEstateModel> userRealEstateModels = new List<UserRealEstateModel>();
            foreach (var item in data)
            {
                UserRealEstateModel userRealEstateModel = new UserRealEstateModel();
                userRealEstateModel.id = item.id;
                userRealEstateModel.UserId = item.UserId;
                userRealEstateModel.name = item.name;
                userRealEstateModel.sell = item.sell;
                userRealEstateModel.length = item.length;
                userRealEstateModel.width = item.width;
                userRealEstateModel.orientation = item.orientation;
                userRealEstateModel.acreage = item.acreage;
                userRealEstateModel.price = item.price;
                userRealEstateModel.location = item.location;
                userRealEstateModel.type = _context.RealEstateType.FirstOrDefault(x => x.id == item.typeId)?.name;
                userRealEstateModel.facade = item.facade;
                userRealEstateModel.mainLine = item.mainLine;
                userRealEstateModel.floor = item.floor;
                userRealEstateModel.bedRoom = item.bedRoom;
                userRealEstateModel.bathRoom = item.bathRoom;
                userRealEstateModel.DateCreated = item.DateCreated;
                userRealEstateModel.DateModify = item.DateModify;
                userRealEstateModel.description = item.description;
                userRealEstateModel.address = item.address;
                userRealEstateModel.userRealEstateMedia = 
                    await _context.UserRealEstateMedia.Where(x => x.UserRealEstateId == item.id).ToListAsync();
                
                userRealEstateModels.Add(userRealEstateModel);
            }
            
            return userRealEstateModels;
        }

        public async Task<UserRealEstateModel> GetUserRealEstateById(long id)
        {
            var item = await _context.UserRealEstate.FirstOrDefaultAsync(x=>x.id == id);
            
            UserRealEstateModel userRealEstateModel = new UserRealEstateModel();
            userRealEstateModel.id = item.id;
            userRealEstateModel.UserId = item.UserId;
            userRealEstateModel.name = item.name;
            userRealEstateModel.sell = item.sell;
            userRealEstateModel.length = item.length;
            userRealEstateModel.width = item.width;
            userRealEstateModel.orientation = item.orientation;
            userRealEstateModel.acreage = item.acreage;
            userRealEstateModel.price = item.price;
            userRealEstateModel.location = item.location;
            userRealEstateModel.type = _context.RealEstateType.FirstOrDefault(x => x.id == item.typeId)?.name;
            userRealEstateModel.facade = item.facade;
            userRealEstateModel.mainLine = item.mainLine;
            userRealEstateModel.floor = item.floor;
            userRealEstateModel.bedRoom = item.bedRoom;
            userRealEstateModel.bathRoom = item.bathRoom;
            userRealEstateModel.DateCreated = item.DateCreated;
            userRealEstateModel.DateModify = item.DateModify;
            userRealEstateModel.description = item.description;
            userRealEstateModel.address = item.address;
            userRealEstateModel.userRealEstateMedia =
                await _context.UserRealEstateMedia.Where(x => x.UserRealEstateId == id).ToListAsync();
            
            return userRealEstateModel;
        }
    }
}