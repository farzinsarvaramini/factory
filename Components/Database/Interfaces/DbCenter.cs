/*
 * this class handle all of the database works
 * this class must implement for both of clent side and server side
 */
using System;

namespace Database
{
	public class DbCenter
	{

		private serverContainer _serverDb;
		/*
		 * for client side:
		 * clientContainer _clientDb
		 */
		public DbReportCenter Report;

		public DbCenter ()
		{
			Report = new DbReportCenter ();
			_serverDb = new serverContainer ();
		}
	}
}
