using System;

namespace Database
{
	public class DbReportCenter : DbCenter
	{
		public DbReportCenter instance;

		public DbReportCenter ()
		{
			instance = this; // singleton patern
		}

		/*
		 * this function add new report to data base
		 * return value is result
		 */
		public bool NewReport (Report r, ReportCategory rc, Attachment a);

		/*
		 * this function delete report
		 * only used in client side
		 * id is report id which should be deleted
		 * return value is result
		 */
		public bool DeleteReport(int id);

		/*
		 * this function change report mark flag 
		 * only used in client side
		 * id is report id which should be marked
		 * return value is result
		 */
		public bool MarkReport(int id);

		/*
		 * this function change report read flag
		 * only used in client side
		 * id is report id which should be readed
		 * return value is result
		 */
		public bool ReadReport(int id);

		/*
		 * this function return server report id in database
		 * only in client side
		 */
		public int GetServerReportId(int reportId);

		/*
		 * this function return details of a report
		 */
		public Tuple<Report, ReportCategory, Attachments> GetReportDetails (int reportId);

		/*
		 * this function return list of new report for any user
		 * symbol of new report in server is isRead flag
		 */
		public List<Tuple<Report, ReportCategory, Attachments>> GetNewReports (int user_id);
	}
}

