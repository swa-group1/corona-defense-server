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
  internal partial class Router : ServerRandom
  {
    /// <summary>
    /// Map from <see cref="long"/> addresses to the receivers of this <see cref="Router"/>.
    /// </summary>
    private readonly Dictionary<long, IReceiver> receivers = new Dictionary<long, IReceiver>();

    /// <summary>
    /// Initializes a new instance of the <see cref="Router"/> class.
    /// </summary>
    internal Router()
    {
      API.API.Instance.AttachJoinLobbyHandler(new JoinLobbyRouter(this));
      API.API.Instance.AttachLeaveLobbyHandler(new LeaveLobbyRouter(this));
      API.API.Instance.AttachPlaceTowerHandler(new PlaceTowerRouter(this));
      API.API.Instance.AttachSellTowerHHandler(new SellTowerRouter(this));
      API.API.Instance.AttachStartGameHandler(new StartGameRouter(this));
      API.API.Instance.AttachStartRoundHandler(new StartRoundRouter(this));
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
        address = this.RandomLong;
      }
      while (this.receivers.ContainsKey(address));

      // Register receiver
      this.receivers[address] = receiver;
      return address;
    }
  }
}
