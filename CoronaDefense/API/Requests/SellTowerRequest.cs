// <copyright file="SellTowerRequest.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.Communication.API.Requests
{
  /// <summary>
  /// Request to place tower.
  /// </summary>
  public class SelltowerRequest : LocalRequest
  {
    /// <summary>
    /// Gets ID of tower to sell.
    /// </summary>
    public int TowerId { get; init; }
  }
}
