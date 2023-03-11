using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExchange
{
    public static class CurrentUser
    {
        /*
        public CurrentUser(CurrentUser user)
        {
             this = user;
        }

        public CurrentUser(int id, string name, string code, int role) : this()
        {
            currentUserid = id;
            currentUserName = name;
            currentUserCode = code;
            currentUserRole = role;
        }
       
        private static int currentUserid;
        private static string currentUserName;
        private static string currentUserCode;
        private static int currentUserRole;
        */
        public static long CurrentUserId { get; set; }
        public static string CurrentUserName { get; set; }
        public static string CurrentUserCode { get; set; }
        public static long CurrentUserRole { get; set; }
        public static long CurrentUserCentre { get; set; }

        public static long CurrentUserZona { get; set; }
        public static long CurrentUserShift { get; set; }

    }
}
