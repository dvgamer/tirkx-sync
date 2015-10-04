using iHelpful_Tirkx.Collection;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace iHelpful_Tirkx
{
    public class iSocket
    {
        public static ResponseBuilder Connect(RequestBuilder request)
        {
            ResponseBuilder response = new ResponseBuilder();
            try
            {
                Socket s = Connect(request.CurrentURL.Host, request.CurrentURL.Port);
                if (s == null) throw new Exception("Connection failed");

                Byte[] bytesSent = Encoding.UTF8.GetBytes(request.ToString());
                Byte[] bytesReceived = new Byte[request.ToString().Length];

                // Send request to the server.
                s.Send(bytesSent, bytesSent.Length, 0);
                int bytes = 0;
                do
                {
                    // Receive response to the server.
                    bytes = s.Receive(bytesReceived, bytesReceived.Length, 0);
                    response.Append(Encoding.UTF8.GetString(bytesReceived, 0, bytes));
                }
                while (bytes > 0);
            }
            catch (Exception ex)
            {
                response.Clear();
                response.AppendLine(ex.Message);
                response.AppendLine(ex.StackTrace);
            }
            return response;
        }

        private static Socket Connect(string server, int port)
        {
            Socket s = null;
            IPHostEntry hostEntry = Dns.GetHostEntry(server);
            foreach (IPAddress address in hostEntry.AddressList)
            {
                IPEndPoint ipe = new IPEndPoint(address, port);
                Socket tempSocket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                tempSocket.Connect(ipe);
                if (tempSocket.Connected) { s = tempSocket; break; } else { continue; }
            }
            return s;
        }

    }
}
