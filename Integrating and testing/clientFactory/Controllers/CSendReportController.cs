using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;



namespace clientFactory
{
   public class CSendReportController
    {
     
        
        private SendReportView _view;
        private Report _report;
        private List<ReportCategory> _categories;
      
        private List<User> _allowedRecipients;

        private Attachments _attach;
        private ReportCategory _reportCat;

        public DbReportCenter _db;
		private ClientCommunication _communication;
        
        public CSendReportController(DbReportCenter db)
        {
            _db = db;
            _communication = new ClientCommunication();
        }

        public bool uploadFile()
        {
     
            _communication.SendFile(_view.getAttachments());//C:\\Users\\farzin\\Desktop\\downloads.txt;
            
        
             
            return true;
        }

        public bool sendReport()
        {
            //MessageBox.Show("send request");
            _communication.Connect();
           
            
             //_report = new Report(_view.getRecipientID(),_view.getRecipient(),_view.getTDescription(),_view.getTitle());
            Attachments attach;
            if(!String.IsNullOrEmpty(_view.getAttachments())){
                attach = new Attachments();
                attach.FileLocation = _view.getAttachments();
                attach.uploadTime = DateTime.Now;
            
            }else{
                attach=null;
            }

            ReportCategory repCat = new ReportCategory();
            repCat.Title = _view.getCategoryTitle();

            saveNewReport(_report,repCat,attach);
            
            Request reportRequest = new Request(RequestType.NEW_REPORT,new object[]{_report,repCat,attach});
            _communication.SendRequest(reportRequest);
            if(attach!=null){
                this.uploadFile();
            }

            _communication.Disconnect();

            return true;
        }

        public bool saveNewReport(Report r,ReportCategory rC , Attachments a)
        {
            return _db.newReport(r, rC, a);

            
        }

		public void showView()
        {

            _view = new SendReportView();
            _view._controller = this;


            _categories = _db.getCategoryList();
            _view.setCategoriesList(_categories);

            _allowedRecipients = _db.getAllowedRecipientsList();
            _view.setRecipientList(_allowedRecipients);

            _view.show();
        }
        public void closeView()
        {
            _view.close();
        }

    }
}
