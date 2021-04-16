// <copyright file="PathSpeedComponent.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.Game.Components
{
  /// <summary>
  /// Component describing the speed of an entity along the path.
  /// </summary>
  internal struct PathSpeedComponent
  {
    /// <summary>
    /// Value describing how far the entity moves along the path in one second.
    /// </summary>
    public double Speed;
  }
}
