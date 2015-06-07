using System;
using System.Collections.Generic;

namespace Database
{
	public class DbUserCenter
	{

		// singleton pattern
		public DbUserCenter Instanse;

		public DbUserCenter ()
		{
		}

		/*
		 * this function add a user to database
		 * this funtion implements in both of client and server
		 */
		public bool AddUser(User usr);

		/*
		 * this function returns details of one user
		 * this funtion implements in both of client and server
		 */
		public User GetUserDetails(int user_id);

		/*
		 * this function return list of new users which create by Admin that related to user_id 
		 * this function implement only for server
		 */
		public List<User> GetNewUsers (int user_id);

		/*
		 * this fuction set a user as default user
		 * this function must change IsDefault field in database to true
		 * this function implement only for client
		 */
		public void SetDefaultUser(int user_id);

		/*
		 * this function return Default user
		 * this function implement only for client
		 */
		public User GetDefaultUser();
	}
}

