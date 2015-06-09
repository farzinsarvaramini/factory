using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.IO;

namespace clientFactory
{

    class ServerCommunication
    {

        public string _receivedPath;
        private int _port;
        private Socket serverSocket = new Socket
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private DbCenter _dbCenter;
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

        public ServerCommunication()
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
            string response;
            string fileResponse;
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
                ServerRequestManager reqMan = ServerRequestManager.Instance;
                response = reqMan.ExeRequest(request, sck);
                // Recieve file if they exist.
                SendResponse(response, sck);
                if(reqMan.HasFile(request))
                {
                    fileResponse = RecieveFile(sck);
                    SendResponse(fileResponse, sck);
                }
                // Send file for client if request id download.
                if(reqMan.HasDownload(request) != null)
                {
                    fileResponse = SendFile(reqMan.HasDownload(request), sck);
                    SendResponse(fileResponse, sck);
                }
            }
            catch(Exception ex)
            {
                response = ex.Message;
            }
            serverSocket.BeginAccept(new AsyncCallback(RecieveRequestCallBack), 0);
        }

        public void SendRequest(Request request, Socket sck)
        {
            try
            {
                string json = request.ToJson();
                byte[] sendData = Encoding.ASCII.GetBytes(json);
                sck.Send(sendData);
            }
            catch (Exception ex)
            {

            }
        }

		private void SendResponse(string response, Socket socket)
        {
            byte[] res = Encoding.ASCII.GetBytes(response.ToString());
            socket.Send(res);
        }
		
        private string RecieveFile(Socket clientSocket)
        {
            string response = "Code #5.0"; 
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
                response = "Code #5.1";  
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }
            return response;
        }

        private string SendFile(string fileName, Socket clientSocket)
        {
            string response = "Code #6.0";
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
                response = "Code #6.1";
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }
            return response;
        }

        private void Shutdown()
        {
            serverSocket.Shutdown(SocketShutdown.Both);
            serverSocket.Close();            
        }
	}

}
