// <copyright file="Router.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;

namespace BackEnd.Router
{
  /// <summary>
  /// Router that route requests from an <see cref="API.API"/> to <see cref="IReceiver"/>s.
  /// </summary>
  internal partial class Router
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
    internal Router()
    {
      API.API.Instance.AttachActivateClientHandler(new ActivateClientRouter(this));
      API.API.Instance.AttachJoinLobbyHandler(new JoinLobbyRouter(this));
      API.API.Instance.AttachLeaveLobbyHandler(new LeaveLobbyRouter(this));
      API.API.Instance.AttachPlaceTowerHandler(new PlaceTowerRouter(this));
      API.API.Instance.AttachSellTowerHHandler(new SellTowerRouter(this));
      API.API.Instance.AttachStartRoundHandler(new StartRoundRouter(this));
    }

    /// <summary>
    /// Create a new random <see cref="long"/> acting as an address.
    /// </summary>s
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
