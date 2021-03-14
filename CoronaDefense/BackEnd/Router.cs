// <copyright file="Router.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.APIEndpoint;
using System;
using System.Collections.Generic;

namespace BackEnd
{
  /// <summary>
  /// Router that route <see cref="ILocalMessage"/>s from an <see cref="APIEndpoint"/> to <see cref="Router.IReceiver"/>s.
  /// </summary>
  internal partial class Router : IObserver
  {
    /// <summary>
    /// Internal backing generator used for address generation.
    /// </summary>
    private static readonly Random Random = new Random();

    /// <summary>
    /// Map from <see cref="long"/> addresses to the receivers of this <see cref="Router"/>.
    /// </summary>
    private readonly Dictionary<long, IReceiver> receivers = new Dictionary<long, IReceiver>();

    /// <summary>
    /// Initializes a new instance of the <see cref="Router"/> class.
    /// </summary>
    /// <param name="api">The Api Endpoint to connect to.</param>
    public Router(IAPIEndpoint api)
    {
      api.AttachObserver(this);
    }

    /// <inheritdoc/>
    public void OnGlobalMessage(IGlobalMessage message)
    {
    }

    /// <inheritdoc/>
    public void OnLocalMessage(ILocalMessage message)
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
    /// Register <see cref="IReceiver"/> as a possible destination for messages routed through it.
    /// </summary>
    /// <param name="receiver">Receiver to register.</param>
    /// <returns>Generated <see cref="long"/> used as address for supplied <see cref="IReceiver"/>.</returns>
    public long Register(IReceiver receiver)
    {
      // Find address
      long address;
      do
      {
        address = GetRandomAddress();
      }
      while (this.receivers.ContainsKey(address));

      // Register receiver
      this.receivers[address] = receiver;
      return address;
    }
  }
}
