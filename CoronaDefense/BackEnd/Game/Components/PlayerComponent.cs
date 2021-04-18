// <copyright file="PlayerComponent.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.Game.Components
{
  /// <summary>
  /// Component describing the player.
  /// </summary>
  internal struct PlayerComponent
  {
    /// <summary>
    /// Value describing how much money the player has to spend.
    /// </summary>
    public int Balance;

    /// <summary>
    /// Value describing how much health the player has left.
    /// </sunmmary>
    public int Health;
  }
}
