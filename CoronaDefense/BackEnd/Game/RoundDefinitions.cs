// <copyright file="RoundDefinitions.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using Newtonsoft.Json;
using System.Collections.Generic;

namespace BackEnd.Game
{
  /// <summary>
  /// Descriptions for which enemies to spawn each round and when.
  /// </summary>
  /// <remarks>
  /// This object is part of the Game Layer – Layer 3.
  /// </remarks>
  internal class RoundDefinitions
  {
    /// <summary>
    /// Gets list of lists of <see cref="RoundSection"/>s. Each list is one round.
    /// </summary>
    public IList<IList<RoundSection>> Rounds { get; init; }

    /// <summary>
    /// Create a new <see cref="RoundDefinitions"/> object.
    /// </summary>
    /// <param name="jsonContent">JSON text to parse into <see cref="RoundDefinitions"/>.</param>
    /// <returns>The parsed <see cref="RoundDefinitions"/>.</returns>
    public static RoundDefinitions Parse(string jsonContent)
    {
      return JsonConvert.DeserializeObject<RoundDefinitions>(jsonContent);
    }

    /// <summary>
    /// Definition of one section in a round.
    /// </summary>
    public class RoundSection
    {
      /// <summary>
      /// Gets type of enemy in section. Valid values for this field can be found in enemy definition document.
      /// </summary>
      public string EnemyType { get; init; }

      /// <summary>
      /// Gets number of enemies of this type.
      /// </summary>
      public int Count { get; init; }

      /// <summary>
      /// Gets distance between the enemies in this section measured in time.
      /// </summary>
      /// <remarks>
      /// The time is also added before the first enemy, not only between them in the section.
      /// </remarks>
      public double DeltaTime { get; init; }
    }
  }
}
