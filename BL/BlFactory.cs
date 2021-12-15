using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   static public class BlFactory
    {
        static public  BlApi.IBL GetBl()
        { 
            BlApi.IBL bl = BlApi.BL.Instance;
            return bl;
        }
    }
}
