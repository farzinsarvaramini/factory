using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using System.Net;
using System.IO;

namespace clientFactory
{

    class ClientCommunication
    {

        private Socket _clientSocket = new Socket
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private string _ip;
        private int _port;
        private string _receivedPath;
        private string _response;
        public static ClientCommunication _instance;
        private DbCenter _dbClient;

        public static ClientCommunication Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ClientCommunication();
                return _instance;
            }
        }

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
                _response = "WAITING";
                do
                {
                    attempt++;
                    _clientSocket.Connect(new IPEndPoint(IPAddress.Parse(_ip), _port));
                    if (attempt > maxAttempt) throw new Exception("too many attempts for connecting client to server !!!");
                    if (_clientSocket.Connected)
                    {
                        _response = "SUCCESS";
                        return true;
                    }
                } while (!_clientSocket.Connected);
            }
            catch
            {
                _response = "NO_RESPONSE";
                return false;
            }
            return false;
        }

        public void RecieveResponse(Socket socket)
        {
            byte[] recieved = new byte[1024];
            socket.Receive(recieved, 0, recieved.Length, 0);
            string res = System.Text.Encoding.Default.GetString(recieved);
            _response = res;
        }

        public void SendRequest(Request request)
        {
            try
            {
                string json = request.ToJson();
                byte[] sendData = Encoding.ASCII.GetBytes(json);
                _clientSocket.Send(sendData);
                RecieveResponse(_clientSocket);
            }
            catch (Exception ex)
            {
                _response = ex.Message;
            }
        }

        public Request ReciveRequest()
        {
            byte[] recieved = new byte[1024 * 8];
            _clientSocket.Receive(recieved, 0, recieved.Length, 0);
            string js = System.Text.Encoding.Default.GetString(recieved);
            return Request.ToRequest(js);
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
                    // File transferred.
                }
                RecieveResponse(_clientSocket);
            }
            catch (Exception ex)
            {
                _response = ex.Message;

            }
        }

        public void RecieveFile(Socket serverSocket)
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
                RecieveResponse(_clientSocket);
            }
            catch (Exception ex)
            {
                _response = ex.Message;
            }
        }

        public void Disconnect()
        {
            _clientSocket.Close();
        }

        public Socket GetSocket()
        {
            return _clientSocket;
        }
    }
}
