// <copyright file="IReceiver.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Communication.API.Requests;
using BackEnd.Communication.API.Schemas;

namespace BackEnd.Router
{
  /// <summary>
  /// An interface containing methods that must be implemented when attached to a <see cref="Router"/>.
  /// </summary>
  internal interface IReceiver
  {
    /// <summary>
    /// Join lobby.
    /// </summary>
    /// <param name="request">Specifics of the request.</param>
    /// <returns>The response of the request to be sent to the client.</returns>
    JoinLobbyResult JoinLobby(JoinLobbyRequest request);

    /// <summary>
    /// Leave lobby.
    /// </summary>
    /// <param name="request">Specifics of the request.</param>
    void LeaveLobby(LocalRequest request);

    /// <summary>
    /// Place tower.
    /// </summary>
    /// <param name="request">Specifics of the request.</param>
    void PlaceTower(PlaceTowerRequest request);

    /// <summary>
    /// Sell tower.
    /// </summary>
    /// <param name="request">Specifics of the request.</param>
    void SellTower(SelltowerRequest request);

    /// <summary>
    /// Start game.
    /// </summary>
    /// <param name="request">Specifics of the request.</param>
    void StartGame(StartGameRequest request);

    /// <summary>
    /// Start round.
    /// </summary>
    /// <param name="request">Specifics of the request.</param>
    void StartRound(LocalRequest request);
  }
}
