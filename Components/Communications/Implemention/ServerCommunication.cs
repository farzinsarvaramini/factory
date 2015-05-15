using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Windows.Forms;
using System.IO;

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
        private Socket serverSocket = new Socket
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // private DbCenter _dbCenter;

        public ServerCommunication ()
		{
            _port = 101;
            _receivedPath = ""; // defualt path must be added
        }		

		public void AcceptClientCallBack()
        {
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, _port));
            serverSocket.Listen(100);
            serverSocket.BeginAccept(new AsyncCallback(RecieveRequestCallBack), null);

           
        }
	    
        private void RecieveRequestCallBack(IAsyncResult AR)
        {
            Socket sck = serverSocket.EndAccept(AR);

            //byte[] request = new byte[1024];
            // request manager would be here
            ReceiveFile(sck);

            serverSocket.BeginAccept(new AsyncCallback(RecieveRequestCallBack), 0);
        }

		private void SendResponse(string response, Socket socket)
        {
            // send response : Json or string?
        }
		
        private void ReceiveFile(Socket clientSocket)
        {
            try
            {
                Stream clientStream = new NetworkStream(clientSocket);
                Int64 bytesReceived = 0;
                int count;
                const int CHUNK_SIZE = 2 * 1024;
                byte[] buffer = new byte[CHUNK_SIZE];
                    // read file name length
                clientStream.Read(buffer, 0, 4);
                int fileNameLen = BitConverter.ToInt32(buffer, 0);
                    // read file name 
                byte[] nameBuffer = new byte[fileNameLen];
                clientStream.Read(nameBuffer, 0, fileNameLen);
                string fileName = System.Text.Encoding.Default.GetString(nameBuffer);
                    // read file data size
                clientStream.Read(buffer, 0, 8);
                Int64 numberOfBytes = BitConverter.ToInt64(buffer, 0);
                    // read file data
                using (var fileData = File.Create(_receivedPath + "/" + fileName))
                    while (bytesReceived < numberOfBytes && (count = clientStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fileData.Write(buffer, 0, count);
                        bytesReceived += count;
                    }
                clientSocket.Close();
            }
            catch (Exception ex)
            {
                // Exception
                // change and send response
            }
        }

        public void SendFile(string fileName, Socket clientSocket)
        {
            Response response;
            try
            {
                string filePath = "";
                fileName = fileName.Replace("\\", "/");
                while (fileName.IndexOf("/") > -1)
                {
                    filePath += fileName.Substring(0, fileName.IndexOf("/") + 1);
                    fileName = fileName.Substring(fileName.IndexOf("/") + 1);
                }
                //   Buffering ...
                using (var fileData = File.OpenRead(filePath + fileName))
                using (Stream clientStream = new NetworkStream(clientSocket))
                {
                    const int CHUNK_SIZE = 2 * 1024;
                    var buffer = new byte[CHUNK_SIZE];
                    // send file name length
                    clientStream.Write(BitConverter.GetBytes(fileName.Length), 0, 4);
                    // send file name
                    buffer = Encoding.ASCII.GetBytes(fileName);
                    clientStream.Write(buffer, 0, fileName.Length);
                    // send file data size
                    clientStream.Write(BitConverter.GetBytes(fileData.Length), 0, 8);
                    // send file data
                    int count;
                    while ((count = fileData.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        clientStream.Write(buffer, 0, count);
                    }
                    clientSocket.Close();
                    // File transferred.
                }
                response = Response.SUCCESS;
            }
            catch (Exception ex)
            {
                if (ex.Message == "No connection could be made because the target machine actively refused it") // No connection.
                    response = Response.No_Response;
                else  // File Sending fail.
                    response = Response.FAIL;
            }
        }

        public void Shutdown()
        {
            
            serverSocket.Shutdown(SocketShutdown.Both);
            serverSocket.Close();            
        }
	}

}
