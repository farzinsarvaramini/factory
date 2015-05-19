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
        NO_RESPONSE = 3
    }

    class ServerCommunication
    {

        public string _receivedPath;
        private int _port;
        private Socket serverSocket = new Socket
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // private DbCenter _dbCenter;
        public static ServerCommunication _instance;


        public static ServerCommunication Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ServerCommunication();
                return _instance;
            }
        }

        public ServerCommunication ()
		{
            _port = 101;
            _receivedPath = "E:\\"; // defualt path must be added
        }		

		public void AcceptClientCallBack()
        {
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, _port));
            serverSocket.Listen(100);
            serverSocket.BeginAccept(new AsyncCallback(RecieveRequestCallBack), null);  
        }
	    
        private void RecieveRequestCallBack(IAsyncResult AR)
        {
            Response response;
            Socket sck = serverSocket.EndAccept(AR);
            try
            {
                // Recieve request in Byte.
                byte[] requestByte = new byte[1024 * 2];
                sck.Receive(requestByte, 0, requestByte.Length, 0);
                // Convert request to Request object.
                string recievedJson = Encoding.ASCII.GetString(requestByte).Replace("\0", "");;
                Request request = Request.ToRequest(recievedJson);
                // Execute request.
                RequestManager reqMan = RequestManager.Instance;
                response = reqMan.ExeRequest(request);
                // Recieve file if they exist.
                if(reqMan.HasFile(request))
                {
                    ReceiveFile(sck);
                }
                // Send file for client if request id download.
                if(reqMan.HasDownload(request) != null)
                {
                    SendFile(reqMan.HasDownload(request), sck);
                }
            }
            catch(Exception ex)
            {
                response = Response.FAIL;
            }
            SendResponse(response.ToString(), sck);
            serverSocket.BeginAccept(new AsyncCallback(RecieveRequestCallBack), 0);
        }

		private void SendResponse(string response, Socket socket)
        {
            byte[] res = Encoding.ASCII.GetBytes(response);
            socket.Send(res);
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
                    // File transferred.
                }
                response = Response.SUCCESS;
            }
            catch (Exception ex)
            {
                if (ex.Message == "No connection could be made because the target machine actively refused it") // No connection.
                    response = Response.NO_RESPONSE;
                else  // File Sending fail.
                    response = Response.FAIL;
            }
        }

        private void Shutdown()
        {
            serverSocket.Close();            
        }
	}

}
