// this class handle request actions and work with database

using System;

namespace Communication
{
	struct Message 
	{
		int success; // 0 for success and 1 for fail
		string message;
	}

	public class RequestManager 
	{
		private DbCenter _dbcenter;

		/*
		 * constructor set Db and connect to db
		 */
		public RequestManager (DbCenter db);

		/*
		 * this function receive request and do appropriate action and communicate with db
		 * return value is a Message struct
		 */
		public Message ExeRequest(Request r);

		/*
		 * this function add new report to database
		 * notify to recipient that has new report
		 * and change flag table
		 * return value is a Message struct
		 */
		private Message newReport(Report r);

		/*
		 * this function delete one report and relations from db
		 * delete attachment file from server repository
		 * return value is a Message struct
		 */
		private Message deleteReport(int reportId);

		/*
		 * these functions change mark and read flag report in db
		 */
		private Message markReport(int reportId);
		private Message readReport(int reportId);

	}
}
