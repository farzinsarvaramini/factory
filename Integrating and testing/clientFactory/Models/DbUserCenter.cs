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

        public bool AddUser(User usr, int masterId)
        {
            User user = _container.Users.Create();
            user.FirstName = usr.FirstName;
            user.LastName = usr.LastName;
            user.Email = usr.Email;
            user.RoleId = usr.RoleId;
            user.RoleName = usr.RoleName;
            user.Age = usr.Age;
            user.EmploymentDate = usr.EmploymentDate;
            user.Resume = usr.Resume;
            user.Password = usr.Password;
            user.AvatarLocation = usr.AvatarLocation;
            user.NationalId = usr.NationalId;
            user.PhoneNumber = usr.PhoneNumber;
            user.Address = usr.Address;
            user.Gender = usr.Gender;
            user.Username = usr.Username;
            user.DefaultUser = usr.DefaultUser;
            user.UserId = masterId;

            _container.Users.Add(user);

            try
            {
                _container.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public void SetDefaultUser(int user_id)
        {
            var user = _container.Users.Where(u => u.DefaultUser == true).First();
            user.DefaultUser = false;
            _container.Users.Where(u => u.UserId == user_id).First().DefaultUser = true;

            try
            {
                _container.SaveChanges();
            }
            catch
            {
                Console.WriteLine("Can't update");
            }
        }

        public User GetUserDetails(int user_id)
        {
            var user = _container.Users.Where(u => u.Id == user_id).First();
            return user;
        }

        public User GetDefaultUser()
        {
            var user = _container.Users.Where(u => u.DefaultUser == true).First();
            return user;
        }

        public List<User> GetNewUsers()
        {
            var user = _container.Users.Where(u => u.IsNew == true).ToList();
            return user;
        }
    }
}
