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
                case RequestType.NEW_REPORT:
                    try
                    {
                        Report report = req.ToModel<Report>(0);
                        ReportCategory category = req.ToModel<ReportCategory>(1);
                        Attachments att = req.ToModel<Attachments>(2);
                        // save report in Db.
                        _response = "Code #-1"; 
                    }
                    catch(Exception ex)
                    {
                        _response = ex.Message;
                    }
                    break;
                case RequestType.NEW_REQUESTMODEL:
                    try
                    {
                        RequestModel rM = req.ToModel<RequestModel>(0);
                        // save Request in db of server
                        _response = "Code #-2"; 
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
                        _response = "Code #-3"; 
                    }
                    catch(Exception ex)
                    {
                        _response = ex.Message;
                    }
                    break;

                case RequestType.INIT_ANS:
                    try
                    {
                        User user = req.ToModel<User>(0);
                        // must save user information to db
                        _response = "Code #-4";
                    }
                    catch(Exception ex)
                    {
                        _response = ex.Message;
                    }
                    break;

                case RequestType.GET_ANS:
                    try
                    {

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
            return null;
        }

        private string NewRequestModel(RequestModel reqModel)
        {
            return null;
        }

    }
}
    

