using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientFactory
{
    public class DbUserCenter
    {
        private clientContainer _container;
        public DbUserCenter(clientContainer con)
        {
            _container = con;
        }

        public bool AddUser(User usr)
        {
            return true;
        }

        public void SetDefaultUser(int user_id)
        {

        }

        public User GetUserDetails(int user_id)
        {

            return null;
        }

        public User GetDefaultUser()
        {



            return null;
        }

        public List<User> GetNewUsers()
        {
            return null;
        }



    }
}
