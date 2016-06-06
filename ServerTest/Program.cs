using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TMR;

namespace ServerTest
{
	class Program
	{
		static void Main(string[] args)
		{
			enterHostPort:
			ushort port;
			Console.Write("Open Port> ");
			try
			{
				port = Convert.ToUInt16(Console.ReadLine().Trim());
			}
			catch
			{
				Console.WriteLine("Please re-enter.");
				goto enterHostPort;
			}

			Server s = new Server();
			s.Data.ServerName = "TestServer";
			s.Data.MaxUserCount = 5;
			s.Data.ServerImage = Utility.TransparentImage;

			s.ReceiveMessage += new MessageEventHandler((e) => Console.WriteLine(e.Message.Text));
			s.Start(port);

			while (true)
			{
				string msg;
				Console.Write("Message (Exit: X)> ");
				msg = Console.ReadLine().Trim();

				if (string.IsNullOrEmpty(msg))
					continue;

				if (msg.Equals("X"))
				{
					s.Stop();
					Console.ReadLine();
					return;
				}

				s.SendToAll(new Message() { Text = "[Server] " + msg, Type = MessageType.Chat });
			}
		}
	}
}
