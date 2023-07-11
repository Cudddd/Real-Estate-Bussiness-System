using BDS.Services.Facades.Models;
using BDS.Services.Project;
using BDS.Services.User;
using BDS.Services.Wishlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Services.Facades.Services
{
    public class HomeFacade : IHomeFacade
    {
        private readonly IProjectService _projectService;
        private readonly IWishlistService _wishlistService;
        private readonly IUserService _userService;
        public HomeFacade(
           IProjectAbstractFactory projectAbstractFactory,
           IWishlistServiceAbtractFactory wishlistServiceAbtractFactory,
           IUserServiceAbstractFactory userServiceAbstractFactory
        )
        {
            _projectService = projectAbstractFactory.CreateProjectServices();
            _wishlistService = wishlistServiceAbtractFactory.CreateWishlistService();
            _userService = userServiceAbstractFactory.CreateUserService();
        }
        public async Task<HomeFacadeModel> GetData(ClaimsPrincipal User)
        {
            HomeFacadeModel result= new HomeFacadeModel();
            result.ListProjects = _projectService.GetHighlightProject().Result;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                long userId = _userService.GetUserId(User);
                result.ListWishList = _wishlistService.GetByUserId(userId).Result;
            }

            return result;
        }
    }
}
