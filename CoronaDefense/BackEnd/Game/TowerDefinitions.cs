// <copyright file="TowerDefinitions.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using Newtonsoft.Json;
using System.Collections.Generic;

namespace BackEnd.Game
{
  /// <summary>
  /// Collection of definitions for tower types.
  /// </summary>
  /// <remarks>
  /// This object is part of the Game Layer – Layer 3.
  /// </remarks>
  internal class TowerDefinitions
  {
    /// <summary>
    /// Gets list of tower definitions.
    /// </summary>
    public IList<TowerType> Towers { get; init; }

    /// <summary>
    /// Create a new <see cref="TowerDefinitions"/> object.
    /// </summary>
    /// <param name="jsonContent">JSON text to parse into <see cref="TowerDefinitions"/>.</param>
    /// <returns>The parsed <see cref="TowerDefinitions"/>.</returns>
    public static TowerDefinitions Parse(string jsonContent)
    {
      return JsonConvert.DeserializeObject<TowerDefinitions>(jsonContent);
    }

    /// <summary>
    /// Definition for one type of tower.
    /// </summary>
    public class TowerType
    {
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
}
