using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clientFactory;
using factory_communication;

namespace Controller
{
    public class CViewReportController
    {
        private VReportListView _listView;
        private VReportView _view;
        private DbCenter _db;
        private ClientCommunication _communication;


        public CViewReportController(VReportListView lv, VReportView v, DbCenter db)
        {
            _listView = lv;
            _view = v;
            _db = db;
        }


        public void ShowReportList()
        {
            _listView.Show();
        }

        public void ShowReport()
        {
            _view.Show();
        }

        private void ChangeReport()
        {
            int id = _listView.SelectedReportId;
            Tuple<Report, ReportCategory, Attachments> tup = _db.GetReportDetails(id);
            Report report = tup.Item1;
            ReportCategory category = tup.Item2;
            Attachments attachment = tup.Item3;
            string fileName = "";
            string fileLocation = attachment.FileLocation;
            for(int i = fileLocation.Length - 1; i >= 0 && fileLocation[i] != '\\'; i--)
            {
                fileName = fileLocation[i] + fileName;
            }
            
        }

        public void DeleteReport()
        {
            int id = _listView.SelectedReportId;
            _db.DeleteReport(id);
        }

        public void MarkReport()
        {
            int id = _listView.SelectedReportId;
            _db.MarkReport(id);
        }

        public void ReadReport()
        {
            int id = _listView.SelectedReportId;
            _db.ReadReportt(id);
        }

        public void DownloadAttachment()
        {
            int id = _listView.SelectedId;
            int serverId = _db.GetServerReportId(id);
            object[] con = {serverId};
            Request req = new Request(RequestType.Download, con);
            ClientCommunication client = new ClientCommunication();
            client.ReceiveFile(client.GetSocket());
        }
    }
}
