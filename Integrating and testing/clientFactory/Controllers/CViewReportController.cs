using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace clientFactory
{
    public class CViewReportController
    {
        
        private ReportListView _listView;
        private ReportView _view;
        private DbReportCenter _db;
        private List<User> _allowedSenders;
        private ClientCommunication _communication;
        private List<Report> allReportsList;
        private List<Report> sentReportsList;
        private List<Report> recievedReportsList;
        private List<Report> allFoundReportsList;
        private List<Report> sentFoundReportsList;
        private List<Report> recievedFoundReportsList;

        public CViewReportController(DbReportCenter db)
        {
            _db = db;
        }

        private void ChangeReport()
        {
            //int id = _listView.SelectedReportId;
            //Tuple<Report, ReportCategory, Attachments> tup = _db.GetReportDetails(id);
            //Report report = tup.Item1;
            //ReportCategory category = tup.Item2;
            //Attachments attachment = tup.Item3;
            //string fileName = "";
            //string fileLocation = attachment.FileLocation;
            //for(int i = fileLocation.Length - 1; i >= 0 && fileLocation[i] != '\\'; i--)
            //{
            //    fileName = fileLocation[i] + fileName;
            //}
            
        }

        public void DeleteReport(Report r)
        {
            
            _db.deleteReport(r.Id);
        }

        public void MarkReport(Report r)
        {
            
            _db.markReport(r.Id);
        }

        public void ReadReport(Report r)
        {
            //int id = _listView.SelectedReportId;
            //_db.readReportt(id);
        }

        public void DownloadAttachment()
        {
            //int id = _listView.SelectedId;
            //int serverId = _db.GetServerReportId(id);
            //object[] con = {serverId};
            //Request req = new Request(RequestType.Download, con);
            //ClientCommunication client = new ClientCommunication();
            //client.ReceiveFile(client.GetSocket());
        }




        public void setReportViewFields(Report r,ReportView rV)
        {
            rV.title_l.Content = r.Title;
            rV.sender_l.Content = r.Sender;
            if(r.ReportCategory!=null) rV.reportCategory_l.Content = r.ReportCategory.Title;
            rV.description_tbl.Text = r.Description;
            if(r.Attachment!=null) rV.attachments_tbl.Text = r.Attachment.FileLocation;
        }

        public void showReportListView()
        {
            //get needed stuff from db and fill view and then show the view
            
            _listView = new ReportListView();
            _listView._controller = this;
            //we have delay here
            //allReportsList = _db.getAllReportList();
            _listView.setAllReportList(allReportsList);
            
            //sentReportsList = _db.getSentReportList();
            //_listView.setSentReportList(sentReportsList);

            //recievedReportsList = _db.getRecievedList();
            //_listView.setRecievedReportList(recievedReportsList);

            //_allowedSenders = _db.getAllowedSendersList();
            //_listView.setSenderList(_allowedSenders);


            _listView.show();

        }

        public void updateReportListView(string title,string category,string sender
            , DateTime from, DateTime to)
        {
            foreach(Report r in allReportsList){
                if(r.Title == title &&
                r.ReportCategory.Title == category && r.Sender == sender &&
                r.SendDate.CompareTo(from) > 0 && r.SendDate.CompareTo(to) < 0)
                {
                    allFoundReportsList.Add(r);
                    if (r.Sender_ID == SessionInfos.login_user.Id) sentFoundReportsList.Add(r);
                    if (r.Recipient_ID == SessionInfos.login_user.Id) recievedFoundReportsList.Add(r);
                }
                _listView.setAllReportList(allFoundReportsList);
                _listView.setSentReportList(sentFoundReportsList);
                _listView.setRecievedReportList(recievedFoundReportsList);
            }
        }


            public void renewReportListView()
            {

                _listView.setAllReportList(allReportsList);
                //_listView.setSentReportList(sentReportsList);
                //_listView.setRecievedReportList(recievedReportsList);

                //allFoundReportsList.Clear();
                //sentFoundReportsList.Clear();
                //recievedFoundReportsList.Clear();
            }


        public void closeReportListView()
        {
            _listView.close();
        }


        public void showReportView(Report rep)
        {
            if (rep != null)
            {
                //get needed stuff from selected report and fill view and then show it
                _view = new ReportView();
                _view._controller = this;
                _view.report = rep;
                setReportViewFields(rep, _view);
                _view.show();
            }
        }

        public void closeReportView()
        {

            _view.close();
        }

        public void printReportView()
        {

        }


    }
}
