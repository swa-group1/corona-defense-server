// <copyright file="TowerComponent.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.Game.Components
{
  /// <summary>
  /// Component that contains information about a tower.
  /// </summary>
  internal struct TowerComponent
  {
    /// <summary>
    /// Distance travelled by projectiles per second.
    /// </summary>
    public double ProjectileSpeed;

    /// <summary>
    /// Number of projectile sprite.
    /// </summary>
    public int ProjectileSpriteNumber;

    /// <summary>
    /// Time the tower uses to reload.
    /// </summary>
    public double ReloadTime;

    /// <summary>
    /// Time left until the tower can shoot again.
    /// </summary>
    public double TimeUntilReloaded;

    /// <summary>
    /// Number of tower sprite.
    /// </summary>
    public int TowerSpriteNumber;
  }
}
