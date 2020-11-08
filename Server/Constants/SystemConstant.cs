using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Constants
{
    public static class SystemConstant
    {
        public const string Issuer = Audience;
        public const string Audience = "http://localhost:49191";
        public const string Secret = "not_too_short_secret_otherwise_it_might_error";
        public const int TokenExpMin = 20;


    }
}
