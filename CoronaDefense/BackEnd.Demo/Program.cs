// <copyright file="Program.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using System;
using System.Net;
using System.Net.Sockets;

namespace BackEnd.Demo
{
  /// <summary>
  /// Program that connects to <see cref="BackEnd"/> via network sockets.
  /// </summary>
  internal static class Program
  {
    private const int ServerPortNumber = 19001;

    /// <summary>
    /// Main code entry-point.
    /// </summary>
    internal static void Main()
    {
      IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.IPv6Loopback, ServerPortNumber);

      Socket socket = new Socket(serverEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
      socket.Connect(serverEndPoint);

      byte[] connectionNumberBytes = new byte[8];
      socket.Receive(connectionNumberBytes);

      if (BitConverter.IsLittleEndian)
      {
        Array.Reverse(connectionNumberBytes);
      }

      socket.Close();

      Console.WriteLine(BitConverter.ToInt64(connectionNumberBytes));
    }
  }
}
