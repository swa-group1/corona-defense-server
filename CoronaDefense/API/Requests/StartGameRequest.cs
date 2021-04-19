// <copyright file="StartGameRequest.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.Communication.API.Requests
{
  /// <summary>
  /// Request to place tower.
  /// </summary>
  public class StartGameRequest : LocalRequest
  {
    /// <summary>
    /// Gets number of stage.
    /// </summary>
    public byte StageNumber { get; init; }

    /// <summary>
    /// Gets the desired difficulty of the game.
    /// </summary>
    public Difficulties Difficulty { get; init; }

    /// <summary>
    /// Available difficulties.
    /// </summary>
    public enum Difficulties
    {
      /// <summary>
      /// Easiest difficulty.
      /// </summary>
      EASY,

      /// <summary>
      /// Medium difficulty.
      /// </summary>
      MEDIUM,

      /// <summary>
      /// Hard difficulty.
      /// </summary>
      HARD,
    }
  }
}
