// <copyright file="PathPositionComponent.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.Game.Components
{
  /// <summary>
  /// Component describing a position along the path.
  /// </summary>
  internal struct PathPositionComponent
  {
    /// <summary>
    /// Value describing how far along the path the position is located.
    /// </summary>
    public double LengthTraveled;
  }
}
