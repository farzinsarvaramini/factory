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
		private RequestType _type;
		private object _content;
		
		public Request(RequestType type, object content) 
		{
			_type = type;
			_content = content;
		}
		
		// this function return JSON serialize string
		public string ToJson();
	}
}

