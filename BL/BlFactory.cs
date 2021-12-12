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

            //BlApi.BL bl = BlApi.BL.Instance;
            IBL.IBL bl = BlApi.BL.Instance;

            return bl;
        }
    }
}
