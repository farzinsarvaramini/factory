using System;
using Communication.RequestType;

namespace Communication
{
	public class Request
	{
		private RequestType _type;
		private object _content;
		
		public Request(RequestType type, object content) {
			_type = type;
			_content = _content;
		}
		
		// this function return json searilize string;
		public void toString();
	}
}

