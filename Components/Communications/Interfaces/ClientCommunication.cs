using System;
using System.Net.Sockets;

namespace Communication
{
	enum Response {
		SUCCESS = 0,
		FAIL = 1,
		WAITING = 2,
		NOT_RESPONSE = 3
	}

	class ClientCommunication
	{
		// necessary attributes
		private Socket _clientSocket;
		private string _ip;
		private int _port; 
		private double _timeOut;
		private Response _response;
		private Boolean _isAlive = true;
		private RequestType lastRequest;

		private DbCenter _dbClient;

		/*
		 * in constructor set DbCenter
		 */
		public ClientCommunication (DbCenter db);

		/*
		 * this function connect client to server.
		 * return false if connected to server
		 * 				else return true
		 * new thread must create that in infinity loop check response events.
		 */
		public bool connect();

		/*
		 * this function disconnect from server and close current socket
		 * return true if disconnecting is succesfully else return false
		 */
		public bool disconnect();

		/* when user quit from program this function called
		 * also destroy stuffs and free memory
		 * isAlive parameter equals false;
		 */
		public void shutdown();

		/*
		 * this function is base of our comminications.
		 * request parameter is a Request object which include our request details
		 * Request object has toString() methode that return json searilize string what we must send it to server
		 * for sending we must construct new thread and send message among it.
		 * if we wait more than TimeOut and server dosen't response anything, FAIL response occured.
		 * this funtion return response of our request
		 * lastRequest should be update
		 * this function communicate with client DbCenter
		 */
		public void sendRequest(Request request);

		/*
		 * this function upload file to server.
		 * also for sending file, we need to new thread
		 * if we wait more than TimeOut and server dosen't response anything, FAIL response occured.
		 * during sending file to server, server response is WAITING. (in future we must grab send file process percent)
		 */
		public void sendFile(string fileLocation);

		/*
		 * if client has a new request or receive response this function called and receive this event.
		 * when completely receive request from server, ask to server that disable new request flag
		 * if response recieved response attribute changed.
		 * if request received receivedRequest() function called.
		 */
		private void receiveCallBack(IAsyncResult AR);

		/*
		 * this function download a file from server and give it to DbCenter.
		 * when client receive a new request which has files, this function called and receive files.
		 */
		private void receiveFile();

		/* 
		 * when response state changed this function called.
		 * this function do some appropriate action depend on received response.
		 */
		private void AnswerToResponse();

		/*
		 * this function do appropriate action depends on request type
		 * also this function communicate with client DbCenter
		 */
		private void receivedRequest(Request newRequest);
	}
}
