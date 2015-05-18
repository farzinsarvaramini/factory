using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace factory_communication
{

    class ClientCommunication
    {
        enum Response
        {
            SUCCESS = 0,
            FAIL = 1,
            WAITING = 2,
            NO_RESPONSE = 3
        }

        private Socket _clientSocket = new Socket
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private string _ip;
        private int _port;
        private string _receivedPath;
		private Response _response;
		//private DbCenter _dbClient;


        public ClientCommunication()
        {
            _ip = "127.0.0.1";
            _port = 101;
            _receivedPath = "";
        }

		public bool Connect()
        {
            const int maxAttempt = 1000;
            int attempt = 0;
            try
            {
                _response = Response.WAITING;
                do
                {               
                        attempt++;
                        _clientSocket.Connect(new IPEndPoint(IPAddress.Parse(_ip), _port));
                        if (attempt > maxAttempt) throw new Exception("too many attempts for connecting client to server !!!");
                        if (_clientSocket.Connected)
                        {
                            _response = Response.SUCCESS;
                            return true;
                        }
                } while (!_clientSocket.Connected);
            }
            catch
           {
               _response = Response.NO_RESPONSE;
               return false;
           }
            return false;
        }

        public string SendRequest(Request request) 
        {
            try
            {
                string json = request.ToJson();
                byte[] sendData = Encoding.ASCII.GetBytes(json);
                _clientSocket.Send(sendData);
                byte[] response = new byte[1024];
                _clientSocket.Receive(response, 0, response.Length, 0);
                return Encoding.ASCII.GetString(response);
            }
            catch(Exception ex)
            {
                _response = Response.FAIL;
                return ex.ToString();
            }
        } 

        public void SendFile(string fileName)
        {
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
                using (Stream clientStream = new NetworkStream(_clientSocket))
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
                    clientStream.Close();
                    // File transferred.
                }
                _response = Response.SUCCESS;
            }
            catch (Exception ex)
            {
                if (ex.Message == "No connection could be made because the target machine actively refused it")
                    // No connection.
                   _response = Response.NO_RESPONSE;
                else 
                    // File Sending fail.
                    _response = Response.FAIL;
            }

        }

        private void ReceiveFile(Socket serverSocket)
        {
            try
            {
                Stream serverStream = new NetworkStream(serverSocket);
                Int64 bytesReceived = 0;
                int count;
                const int CHUNK_SIZE = 2 * 1024;
                byte[] buffer = new byte[CHUNK_SIZE];
                // read file name length
                serverStream.Read(buffer, 0, 4);
                int fileNameLen = BitConverter.ToInt32(buffer, 0);
                // read file name 
                byte[] nameBuffer = new byte[fileNameLen];
                serverStream.Read(nameBuffer, 0, fileNameLen);
                string fileName = System.Text.Encoding.Default.GetString(nameBuffer);
                // read file data size
                serverStream.Read(buffer, 0, 8);
                Int64 numberOfBytes = BitConverter.ToInt64(buffer, 0);
                // read file data
                using (var fileData = File.Create(_receivedPath + "/" + fileName))
                    while (bytesReceived < numberOfBytes && (count = serverStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fileData.Write(buffer, 0, count);
                        bytesReceived += count;
                    }
                serverSocket.Close();
            }
            catch (Exception ex)
            {
                // Exception
            }
        }

        private void Disconnect()
        {
            _clientSocket.Close();
        }
    } 
}
