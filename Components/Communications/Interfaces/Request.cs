using System;
using Communication.RequestType;

namespace Communication
{
	enum RequestType
	{
		Get, // for get new report and stuffs from server.
		New_Report, // for sending new report
		Delete_Report, // for deleting a report
		Mark_Report, // for marking a report
		Read_Report // for changing report read flag 
	}
	public class Request
	{
		public RequestType Type {get; set;}
        public object Content {get; set;}
		
		public Request(RequestType type, object content)
		
		// this function return JSON serialize string
		public string ToJson();
		
		/* this function get JSON of Request
		   and Deserialize it and return Request object
		*/
		public Request ToRequest(string str);
	}
}

