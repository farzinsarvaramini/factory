using System;

namespace Controller
{
	public class CViewReportController
	{
		private VReportListView _listView;
		private VReportView _view;

		private DbCenter _db;
		private ClientCommunication _communication;

		public CViewReportController (VReportListView lv, VReportView v, DbCenter db)
		{
			_listView = lv;
			_view = v;
			_db = db;
		}

		// these function show views
		public void showReportList();
		public void showReport();

		// this function set attributes of ReportView when selected report changed
		private void changeReport();

		// these function work with DbCenter
		public void deleteReport();
		public void markReport();
		public void readReport();

		// this function download attachment from server
		public void downloadAttachment();
	}
}

