using System;

namespace Database
{
	enum Event {
		NEW_REPORT
	}

	struct Message {
		int success; // 0 for success and 1 for fail
		string message;
	}

	public class DbUserCenter
	{

		// singleton pattern
		public DbUserCenter Instanse;

		public DbUserCenter ()
		{
		}

		/*
		 * this function return a user new events
		 * if user hasn't new event return empty array
		 * checked flag table in database
		 */
		public Event[] GetNew (int user_id);

		/*
		 * this function check username and password for login and return a message struct
		 * error massage: Dont exist username
		 * error message: Dont match password
		 * success message: ok
		 */
		public Message LoginCheck (string user_name, string password);


	}
}

