// <copyright file="ConnectionBroker.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace BackEnd
{
  /// <summary>
  /// Object that accepts internet socket connections.
  /// </summary>
  internal class ConnectionBroker : ServerRandom, IDisposable
  {
    /// <summary>
    /// Port number this port will bind to.
    /// </summary>
    private const int PortNumber = 19001;

    /// <summary>
    /// Gets a map between connection numbers and <see cref="Socket"/>s that are open, but that are not used in any lobbies.
    /// </summary>
    private Dictionary<long, Socket> ConnectionPool { get; } = new Dictionary<long, Socket>();

    /// <summary>
    /// Gets public socket that accepts new connections.
    /// </summary>
    private Socket PublicSocket { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConnectionBroker"/> class.
    /// </summary>
    public ConnectionBroker()
    {
      IPEndPoint endPoint = new IPEndPoint(IPAddress.IPv6Any, PortNumber);

      this.PublicSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
      this.PublicSocket.Bind(endPoint);
      this.PublicSocket.Listen(16);

      this.Start();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
      this.PublicSocket?.Dispose();
    }

    /// <summary>
    /// Add unused established connection to the <see cref="ConnectionPool"/>.
    /// </summary>
    /// <param name="connectionNumber">Connection number assigned to the connection with the supplied <paramref name="socket"/>.</param>
    /// <param name="socket"><see cref="Socket"/> of connection.</param>
    private void AddConnectionToPool(long connectionNumber, Socket socket)
    {
      this.ConnectionPool.Add(connectionNumber, socket);
      Console.WriteLine($"Connection number {connectionNumber} added to connection pool.");
    }

    /// <summary>
    /// Accept and process connections.
    /// </summary>
    private void Start()
    {
      while (true)
      {
        try
        {
          Console.WriteLine("Waiting for connection...");
          Socket clientSocket = this.PublicSocket.Accept();
          Console.WriteLine("Connection accepted.");
          long connectionNumber;
          do
          {
            connectionNumber = this.RandomLong;
          }
          while (this.ConnectionPool.ContainsKey(connectionNumber));
          this.AddConnectionToPool(connectionNumber, clientSocket);
          _ = Task.Run(async () => { await WriteConnectionNumber(clientSocket, connectionNumber); });
        }
        catch (ObjectDisposedException)
        {
          break;
        }
      }
    }

    /// <summary>
    /// Write supplied <paramref name="connectionNumber"/> to supplied <paramref name="socket"/>.
    /// </summary>
    /// <param name="socket"><see cref="Socket"/> to write <paramref name="connectionNumber"/> to.</param>
    /// <param name="connectionNumber"><see cref="long"/> to write to <paramref name="socket"/>.</param>
    /// <returns>Task handle.</returns>
    private static async Task WriteConnectionNumber(Socket socket, long connectionNumber)
    {
      Console.WriteLine($"Writing connection number {connectionNumber} to socket...");
      NetworkStream stream = new NetworkStream(socket, false);

      byte[] bytes = BitConverter.GetBytes(connectionNumber);

      if (BitConverter.IsLittleEndian)
      {
        Array.Reverse(bytes);
      }

      await stream.WriteAsync(bytes.AsMemory(0, 8));

      await stream.DisposeAsync();
      Console.WriteLine($"Connection number {connectionNumber} written to socket.");
    }
  }
}
