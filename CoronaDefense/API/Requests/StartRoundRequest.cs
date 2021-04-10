// <copyright file="StartRoundRequest.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace API.Requests
{
  /// <summary>
  /// Request to start round.
  /// </summary>
  public class StartRoundRequest
  {
    /// <summary>
    /// Gets ID of lobby in which to place tower.
    /// </summary>
    public long LobbyId { get; init; }

    /// <summary>
    /// Gets access token of client.
    /// </summary>
    public string AccessToken { get; init; }
  }
}

