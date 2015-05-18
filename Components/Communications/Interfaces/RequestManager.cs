// this class handle request actions and work with database
// this class must design for both of server DbCenter and client DbCenter

using System;

namespace Communication
{

	public class RequestManager 
	{
		private DbCenter _dbcenter;
		private Response _response;

		/*
		 * constructor set DB and connect to DB
		 */
		public RequestManager (DbCenter db);

		/*
		 * this function receive request and do appropriate action and communicate with DB
		 * return value is a Response
		 */
		public Response ExeRequest(string req);
		
		/*
			this function determine that request has file or not.
		*/
		public bool HasFile(request req);
		
		/*
		 * this function add new report to database
		 * notify to recipient that has new report
		 * and change flag table
		 * return value is a Response
		 */
		private Response newReport(Report r);

		/*
		 * this function delete one report and relations from db
		 * delete attachment file from server repository
		 * return value is a Response
		 */
		private Response deleteReport(int reportId);

		/*
		 * these functions change mark and read flag report in db
		 */
		private Response markReport(int reportId);
		private Response readReport(int reportId);

	}
}
