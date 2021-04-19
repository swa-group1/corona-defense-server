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
    /// List of tower definitions.
    /// </summary>
    public IList<Tower> Towers;

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
    public class Tower
    {
      public string Description;

      public int MediumCost;

      public double ProjectileSpeed;

      public int ProjectileSpriteNumber;

      public double Range;

      public double ReloadTime;

      public int TowerSpriteNumber;

      public int TypeNumber;
    }
  }
}
