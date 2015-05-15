using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
