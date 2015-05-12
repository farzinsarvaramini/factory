using System;
using System.Collections.Generic;

namespace Controller
{
	public class CSendReportController
	{
        private VSendReportForm _view;
        private ReportModel _report;
        private List<String> _categories;
        private List<Recipients> _allowedRecipients;

        private DbCenter _db;
		private ClientCommunication _communication;
        
        /*
         * constructor set db, controller and model
         * client must connect to server
         */
		public CSendReportController (VSendReportForm view, ReportModel model, DbCenter db)
        {
            _view = view;
            _report = model;
            _db = db;
			// connecting to server ...
        }

		/* 
		 * this function get file location from view and upload file to server.
		 * file uploading must run in new thread. Multithreading for this function is vital
		 * if uploading process done successfully return true, else return false.
		 */
		private bool uploadFile();


		/*
		 * The main function of this class is this function.
		 * when user click on send button view call this function.
		 * controller get all of the details from view and box them into one report object
		 * then upload file to server and create new request for new report and send this request to server.
		 * also this function save everything about this report in client DbCenter.
		 * if this process done successfully return true, else false.
		 */
		public bool sendReport();

		/*
		 * this function save new report in client DbCenter.
		 * actually only this function work with DbCenter.
		 * this function also copy attachments files to client application repository.
		 */
		private void saveNewReport();

		/*
		 * this function show and close send report form.
		 */
		public void showView();
		public void closeView();

		/*
		 * get and set functions for _categories and _allowedRecipients
		 */





	}
}

