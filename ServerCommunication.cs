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
    class ServerCommunication
    {
        private List<Socket> clientSockets = new List<Socket>();
        private byte[] buffer = new byte[1024];
        private Socket serverSocket = new Socket
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); 

        public void connect()
        {
            setupServer();         
        }

        private void setupServer()
        {
            //Console.WriteLine("setting up server...");
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, 101));
            serverSocket.Listen(3);
            MessageBox.Show("server ok");
            serverSocket.BeginAccept(new AsyncCallback(acceptCallBack), null);

        }
        private  void acceptCallBack(IAsyncResult AR)
        {
            Socket sck = serverSocket.EndAccept(AR);
            clientSockets.Add(sck);
            //Console.WriteLine("client {0} connected ,", sck.RemoteEndPoint.ToString());
            
            sck.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(recieveCallBack), sck);
            serverSocket.BeginAccept(new AsyncCallback(acceptCallBack), 0);

        }
        private  void recieveCallBack(IAsyncResult AR)
        {

            Socket sck = (Socket)AR.AsyncState;

            try
            {
                int recieved = sck.EndReceive(AR);


                byte[] tmpData = new byte[recieved];
                Array.Copy(buffer, tmpData, recieved);
                string response = null;

                MessageBox.Show("server received : "+ Encoding.ASCII.GetString(tmpData));
                
                if (Encoding.ASCII.GetString(tmpData) == "ehsan") response = "true";
                else response = "false";

               //place to do data base work and return a respomse



               byte[] sendData = Encoding.ASCII.GetBytes(response);
               sck.BeginSend(sendData, 0, sendData.Length, SocketFlags.None, new AsyncCallback(sendCallBack), sck);

               sck.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(recieveCallBack), sck);

            }
            catch (SocketException e)
            {
               // Console.Write(e.Message);
            }
        }

        private void sendCallBack(IAsyncResult AR)
        {
            Socket sck = (Socket)AR.AsyncState;
            sck.EndSend(AR);

        }


        public void disconnect()
        {
            serverSocket.Close();

        }
        public bool respondToRequest(string msg)
        {


/*
            byte[] sendData = Encoding.ASCII.GetBytes(msg);
            sck.BeginSend(sendData, 0, sendData.Length, SocketFlags.None, new AsyncCallback(sendCallBack), sck);

            sck.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(recieveCallBack), sck);
            */
            return true;
        }
        public bool sendFile(string file)
        {


            return true;
        }


    }
}
