using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace factory_automation
{
    enum Response
    {
        SUCCESSFUL,
        FAIL,
        NO_RESPONSE
    };
    class ClientCommunication
    {

        private Socket clientSocket = new Socket
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        
        public bool connect(string ip="127.0.0.1")
        {
            const int maxAttempt = 1000;
            int attempt = 0;

            do
            {
                try
                {
                    attempt++;
                    clientSocket.Connect(new IPEndPoint(IPAddress.Parse(ip), 101));

                    if (clientSocket.Connected)
                    {
                        MessageBox.Show("client connected");
                        return true;
                    }
                }
                catch
                {
                    if (attempt > maxAttempt)
                    {
                        MessageBox.Show("client not connected");
                        return false;
                    }
                }
            } while (!clientSocket.Connected);

            return true;

        }

        public void disconnect()
        {
            clientSocket.Close();
            
        }
        public Response sendRequest(string msg)
        {
            Response response = Response.NO_RESPONSE;
            MessageBox.Show("reqest "+msg+" is going to send by client");
            Thread tmpThread = new Thread(delegate()
            {
                byte[] sendBuffer = Encoding.ASCII.GetBytes(msg);

                clientSocket.Send(sendBuffer);

                byte[] recievedBuffer = new byte[1024];
                int recSize = clientSocket.Receive(recievedBuffer);
                //byte[] data = new byte[recSize];
                //Array.Copy(recievedBuffer, data, recSize);
                Array.Resize(ref recievedBuffer, recSize);
                if (Encoding.ASCII.GetString(recievedBuffer) == "true") response=Response.SUCCESSFUL;
                else response = Response.FAIL;
            });
            tmpThread.Start();
            tmpThread.Join();
            MessageBox.Show("result by server is : "+response);
            //Console.WriteLine(response);
            return response;

        }

        public bool sendFile(string file)
        {


            return true;
        }
    }
}
