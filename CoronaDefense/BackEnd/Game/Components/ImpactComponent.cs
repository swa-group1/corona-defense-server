// <copyright file="ImpactComponent.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.Game.Components
{
  /// <summary>
  /// Information about previous impact a entity took part in.
  /// </summary>
  internal struct ImpactComponent
  {
    /// <summary>
    /// Length along path were enemy was last hit.
    /// </summary>
    public double PreviousImpactPosition;

    /// <summary>
    /// Time from start of round when enemy was last hit.
    /// </summary>
    public double PreviousImpactTime;
  }
}
