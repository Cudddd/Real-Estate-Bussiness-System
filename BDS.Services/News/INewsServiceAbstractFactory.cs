﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Services.News
{
    public interface INewsServiceAbstractFactory
    {
        INewsService CreateNewService();
    }
}