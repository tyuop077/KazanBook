using System;
using System.Collections.Generic;
using System.Text;

namespace KazanBook.BAL.Schema
{
    internal class Response<T>
    {
        internal Boolean success;
        internal T data;
        internal string reason;
    }
}
