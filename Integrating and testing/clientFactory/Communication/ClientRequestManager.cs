using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientFactory.Communication
{
    class ClientRequestManager
    {
        static DbCenter _dbCenter;
        string _response;
        public static ClientRequestManager _instance;

        public ClientRequestManager(DbCenter dbCenter)
        {
            _dbCenter = dbCenter;
        }

        public static ClientRequestManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ClientRequestManager(_dbCenter);
                return _instance;
            }
        }

        public string ExeRequest(Request req)
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
                        NewReport(report, category, att);
                        _response = "Code #-1"; 
                    }
                    catch(Exception ex)
                    {
                        _response = ex.Message;
                    }
                    break;
                //-----------------------------------NEW REQUEST MODEL
                case RequestType.NEW_REQUESTMODEL:
                    try
                    {
                        RequestModel rM = req.ToModel<RequestModel>(0);
                        NewRequestModel(rM);
                        _response = "Code #-2"; 
                    }
                    catch(Exception ex)
                    {
                        _response = ex.Message;
                    }
                    break;
                //----------------------------------- FOLLOW
                case RequestType.FOLLOW:
                    try
                    {
                        int requestModelId = req.ToModel<int>(0);
                        Follow(requestModelId);
                        _response = "Code #-3"; 
                    }
                    catch(Exception ex)
                    {
                        _response = ex.Message;
                    }
                    break;
                //----------------------------------- INIT_ANS
                case RequestType.INIT_ANS:
                    try
                    {
                        User user = req.ToModel<User>(0);
                        InitUser(user);
                        _response = "Code #-4";
                    }
                    catch(Exception ex)
                    {
                        _response = ex.Message;
                    }
                    break;
                //----------------------------------- GET_ANS
                case RequestType.GET_ANS:
                    try
                    {
                        for(int i = 0; i < req.Content.Length; ++i)
                        {
                            Request R = (Request)req.Content[i];
                            ExeRequest(R);
                        }
                        _response = "Code #-5";
                    }
                    catch(Exception ex)
                    {
                        _response = ex.Message;
                    }
                    break;

            }
            return _response;
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

        private void InitUser(User user)
        {
            _dbCenter.UserCenter.AddUser(user, user.UserId);
        }

    }
}
    

