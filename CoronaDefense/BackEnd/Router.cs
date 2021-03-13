// <copyright file="Router.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd
{
  using System;
  using System.Collections.Generic;

  /// <summary>
  /// Router that route <see cref="APIEndpoint.ILocalMessage"/>s from <see cref="APIEndpoint"/> to <see cref="Router.IReceiver"/>s.
  /// </summary>
  internal partial class Router : APIEndpoint.IObserver
  {
    /// <summary>
    /// Internal backing generator used for address generation.
    /// </summary>
    private static readonly Random Random = new Random();

    /// <summary>
    /// Create a new random <see cref="long"/> acting as an address.
    /// </summary>
    /// <returns>The generated <see cref="long"/>.</returns>
    private static long GetRandomAddress()
    {
      byte[] buffer = new byte[8];
      Random.NextBytes(buffer);
      long address = BitConverter.ToInt64(buffer, 0);
      return address;
    }

    /// <summary>
    /// Receivers of this <see cref="Router"/>.
    /// </summary>
    private readonly Dictionary<long, IReceiver> receivers = new Dictionary<long, IReceiver>();

    /// <summary>
    /// Initializes a new instance of the <see cref="Router"/> class.
    /// </summary>
    /// <param name="api"> The Api Endpoint to connect to. </param>
    public Router(APIEndpoint api)
    {
      api.AttachObserver(this);
    }

    /// <inheritdoc/>
    public void OnGlobalMessage(APIEndpoint.IGlobalMessage message)
    {
    }

    /// <inheritdoc/>
    public void OnLocalMessage(APIEndpoint.ILocalMessage message)
    {
      long address = message.Address;

      // Check if this router recognizes the address.
      if (!this.receivers.TryGetValue(address, out IReceiver receiver))
      {
        return;
      }

      receiver.OnMessage(message);
    }

    /// <summary>
    /// Register <see cref="IReceiver"/> as a possible destination for messages routed through it.
    /// </summary>
    /// <param name="receiver">Receiver to register.</param>
    /// <returns>Generated <see cref="long"/> used as address for supplied <see cref="IReceiver"/>.</returns>
    public long Register(IReceiver receiver)
    {
      long address = GetRandomAddress();
      this.receivers[address] = receiver;
      return address;
    }
  }
}
