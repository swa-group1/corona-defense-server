// <copyright file="EnemyComponent.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.Game.Components
{
  /// <summary>
  /// Information about an enemy entity.
  /// </summary>
  internal struct EnemyComponent
  {
    /// <summary>
    /// Length along path were enemy was last hit.
    /// </summary>
    public double PreviousImpactPosition;

    /// <summary>
    /// Time from start of round when enemy was last hit.
    /// </summary>
    public double PreviousImpactTime;

    /// <summary>
    /// Number of sprite of this enemy currently.
    /// </summary>
    /// <remarks>
    /// All enemies have a continous sequence of sprites with length at least equal to the number of lives. It's therefore intended to increment this number by one every time the enemy takes damage.
    /// </remarks>
    public int SpriteNumber;
  }
}
