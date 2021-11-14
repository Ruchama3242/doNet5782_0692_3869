using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        using System.Runtime.Serialization;
 
[Serializable]
    public class MyException : Exception
    {
        public MyException() : base() { }
        public MyException(string message) : base(message) { }
        override public string ToString()
        { return  Message; }
    }
      
    }
    
}
