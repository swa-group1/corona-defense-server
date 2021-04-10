// <copyright file="SellTowerRequest.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace API.Requests
{
  /// <summary>
  /// Request to place tower.
  /// </summary>
  public class SelltowerRequest
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
    /// Gets ID of tower to sell.
    /// </summary>
    public int TowerId { get; init; }

  }
}

