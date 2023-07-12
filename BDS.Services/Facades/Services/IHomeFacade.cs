using BDS.Services.Facades.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Services.Facades.Services
{
    public interface IHomeFacade
    {
        Task<HomeFacadeModel> GetData(ClaimsPrincipal User);
    }
}
