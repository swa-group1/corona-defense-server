// <copyright file="StartGameRequest.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace API.Requests
{
  /// <summary>
  /// Request to place tower.
  /// </summary>
  public class StartGameRequest : LocalRequest
  {
    /// <summary>
    /// Number of stage
    /// </summary>
    public byte StageNumber { get; init; }

    /// <summary>
    /// The desired difficulty of the game.
    /// </summary>
    public Difficulties Difficulty { get; init; }

    /// <summary>
    /// Available difficulties
    /// </summary>
    public enum Difficulties
    {
      EASY,
      MEDIUM,
      HARD,
    }
  }
}

