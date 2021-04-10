// <copyright file="PlaceTowerRequest.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace API.Requests
{
  /// <summary>
  /// Request to place tower.
  /// </summary>
  public class PlaceTowerRequest
  {
    /// <summary>
    /// Gets ID of lobby in which to place tower.
    /// </summary>
    public long LobbyId { get; init; }

    /// <summary>
    /// Gets access token of client.
    /// </summary>
    public string AccessToken { get; init; }

    /// <summary>
    /// Gets X position of tower to place.
    /// </summary>
    public int XPosition { get; init; }

    /// <summary>
    /// Gets Y position of tower to place.
    /// </summary>
    public int YPosition { get; init; }
  }
}

