using BDS.Services.Area;
using BDS.Services.Facades.Models;
using BDS.Services.Project;
using BDS.Services.RealEstate;
using BDS.Services.User;
using BDS.Services.Wishlist;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BDS.Services.Facades.Services
{
    public class AreaFacade : IAreaFacade
    {
        private readonly IAreaService _areaService;
        private readonly IProjectService _projectService;
        private readonly IRealEstateService _realEstateService;
        private readonly IWishlistService _wishlistService;
        private readonly IUserService _userService;
        public AreaFacade(IServiceProvider serviceProvider,IAreaServiceAbstractFactory areaAbstractFactory, IProjectAbstractFactory projectAbstractFactory,
            IRealEstateServiceAbstractFactory realEstateAbstractFactory, IWishlistServiceAbtractFactory wishlistServiceAbtractFactory,
            IUserServiceAbstractFactory userServiceAbstractFactory) 
        {
            areaAbstractFactory= serviceProvider.GetServices<IAreaServiceAbstractFactory>().FirstOrDefault(f => f.FactoryName == "AreaServices");

            _areaService = areaAbstractFactory.CreateAreaService();
            _projectService = projectAbstractFactory.CreateProjectServices();
            _realEstateService = realEstateAbstractFactory.CreateRealEstateService();
            _wishlistService = wishlistServiceAbtractFactory.CreateWishlistService();
            _userService = userServiceAbstractFactory.CreateUserService();
        }
        public async Task<AreaFacadeModel> GetAreaService(ClaimsPrincipal User, long id, string name, int pageIndex = 1)
        {
            AreaFacadeModel result = new AreaFacadeModel();
            result.ListProject = _projectService.GetHighlightProject().Result;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                long userId = _userService.GetUserId(User);
                result.ListWishList = _wishlistService.GetByUserId(userId).Result;
            }

            result.ListRealEstate = _realEstateService.GetByAreaId(id, pageIndex, 6).Result;
            return result;
        }
    }
}
