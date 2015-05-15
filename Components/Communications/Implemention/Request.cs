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
        public RequestType Type {get; set;}
        public object Content {get; set;}


        public Request(RequestType type, object content)
        {
            this.Type = type;
            this.Content = content;
        }     

        public string ToJson()
        {
            var json = new JavaScriptSerializer().Serialize(this);
            return json;
        }

        public Request ToRequest(string str)
        {
            Request req = new JavaScriptSerializer().Deserialize<Request>(str);
            return req;
        }
    }
}