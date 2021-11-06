using System;
using System.Collections.Generic;
using System.Text;

namespace KazanBook.DAL
{
    public class Response<T>
    {
        public Boolean success = false;
        public T data;
        public string reason;
        internal static Response<T> Success(T data = default(T))
        {
            Response<T> resp = new Response<T>();
            resp.success = true;
            resp.data = data;
            return resp;
        }
        internal static Response<T> Fail(string reason, T data = default(T))
        {
            Response<T> resp = new Response<T>();
            resp.success = false;
            resp.reason = reason;
            resp.data = data;
            return resp;
        }
        internal static Response<T> NotFound() => Fail("Not Found");
    }
}
