using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Services.Area
{
    public class AreaServicesFileAbstractFactory : IAreaServiceAbstractFactory
    {
        public string FactoryName => "AreaServicesFile";
        public IAreaService CreateAreaService()
        {
            throw new NotImplementedException();
        }
    }
}
