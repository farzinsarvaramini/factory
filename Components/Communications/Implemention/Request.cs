using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace factory_communication
{
    enum RequestType
    {
        Get,
        New_Report,
        Delete_Report,
        Mark_Report,
        Read_Report
    }

    class Request
    {
        private RequestType _type;
        private object _content;

        public Request(RequestType type, object content)
        {
            _type = type;
            _content = content;
        }
        

        public string ToJson()
        {
            var json = new JavaScriptSerializer().Serialize(this);
            return json;
        }

    }
}
