using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public static class BlFactory
    {
        public static IBL GetBl() => BL.BL.Instance;
    }
}


//namespace BL
//{
//    static public class BlFactory
//    {
//        static public BlApi.IBL GetBl()
//        {
//            BlApi.IBL bl = BL.Instance;
//            return bl;
//        }
//    }
//}
