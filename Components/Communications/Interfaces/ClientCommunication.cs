using System;
using System.Net.Sockets;

namespace Communication
{


	class ClientCommunication
	{
		enum Response
		{
			SUCCESS = 0,
			FAIL = 1,
			WAITING = 2,
			NOT_RESPONSE = 3
		}
		
		// necessary attributes
		private Socket _clientSocket;
		private string _ip;
		private string _receivedPath;
		private int _port; 
		private Response _response;
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
		public bool Connect();

		/*
		 * this function disconnect from server and close current socket
		 * return true if disconnecting is successfully else return false
		 */
		public bool Disconnect();

		/*
		 * this function is base of our communications.
		 * request parameter is a Request object which include our request details
		 * Request object has toString() method that return JSON serialize string what we must send it to server
		 * for sending we must construct new thread and send message among it.
		 * if we wait more than TimeOut and server doesn't response anything, FAIL response occurred.
		 * this function return response of our request
		 * lastRequest should be update
		 * this function communicate with client DbCenter
		 */
		public string SendRequest(Request request);

		/*
		 * this function upload file to server.
		 * also for sending file, we need to new thread
		 * if we wait more than TimeOut and server doesn't response anything, FAIL response occurred.
		 * during sending file to server, server response is WAITING. (in future we must grab send file process percent)
		 */
		public void SendFile(string fileLocation);

		/*
		 * this function download a file from server and give it to DbCenter.
		 * when client receive a new request which has files, this function called and receive files.
		 */
		public void ReceiveFile();
		
		/*
			this function return Client Socket.
		*/
		public socket GetSocket();
		


	}
}
