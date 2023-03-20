using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Framework.Infrastructure
{
    public static class Roles
    {
        public const string Administrator = "1";
        public const string SystemUser = "2";
        public const string ContentUploader = "3";
        public const string ColleagueUser = "10002";



        public static string GetRoleBy(long id)
        {
            switch (id)
            {
                case 1: return "مدیر سیستم";
                case 3: return "محتوا گزار";
                default: return "";
            }
        }
    }
}
