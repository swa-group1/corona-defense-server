// <copyright file="JoinLobbyRequest.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.Communication.API.Requests
{
  /// <summary>
  /// Request to create lobby.
  /// </summary>
  public class JoinLobbyRequest
  {
    /// <summary>
    /// Gets ID of lobby.
    /// </summary>
    public long LobbyId { get; init; }

    /// <summary>
    /// Gets password of lobby.
    /// </summary>
    public string Password { get; init; }

    /// <summary>
    /// Gets connection number client has obtained from connection broker.
    /// </summary>
    public long ConnectionNumber { get; init; }
  }
}
