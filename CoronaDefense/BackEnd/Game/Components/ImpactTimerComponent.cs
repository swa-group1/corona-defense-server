// <copyright file="ImpactTimerComponent.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace BackEnd.Game.Components
{
  /// <summary>
  /// Component containing timers for when this entity will collide with projectiles.
  /// </summary>
  internal struct ImpactTimerComponent
  {
    /// <summary>
    /// List of timers for when this entity will collide with projectiles.
    /// </summary>
    public List<double> ImpactTimers;
  }
}
