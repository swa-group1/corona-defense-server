// <copyright file="LeaveLobbyRequest.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace API.Requests
{
  /// <summary>
  /// Request to leave lobby.
  /// </summary>
  public class LeaveLobbyRequest
  {
    /// <summary>
    /// Gets ID of lobby to leave.
    /// </summary>
    public long LobbyId { get; init; }

    /// <summary>
    /// Gets access token of client.
    /// </summary>
    public string AccessToken { get; init; }
  }
}
