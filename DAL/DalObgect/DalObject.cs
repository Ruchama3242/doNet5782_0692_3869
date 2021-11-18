using System;
using IDAL.DO;
using DAL;
using System.Collections.Generic;
using System.Collections;
namespace IDAL

{
  namespace DO
  { 
      namespace DalObject
        {
             public partial class DalObject:IDal
            {
                /// <summary>
                /// constructor
                /// </summary>
                public DalObject() { DataSource.Initialize(); }

            }
        }
    }

}

