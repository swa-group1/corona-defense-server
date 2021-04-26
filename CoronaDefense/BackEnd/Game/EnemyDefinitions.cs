// <copyright file="EnemyDefinitions.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using Newtonsoft.Json;
using System.Collections.Generic;

namespace BackEnd.Game
{
  /// <summary>
  /// List of enemy types.
  /// </summary>
  /// <remarks>
  /// This object is part of the Game Layer – Layer 3.
  /// </remarks>
  internal class EnemyDefinitions
  {
    /// <summary>
    /// Gets list of <see cref="EnemyType"/>s.
    /// </summary>
    public IList<EnemyType> EnemyTypes { get; init; }

    /// <summary>
    /// Create a new <see cref="EnemyDefinitions"/> object.
    /// </summary>
    /// <param name="jsonContent">JSON text to parse into <see cref="EnemyDefinitions"/>.</param>
    /// <returns>The parsed <see cref="EnemyDefinitions"/>.</returns>
    public static EnemyDefinitions Parse(string jsonContent)
    {
      return JsonConvert.DeserializeObject<EnemyDefinitions>(jsonContent);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
      return JsonConvert.SerializeObject(this);
    }
  }
}
