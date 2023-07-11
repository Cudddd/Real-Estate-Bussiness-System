using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Services.Common.FileHander
{
    public interface IFileHander
    {
        IFileHander Successor { get; set; }

        void ProcessFile(long size);
    }
}
