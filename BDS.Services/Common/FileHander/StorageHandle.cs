using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Services.Common.FileHander
{
    public class MiniStorage : IFileHander
    {
        public IFileHander Successor { get; set; }

        public void ProcessFile(long size)
        {
            if (size < 1000)
            {

            }
            else
            {
                Successor.ProcessFile(size);
            }
        }
    }

    public class MediumStorage : IFileHander
    {
        public IFileHander Successor { get; set; }

        public void ProcessFile(long size)
        {
            if (size < 10000)
            {

            }
            else
            {
                Successor.ProcessFile(size);
            }
        }
    }

    public class BigStorage : IFileHander
    {
        public IFileHander Successor { get; set; }

        public void ProcessFile(long size)
        {
            if (size < 100000)
            {

            }
            else
            {
                Successor.ProcessFile(size);
            }
        }


        class FactoryHandler : IFileHander
        {
            public IFileHander Successor { get; set; }

            public void ProcessFile(long size)
            {
                Console.WriteLine($"Factory: I received the request. You will receice product from us");

            }
        }

        class ChainOfHandlers
        {
            readonly IFileHander _mini = new MiniStorage();
            readonly IFileHander _medium = new MediumStorage();
            readonly IFileHander _big = new BigStorage();
            readonly IFileHander _factory = new FactoryHandler();

            public ChainOfHandlers()
            {
                _mini.Successor = _medium;
                _medium.Successor = _big;
                _big.Successor = _factory;
            }

            public void Handle(int size)
            {
                _mini.ProcessFile(size);
            }
        }
    }

}
