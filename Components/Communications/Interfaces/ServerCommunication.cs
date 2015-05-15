using System;
using System.Net.Sockets;

namespace factory_communication
{
	enum Response
        {
            SUCCESS = 0,
            FAIL = 1,
            WAITING = 2,
            No_Response = 3
        }
		
    class ServerCommunication
    {
        public string _receivedPath;
        private int _port;
		private Response _response;
        private Socket serverSocket
        private DbCenter _dbCenter;

        public ServerCommunication ()

		public void AcceptClientCallBack()
	    
        private void RecieveRequestCallBack(IAsyncResult AR)

		private void SendResponse(string response, Socket socket)

        private void ReceiveFile(Socket clientSocket)

        public void SendFile(string fileName, Socket clientSocket)

        public void Shutdown()

	}

}
