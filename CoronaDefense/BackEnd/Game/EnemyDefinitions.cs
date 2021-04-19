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

    /// <summary>
    /// Definition of one type of enemy.
    /// </summary>
    public class EnemyType
    {
      /// <summary>
      /// Gets name of type of <see cref="EnemyType"/>.
      /// </summary>
      public string Name { get; init; }

      /// <summary>
      /// Gets number of health points for <see cref="EnemyType"/>.
      /// </summary>
      public int Health { get; init; }

      /// <summary>
      /// Gets length along path this <see cref="EnemyType"/> travels per second.
      /// </summary>
      public double Speed { get; init; }

      /// <summary>
      /// Gets <see cref="SpriteSet"/> of this <see cref="EnemyType"/>.
      /// </summary>
      public SpriteSet SpriteSet { get; init; }
    }

    /// <summary>
    /// A collection of consecutive sprites in the sprite catalog belonging to the same <see cref="EnemyType"/>.
    /// </summary>
    public class SpriteSet
    {
      /// <summary>
      /// Gets sprite number of first sprite in <see cref="SpriteSet"/>.
      /// </summary>
      public int FirstSprite { get; init; }

      /// <summary>
      /// Gets number of sprites in <see cref="SpriteSet"/>.
      /// </summary>
      public int Sprites { get; init; }
    }
  }
}
