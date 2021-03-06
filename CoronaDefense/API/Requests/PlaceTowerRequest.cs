// <copyright file="PlaceTowerRequest.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.Communication.API.Requests
{
  /// <summary>
  /// Request to place tower.
  /// </summary>
  public class PlaceTowerRequest : LocalRequest
  {
    /// <summary>
    /// Gets the type of tower to place.
    /// </summary>
    public int TowerTypeNumber { get; init; }

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
