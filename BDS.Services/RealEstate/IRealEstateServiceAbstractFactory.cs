using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Services.RealEstate
{
    public interface IRealEstateServiceAbstractFactory
    {
        string FactoryName { get; }
        IRealEstateService CreateRealEstateService();
    }
}
