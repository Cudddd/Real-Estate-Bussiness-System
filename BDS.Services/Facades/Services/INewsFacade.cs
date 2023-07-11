using BDS.Services.Facades.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Services.Facades.Services
{
    public interface INewsFacade
    {
        Task<GetNewsModelFacade> GetNews(ClaimsPrincipal User,int pageIndex = 1);
        Task<NewModelDetail> GetNewsDetail(ClaimsPrincipal User, long id);
    }
}
