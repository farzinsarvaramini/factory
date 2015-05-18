using System;

namespace View
{
	public class VReportView
	{
		private CViewReportController _controller;
		private DateTime _date;
		private bool _isSendingReport;
		private string _senderOrRecepient;
		private string _description;
		private string _attachmentFileName;
		private string[] _categories;

		public VReportView (CViewReportController c)
		{
			this._controller = c;
		}

		public void show();
		public void close();

		/*
		 * setter functions for private attributes
		 */

		// these function called when events occur and call corresponding function in controller
		private void printEvent();
		private void markEvent();
		private void deleteEvent();
		private void downloadEvent();
	}
}

