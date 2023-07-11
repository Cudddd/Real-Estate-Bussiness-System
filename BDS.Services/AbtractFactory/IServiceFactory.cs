using BDS.Services.Area;
using BDS.Services.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Services.AbtractFactory
{
    public interface IServiceFactory
    {
        public IAreaService CreateAreaService(Area type);
        public IProjectService CreateProjectService();
    }
    public enum Area
    {
        V1,
        V2
    }
}
