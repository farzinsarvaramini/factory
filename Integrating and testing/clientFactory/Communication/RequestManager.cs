using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clientFactory;
using System.Web.Script.Serialization;
using clientFactory.Models;


namespace clientFactory
{

    class RequestManager
    {

        private DbCenter _dbCenter;
        string _response;
        public static RequestManager _instance;

        public RequestManager(DbCenter dbCenter)
        {
            _dbCenter = dbCenter;
        }

        public static RequestManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new RequestManager(_dbCenter);
                return _instance;
            }
        }

        public string ExeRequest(Request req)
        {
            switch (req.Type)
            {

                case RequestType.NEW_REPORT:
                    Report report = req.ToModel<Report>(0);
                    ReportCategory category = req.ToModel<ReportCategory>(1);
                    Attachments att = req.ToModel<Attachments>(2);
                    // save report in Db of server.
                    break;

                // case RequestType.DOWNLOAD: handled in server communication.

                case RequestType.NEW_REQUESTMODEL:
                    RequestModel rM = req.ToModel<RequestModel>(0);
                    // save Request in db of server
                    break;

                case RequestType.FOLLOW:
                    int RequestModelId = req.ToModel<int>(0);
                // search db for id of RequestModel
                // change column of this RequestModel
                // and save.

                case RequestType.GET:
                    int userId = req.ToModel<int>(0);
                    List<Tuple<Report, ReportCategory, Attachments>> reports = _dbCenter.dbReportCenter.GetNewReports(userId);
                    List<RequestModel> requestModels = _dbRequestCenter.GetNewRequestModels(userId);
                    List<Request> requests;
                    for (int i = 0; i < reports.Count; ++i)
                    {
                        object[] obj = { reports[i].Item1, reports[i].Item2, reports[i].Item3 };
                        Request r = new Request(RequestType.NEW_REPORT, obj);
                        requests.Add(r);
                    }
                    for (int i = 0; i < requests.Count; ++i)
                    {
                        object[] obj = { requests[i] };
                        Request r = new Request(RequestType.NEW_REQUESTMODEL, obj);
                        requests.Add(r);
                    }


                    break;

            }

            return _response;
        }

        public bool HasFile(Request req)
        {
            if (req.Type != RequestType.NEW_REPORT)
                return false;
            Attachments att = req.ToModel<Attachments>(2);
            if (att.FileLocation != null)
                return true;
            return false;
        }

        public string HasDownload(Request req)
        {
            if (req.Type != RequestType.DOWNLOAD)
                return null;
            int id = req.ToModel<int>(0);
            string location = null;
            // searcg db for location of file with Attachment id
            return location;
        }

        private string NewReport(Report report, ReportCategory category, Attachments attachment)
        {
            //_dbCenter.NewReport(report, category, attachment);
            return _response;
        }

    }
}
