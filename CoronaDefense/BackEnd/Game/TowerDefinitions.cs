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
  }
}
