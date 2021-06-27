using System;
using System.Numerics;

namespace BDS.Services.Utilities
{
    public class UtilitiesService
    {
        public static long GenerateID()
        {
            var bytes = Guid.NewGuid().ToByteArray();
            Array.Resize(ref bytes, 5);
            var bigInt = new BigInteger(bytes);
            var id = long.Parse(bigInt.ToString());
            
            return id;
        }
    }
}