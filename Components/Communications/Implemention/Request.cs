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
        GET,
        NEW_REPORT,
        DELETE_REPORT,
        MARK_REPORT,
        READ_REPORT,
        DOWNLOAD
    }

    class Request
    {
        public RequestType Type { get; set; }
        public object[] Content { get; set; }
        public string[] jsonContent { get; set; }

        public Request()
        {
        }

        public Request(RequestType type, object[] content)
        {
            this.Type = type;
            this.Content = content;
        }

        public string ToJson()
        {
            int len = Content.Length;
            string[] jsonContentTmp = new string[len];
            for (int i = 0; i < Content.Length; ++i)
            {
                jsonContentTmp[i] = new JavaScriptSerializer().Serialize(Content[i]);
            }
            jsonContent = jsonContentTmp;
            var json = new JavaScriptSerializer().Serialize(this);
            return json;
        }

        public static Request ToRequest(string str)
        {
            Request req = new JavaScriptSerializer().Deserialize<Request>(str);
            return req;
        }

        public T ToModel<T>(int i)
        {

            T obj = new JavaScriptSerializer().Deserialize<T>(this.jsonContent[i]);
            return obj;
        }
    }
}