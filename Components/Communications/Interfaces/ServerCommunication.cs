using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Communication
{
	public class ServerCommunication
	{
		// necessary attributes
		private Socket serverMainSocket;
		private List<Socket> clientsSockets;

		/*
		 * in constructor server set up and start to listening
		 */
		public ServerCommunication ()
		{
			// setup server
		}

		/* this function shutdown server and destroy client sockets
		 * also destroy stuffs
		 */
		public void shutdown();

		/*
		 * when client connect() method call this functoin allocate new socket for it and add it to clientSockets
		 */
		private void acceptClientCallBack(IAsyncResult AR);

		/*
		 * when client send a reauest this function called and response to it
		 * among this function we must comminicate with Db through DbCenter object
		 */
		private void receiveRequestCallBack(IAsyncResult AR);

		private 
	}
}

