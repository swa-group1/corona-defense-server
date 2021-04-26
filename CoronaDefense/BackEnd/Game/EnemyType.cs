// <copyright file="EnemyType.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.Game
{
  /// <summary>
  /// Definition of one type of enemy.
  /// </summary>
  public class EnemyType
  {
    /// <summary>
    /// Gets a value indicating whether the enemy is camouflaged or not.
    /// </summary>
    public bool Camo { get; init; }

    /// <summary>
    /// Gets number of health points for <see cref="EnemyType"/>.
    /// </summary>
    public int Health { get; init; }

    /// <summary>
    /// Gets name of type of <see cref="EnemyType"/>.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Gets length along path this <see cref="EnemyType"/> travels per second.
    /// </summary>
    public double Speed { get; init; }

    /// <summary>
    /// Gets the type of enemy that this enemy shold turn into upon damage.
    /// </summary>
    /// <remarks>
    /// If the enemy shuld not transform upon death this should be set to "None".
    /// </remarks>
    public string NextType { get; init; }

    /// <summary>
    /// Gets sprite number of the enemy's sprite.
    /// </summary>
    public int SpriteNumber { get; init; }
  }
}
