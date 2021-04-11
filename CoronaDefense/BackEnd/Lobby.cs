// <copyright file="Lobby.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using API.Requests;
using API.Schemas;
using BackEnd.Router;

namespace BackEnd
{
  /// <summary>
  /// Model of a game-instance. Contains all the state for one running instance of the game.
  /// </summary>
  internal class Lobby : IReceiver
  {
    /// <summary>
    /// Gets the ID of this <see cref="Lobby"/>.
    /// </summary>
    public long Id { get; }

    /// <summary>
    /// Gets the name of this <see cref="Lobby"/>.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the number of players, or clients, currently connected to this <see cref="Lobby"/>.
    /// </summary>
    public int PlayerCount { get; private set; } = 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="Lobby"/> class.
    /// </summary>
    /// <param name="name">Name of the new <see cref="Lobby"/>.</param>
    /// <param name="router">Router the new <see cref="Lobby"/> should connect to.</param>
    public Lobby(string name, Router.Router router)
    {
      this.Id = router.Register(this);
      this.Name = name;
    }

    /// <inheritdoc/>
    public void ActivateClient(LocalRequest request)
    {
      throw new System.NotImplementedException();
    }

    /// <inheritdoc/>
    public JoinLobbyResult JoinLobby(JoinLobbyRequest request)
    {
      throw new System.NotImplementedException();
    }

    /// <inheritdoc/>
    public void LeaveLobby(LocalRequest request)
    {
      throw new System.NotImplementedException();
    }

    /// <inheritdoc/>
    public LobbyResult GetLobby(LobbyRequest request)
    {
      throw new System.NotImplementedException();
    }

    /// <inheritdoc/>
    public void PlaceTower(PlaceTowerRequest request)
    {
      throw new System.NotImplementedException();
    }

    /// <inheritdoc/>
    public void SellTower(SelltowerRequest request)
    {
      throw new System.NotImplementedException();
    }

    /// <inheritdoc/>
    public void StartRound(LocalRequest request)
    {
      throw new System.NotImplementedException();
    }
  }
}
