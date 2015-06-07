using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

class RequestManager
    {

        //private DbCenter _dbcenter;
        Response _response;
        public static RequestManager _instance;


        public RequestManager()//(DbCenter db)
        {
        }

        public static RequestManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new RequestManager();
                return _instance;
            }
        }

        public Response ExeRequest(Request req)
        {
            switch(req.Type)
            {

                case RequestType.New_Report:
                    Report report = req.ToModel<Report>(0);
                    ReportCategory category = req.ToModel<ReportCategory>(1);
                    Attachments att = req.ToModel<Attachments>(2);
                    // save report in Db of server.
                    break;

                case RequestType.Get:

                    break;


            }

            return _response;
        }

        public bool HasFile(Request req)
        {
            if (req.Type != RequestType.New_Report)
                return false;
            Attachments att = req.ToModel<Attachments>(2);
            if (att.FileLocation != null)
                return true;
            return false;
        }

        public string HasDownload(Request req)
        {
            if (req.Type != RequestType.Download)
                return null;
            string location = null;
            // searcg db for location of file with report id
            return location;
        }

        private Response NewReport()
        {

            return _response;
        }

    }
