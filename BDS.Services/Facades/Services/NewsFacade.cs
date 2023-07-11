using BDS.Services.Facades.Models;
using BDS.Services.News;
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

    public class NewsFacade : INewsFacade
    {
        private readonly INewsService _newsService;
        private readonly IProjectService _projectService;
        private readonly IWishlistService _wishlistService;
        private readonly IUserService _userService;

        public NewsFacade(INewsServiceAbstractFactory newsAbstractFactory, IProjectAbstractFactory projectAbstractFactory,
            IWishlistServiceAbtractFactory wishlistAbtractFactory, IUserServiceAbstractFactory userAbstractFactory)
        {
            _newsService = newsAbstractFactory.CreateNewService();
            _projectService = projectAbstractFactory.CreateProjectServices();
            _wishlistService = wishlistAbtractFactory.CreateWishlistService();
            _userService = userAbstractFactory.CreateUserService();
        }
        public async Task<GetNewsModelFacade> GetNews(ClaimsPrincipal User, int pageIndex = 1)
        {
            GetNewsModelFacade result = new GetNewsModelFacade();
            result.ListProjects = _projectService.GetHighlightProject().Result;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                long userId = _userService.GetUserId(User);
                result.ListWishList = _wishlistService.GetByUserId(userId).Result;
            }
            result.ListNews = _newsService.GetAllPaging(pageIndex, 4).Result;

            return result;
        }

        public async Task<NewModelDetail> GetNewsDetail(ClaimsPrincipal User, long id)
        {
            NewModelDetail result = new NewModelDetail();
            result.ListProjects = _projectService.GetHighlightProject().Result;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                long userId = _userService.GetUserId(User);
                result.ListWishList = _wishlistService.GetByUserId(userId).Result;
            }

            result.New = _newsService.GetById(id).Result;

            return result;
        }
    }
}
