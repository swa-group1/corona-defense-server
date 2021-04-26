// <copyright file="TowerType.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.Game
{
  /// <summary>
  /// Definition for one type of tower.
  /// </summary>
  public class TowerType
  {
    /// <summary>
    /// Gets a value indicating whether this <see cref="TowerType"/> can spot camouflaged <see cref="EnemyType"/>.
    /// </summary>
    public bool CanSpotCamo { get; init; }

    /// <summary>
    /// Gets description of tower.
    /// </summary>
    public string Description { get; init; }

    /// <summary>
    /// Gets the cost of this <see cref="TowerType"/> at medium difficulty.
    /// </summary>
    public int MediumCost { get; init; }

    /// <summary>
    /// Gets the speed of the projectiles of this <see cref="TowerType"/>.
    /// </summary>
    public double ProjectileSpeed { get; init; }

    /// <summary>
    /// Gets the sprite number of projectiles of this <see cref="TowerType"/>.
    /// </summary>
    public int ProjectileSpriteNumber { get; init; }

    /// <summary>
    /// Gets the radius of the circle range this <see cref="TowerType"/> can see.
    /// </summary>
    public double Range { get; init; }

    /// <summary>
    /// Gets the time in seconds it takes for this <see cref="TowerType"/> to reload.
    /// </summary>
    public double ReloadTime { get; init; }

    /// <summary>
    /// Gets the sprite number of this <see cref="TowerType"/>.
    /// </summary>
    public int TowerSpriteNumber { get; init; }

    /// <summary>
    /// Gets the number associated with this <see cref="TowerType"/>.
    /// </summary>
    public int TypeNumber { get; init; }
  }
}
