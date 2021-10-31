﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using KazanBook.BAL.Schema;

namespace KazanBook.BAL
{
    class Passer
    {
        public static object Pass<T>(Response<T> resp)
        {
            if (resp.success)
            {
                return resp.data;
            }
            else
            {
                throw new DALError(resp.reason);
            }
        }
        public static string DBString(string str)
        {
            if (str is null)
            {
                return "NULL";
            }
            else
            {
                return $"'{str.Replace("'", "''")}'"; // a bit of security
            }
        }
        public static string[] DBArray(string[] str)
        {
            return str.Select(tag => tag.Replace(";", "")).ToArray();
        }
        public static T Convert<T>(object obj)
        {
            return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(obj));
        }
    }
    public class DALError : Exception
    {
        public object reason;

        public DALError(string reason) : base(reason)
        {
            this.reason = reason;
        }
    }
}
