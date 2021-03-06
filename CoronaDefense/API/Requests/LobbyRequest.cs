// <copyright file="LobbyRequest.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.Communication.API.Requests
{
  /// <summary>
  /// Request to get information about lobby.
  /// </summary>
  public class LobbyRequest
  {
    /// <summary>
    /// Gets ID of lobby to get.
    /// </summary>
    public long Id { get; init; }
  }
}
