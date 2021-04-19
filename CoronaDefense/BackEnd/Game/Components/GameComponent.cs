// <copyright file="GameComponent.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Communication;
using BackEnd.Communication.API.Requests;

namespace BackEnd.Game.Components
{
  /// <summary>
  /// Component to store game constants.
  /// </summary>
  internal struct GameComponent
  {
    /// <summary>
    /// Broadcaster to send messages over.
    /// </summary>
    public Broadcaster Broadcaster;

    /// <summary>
    /// Stage of this game.
    /// </summary>
    public Stage Stage;

    /// <summary>
    /// Value in seconds describing the duration of one tick.
    /// </summary>
    public double TickDuration;

    /// <summary>
    /// Time passed this round.
    /// </summary>
    public double Time;

    /// <summary>
    /// Factor that tower costs should be multiplied by.
    /// </summary>
    public double TowerCostFactor;

    /// <summary>
    /// Factor that money reimbursed from selling towers should be multiplied with.
    /// </summary>
    public double TowerSaleFactor;
  }
}
