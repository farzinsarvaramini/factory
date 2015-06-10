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
                //-----------------------------------NEW REPORT
                case RequestType.NEW_REPORT:
                    try
                    {
                        Report report = req.ToModel<Report>(0);
                        ReportCategory category = req.ToModel<ReportCategory>(1);
                        Attachments att = req.ToModel<Attachments>(2);
                        string serverId = NewReport(report, category, att);
                        _response = "Code #0" + "@" + serverId; 
                    }
                    catch(Exception ex)
                    {
                        _response = ex.Message;
                    }
                    break;
                //-----------------------------------DOWNLOAD

                //case RequestType.DOWNLOAD: handled in server communication.

                //-----------------------------------NEW REQUEST MODEL
                case RequestType.NEW_REQUESTMODEL:
                    try
                    {
                        RequestModel rM = req.ToModel<RequestModel>(0);
                        string serverId = NewRequestModel(rM);
                        _response = "Code #1" + "@" + serverId; 
                    }
                    catch(Exception ex)
                    {
                        _response = ex.Message;
                    }
                    break;
                //-----------------------------------FOLLOW
                case RequestType.FOLLOW:
                    try
                    {
                        int requestModelId = req.ToModel<int>(0);
                        Follow(requestModelId);
                        _response = "Code #2"; 
                    }
                    catch(Exception ex)
                    {
                        _response = ex.Message;
                    }
                    break;
                //----------------------------------- REQ ANS
                case RequestType.REQ_ANS:
                    try
                    {
                        int id = req.ToModel<int>(0);
                        bool status = req.ToModel<bool>(1);
                        string answer = req.ToModel<string>(2);
                        SetAnswer(id, status, answer);
                    }
                    catch(Exception ex)
                    {
                        _response = ex.Message;
                    }
                    break;
                //----------------------------------- INIT
                case RequestType.INIT:
                    try
                    {
                        string userName = req.ToModel<string>(0);
                        User user = GetUser(userName);
                        object[] content = {user};
                        Request R = new Request(RequestType.INIT_ANS, content);
                        string json = R.ToJson();
                        byte[] sendData = Encoding.ASCII.GetBytes(json);
                        socket.Send(sendData);
                        _response = "Code #3"; 
                    }
                    catch(Exception ex)
                    {
                        _response = ex.Message;
                    }
                    break;
                //----------------------------------- GET
                case RequestType.GET:
                    try
                    {
                        int userId = req.ToModel<int>(0);
                        List<Tuple<Report, ReportCategory, Attachments>> reports = _dbCenter.ReportCenter.getNewReport(userId);
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
            string location = AttachmentLocation(id);
            return location;
        }

        private string NewReport(Report report, ReportCategory category, Attachments attachment)
        {
            string id = _dbCenter.ReportCenter.newReportWithId(report, category, attachment).ToString();
            return id;
        }

        private string NewRequestModel(RequestModel reqModel)
        {
            string id = _dbCenter.RequestCenter.AddRequestWithId(reqModel).ToString();
            return id;
        }

        private void Follow(int RequestModelId)
        {
            _dbCenter.RequestCenter.FollowRequest(RequestModelId);
        }

        private string AttachmentLocation(int AttachmentsId)
        {
            return _dbCenter.ReportCenter.getLocation(AttachmentsId);
        }

        private User GetUser(string userName)
        {
            return _dbCenter.UserCenter.Getuser(userName);
        }

        private void SetAnswer(int id, bool status, string answer)
        {
            _dbCenter.RequestCenter.AnswerToRequest(id, status, answer);
        }
    }
}
