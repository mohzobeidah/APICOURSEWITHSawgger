using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPISwagger2.Contracts.v1
{
    public  static class ApiRoutes
    {
        public const string Roote = "Api";
        public const string Version = "v1";
        public  const string Base = Roote+"/"+Version+"/";


        public static class Posts
        {
            public  const  string GetAll = Base+"posts";
            public const string Create = Base +" posts";
            public const string Get = Base+"post/{postId}";
            public const string Upate = Base+"posts/{postId}";
            public const string Delete = Base+ "posts/{postId}";
        }


        public static  class Identity
        {
            public const string register = Base + "Identity/register";
            public const string login = Base + "Identity/Login";

        }
    }
}
