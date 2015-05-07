using System;

namespace Communication
{
	enum Response {
		SUCCESS,
		FAIL,
		WAITING,
		NOT_RESPONSE
	}

	class ClientCommunication
	{
		// necessary attributes
		string DNS; //specify DNS(Server) ip
		double TimeOut;

		/*
		 * this function connect client to server.
		 * return false if connected to server
		 * 				else return true
		 * sync function must start when we connect to datebase
		 */
		public bool connect();

		/*
		 * this function disconnect from server and close current socket
		 * alse destroy stuffs and free memory
		 * return true if disconnecting is succesfully else return false
		 */
		public bool disconnect();

		/*
		 * this function is base of our comminications.
		 * request parameter is a Request object which include our request details
		 * Request object has toString() methode that return json searilize string what we must send it to server
		 * for sending we must construct new thread and send message among it.
		 * if we wait more than TimeOut and server dosen't response anything, FAIL response occured.
		 * this funtion return response of our request
		 */
		public Response sendRequest(Request request);

		/*
		 * this function upload file to server.
		 * also for sending file, we need to new thread
		 * if we wait more than TimeOut and server dosen't response anything, FAIL response occured.
		 * during sending file to server, server response is WAITING. (in future we must grab send file process percent)
		 */
		public Response uploadFile(string fileLocation);

		/*
		 * if client has a new happening this function called and receive this event.
		 * when completely receive request from server, ask to server that disable new happening flag
		 */
		private void receiveRequest();

		/*
		 * this function download a file from server and give it to DbCenter.
		 * when client receive a new request which has files, this function called and receive files.
		 */
		private void downloadFile();
	}
}
