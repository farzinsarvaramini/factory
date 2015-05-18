using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clientFactory;
using System.Windows.Forms;
using System.Web.Script.Serialization;

namespace factory_communication
{

    class RequestManager
    {

        //private DbCenter _dbcenter;
        Response _response;
        /*public RequestManager(DbCenter db)
        {
        }*/

        public Response ExeRequest(Request req)
        {
            switch (req.Type)
            {
                case RequestType.New_Report:
                    Report report = req.ToModel<Report>(0);
                    ReportCategory category = req.ToModel<ReportCategory>(1);
                    Attachments att = req.ToModel<Attachments>(2);

                    break;
                case RequestType.Get:

                    break;
            }

            return _response;
        }

        private Response NewReport()
        {

            return _response;
        }

        private Response DeleteReport(int reportId)
        {
            return _response;
        }

        private Response MarkReport()
        {
            return _response;
        }

        private Response ReadRport()
        {
            return _response;
        }

    }
}
