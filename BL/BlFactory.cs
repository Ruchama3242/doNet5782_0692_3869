using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   static public class BlFactory
    {
        static public  IBL.IBL GetBl()
        { 
            IBL.IBL bl = BlApi.BL.Instance;
            return bl;
        }
    }
}
