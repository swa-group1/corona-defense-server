using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable enable

namespace API
{
  /// <summary>
  /// List of the highest scores observed.
  /// </summary>
  public class HighScoreList
  {
    /// <summary>
    /// Gets list of the highest scores observed.
    /// </summary>
    [Required]
    [JsonProperty(Required = Required.DisallowNull)]
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
      [JsonProperty(Required = Required.DisallowNull)]
      public string Name { get; set; } = string.Empty;

      /// <summary>
      /// Amount of points achieved in the game this <see cref="Score"/> is from.
      /// </summary>
      public int Value { get; set; }
    }
  }
}
