using System;
using System.Collections.Generic;

namespace View
{
	public class VReporListView
	{
		private CViewReportController _controller;
		private List<Tuple<Report, ReportCategory, Attachment>> _reports;
		private int _selectedReportId;
		private string _searchTitle;
		private string _searchCategory;
		private int _searchUserId;
		private Date _searchDateStart;
		private Date _searchDateEnd;
		
		public VReporListView (CViewReportController c)
		{
			this._controller = c;
		}
		
		// this function show dialog
		public void show();
		
		// this function close dialog
		public void close();

		/*
		 * this function update _selectedReportId
		 * this function return id of selected report
		 */
		public int getSelectedReportId();

		/*
		 * this function add new report to list
		 * gui must updated
		 */
		public void addNewReport(Tuple<addNewReport, ReportCategory, Attachment> report);

		// this function delete a report from list in gui
		public void deleteFromList(int id);
		
		// this function return search title in text box
		public string getSearchTitle();

		// this function return id of selected user in search comboBox
		public int getSearchUser();
		
		// this function return title if selected category in search group
		public string getSearchCategory();

		// these functions return start and end report date in search section
		public DateTime getSearchStartDate();
		public DateTime getSearchEndDate();
		
		/*
		 * these functions called when events occur.
		 * every function corresponding function in controller
		 */
		private void deleteEvent();
		private void markEvent();
		private void showEvent();
		private void searchEvent();
	}
}


