using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clientFactory;
using System.Web.Script.Serialization;
using System.Net.Sockets;

namespace clientFactory
{

    class ServerRequestManager
    {
        static DbCenter _dbCenter;
        string _response;
        public static ServerRequestManager _instance;

        public ServerRequestManager(DbCenter dbCenter)
        {
            _dbCenter = dbCenter;
        }

        public static ServerRequestManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ServerRequestManager(_dbCenter);
                return _instance;
            }
        }

        public string ExeRequest(Request req, Socket socket)
        {

            switch (req.Type)
            {

                case RequestType.NEW_REPORT:
                    try
                    {
                        Report report = req.ToModel<Report>(0);
                        ReportCategory category = req.ToModel<ReportCategory>(1);
                        Attachments att = req.ToModel<Attachments>(2);
                        // save report in Db of server.
                        _response = "Code #0"; 
                    }
                    catch(Exception ex)
                    {
                        _response = ex.Message;
                    }
                    break;

                //case RequestType.DOWNLOAD: handled in server communication.

                case RequestType.NEW_REQUESTMODEL:
                    try
                    {
                        RequestModel rM = req.ToModel<RequestModel>(0);
                        // save Request in db of server
                        _response = "Code #1"; 
                    }
                    catch(Exception ex)
                    {
                        _response = ex.Message;
                    }
                    break;

                case RequestType.FOLLOW:
                    try
                    {
                        // int RequestModelId = req.ToModel<int>(0);
                        // search db for id of RequestModel
                        // change column of this RequestModel
                        // and save.
                        _response = "Code #2"; 
                    }
                    catch(Exception ex)
                    {
                        _response = ex.Message;
                    }
                    break;

                case RequestType.INIT:
                    try
                    {
                        // search db for information of user
                        // save them in request with type INIT_ANS
                        // content is information of user{pw, ...?}
                        _response = "Code #3"; 
                    }
                    catch(Exception ex)
                    {
                        _response = ex.Message;
                    }
                    break;

                case RequestType.GET:
                    try
                    {
                        int userId = req.ToModel<int>(0);
                        List<Tuple<Report, ReportCategory, Attachments>> reports = _dbCenter.ReportCenter.GetNewReport(userId);
                        List<RequestModel> requestModels = _dbCenter.RequestCenter.GetNewRequest(userId);
                        List<Request>  requests = new List<Request>();
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
                        object[] content = requests.Cast<Request>().ToArray();
                        Request R = new Request(RequestType.GET_ANS, content);
                        string json = R.ToJson();
                        byte[] sendData = Encoding.ASCII.GetBytes(json);
                        socket.Send(sendData);
                        _response = "Code #4"; 
                    }
                    catch (Exception ex)
                    {
                        _response = ex.Message;
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

        private string NewRequestModel(RequestModel reqModel)
        {
            return null;
        }

    }
}
