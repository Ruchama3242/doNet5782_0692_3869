﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    using System.Runtime.Serialization;

    [Serializable]

    public class generalException : Exception
    {
        public generalException() : base("ERROR") { }
        public generalException(string message) : base(message) { }
        public generalException(string message, Exception innerException) : base(message, innerException) { }

        override public string ToString()
        { return Message; }
    }
    public class IdExistsException : Exception
    {
        public IdExistsException() : base("The ID already Exists") { }
        public IdExistsException(string message) : base(message) { }
        public IdExistsException(string message, Exception innerException) : base(message, innerException) { }

        override public string ToString()
        { return Message; }
    }

    public class IdUnExistsException : Exception
    {
        public IdUnExistsException() : base("The ID don't found") { }
        public IdUnExistsException(string message) : base(message) { }
        public IdUnExistsException(string message, Exception innerException) : base(message, innerException) { }

        override public string ToString()
        { return Message; }
    }

}

