using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientFactory
{
    public static class SessionInfos
    {
        public static User login_user;

        static SessionInfos()
        {
            try
            {


                SessionInfos.login_user.Id = 1;
                SessionInfos.login_user.FirstName = "ehsan";
                SessionInfos.login_user.LastName = "gol";
                SessionInfos.login_user.RoleName = "boss";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
    }
}
