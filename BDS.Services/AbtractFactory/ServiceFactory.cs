using BDS.Services.Area;
using BDS.Services.Project;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Services.AbtractFactory
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public ServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IAreaService CreateAreaService(Area type)
        {
            IAreaService areaService;
            if (type.Equals(Area.V1))
            {
                areaService = _serviceProvider.GetService<IAreaService>();
                return areaService;
            }
            return null;
        }

        public IProjectService CreateProjectService()
        {
            throw new NotImplementedException();
        }
    }
}
