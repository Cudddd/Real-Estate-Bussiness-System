using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Services.Project
{
    public interface IProjectAbstractFactory
    {
        IProjectService CreateProjectServices();

    }
}
