using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Communication
{
	enum Response {
		SUCCESS = 0,
		FAIL = 1,
		WAITING = 2,
		NOT_RESPONSE = 3
	}
	
	public class ServerCommunication
	{
		// necessary attributes
		private Socket _serverMainSocket;
		private List<Socket> _clientsSockets;
		
		private DbCenter _dbCenter;
		
		/*
		 * in constructor server set up and start to listening
		 */
		public ServerCommunication ()
		{
			// setup server
		}
		
		/*
		 * this function shutdown server and destroy client sockets
		 * also destroy stuffs and free memory
		 */
		public void shutdown();
		
		/*
		 * when client connect() method call this functoin allocate new socket for it and add it to clientSockets
		 */
		private void acceptClientCallBack(IAsyncResult AR);
		
		/*
		 * when client send a request this function called and response to it
		 * among this function we must comminicate with Db through DbCenter object
		 */
		private void receiveRequestCallBack(IAsyncResult AR);
		
		/*
		 * this function receive file from client and store that file to application repository
		 */
		private void receiveFile();
		
		/*
		 * this function send response to client
		 * socket parameter is socket which send request
		 */
		private void sendResponse(ResponseType res, Socket socket);
		
		/*
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
	}
}

