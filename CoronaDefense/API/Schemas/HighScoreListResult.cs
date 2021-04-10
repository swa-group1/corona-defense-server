// <copyright file="HighScoreListResult.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Schemas
{
  /// <summary>
  /// List of the highest scores observed.
  /// </summary>
  public class HighScoreListResult : RequestResult
  {
    /// <summary>
    /// Gets list of the highest scores observed.
    /// </summary>
    [Required]
    public List<Score> Scores { get; set; } = new List<Score>();

    /// <summary>
    /// A personal indicator and the value of their game score.
    /// </summary>
    public class Score
    {
      /// <summary>
      /// Personal indicator, or nickname, associated with this <see cref="Score"/>.
      /// </summary>
      [Required]
      public string Name { get; set; } = string.Empty;

      /// <summary>
      /// Amount of points achieved in the game this <see cref="Score"/> is from.
      /// </summary>
      [Required]
      public int Value { get; set; }
    }
  }
}
