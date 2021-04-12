// <copyright file="Lobby.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using API.Requests;
using API.Schemas;
using BackEnd.Router;
using System.Collections.Generic;
using System.Timers;

namespace BackEnd
{
  /// <summary>
  /// Model of a game-instance. Contains all the state for one running instance of the game.
  /// </summary>
  internal class Lobby : ServerRandom, IReceiver
  {
    /// <summary>
    /// Gets the set of access tokens of clients currently connected to this <see cref="Lobby"/>.
    /// </summary>
    private HashSet<long> AccessTokens { get; } = new HashSet<long>();

    private Broadcaster Broadcaster { get; }

    /// <summary>
    /// Default time before a <see cref="Lobby"/> should close itself because of inactivity.
    /// </summary>
    private const int LobbyTimeOut = 2 * 60 * 1000;

    /// <summary>
    /// Gets the ID of this <see cref="Lobby"/>.
    /// </summary>
    public long Id { get; }

    /// <summary>
    /// Gets the name of this <see cref="Lobby"/>.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets password of this <see cref="Lobby"/>.
    /// </summary>
    private string Password { get; }

    /// <summary>
    /// Backing-field for <see cref="PlayerCount"/>.
    /// </summary>
    private int playerCount;

    /// <summary>
    /// If <see cref="playerCount"/> is currently 0, the <see cref="Timer"/> counting towards.
    /// </summary>
    private Timer playerCountZeroTimer = null;

    /// <summary>
    /// Gets the number of players, or clients, currently connected to this <see cref="Lobby"/>.
    /// </summary>
    public int PlayerCount
    {
      get
      {
        return this.playerCount;
      }

      private set
      {
        this.playerCount = value;

        if (value == 0)
        {
          Timer timer = new Timer()
          {
            AutoReset = false,
            Interval = LobbyTimeOut,
          };
          timer.Elapsed += this.OnNoPlayerTimerElapsed;
          timer.Start();
          this.playerCountZeroTimer = timer;
        }
        else
        {
          this.playerCountZeroTimer?.Stop();
          this.playerCountZeroTimer = null;
        }
      }
    }

    private List<IObserver> Observers { get; } = new List<IObserver>();

    /// <summary>
    /// Initializes a new instance of the <see cref="Lobby"/> class.
    /// </summary>
    /// <param name="name">Name of the new <see cref="Lobby"/>.</param>
    /// <param name="password">Password of the new <see cref="Lobby"/>.</param>
    /// <param name="connectionBroker"></param>
    /// <param name="router">Router the new <see cref="Lobby"/> should connect to.</param>
    public Lobby(string name, string password, ConnectionBroker connectionBroker, Router.Router router)
    {
      this.Id = router.Register(this);
      this.Name = name;
      this.Password = password;

      this.PlayerCount = 0;

      this.Broadcaster = new Broadcaster(connectionBroker);
    }

    /// <inheritdoc/>
    public JoinLobbyResult JoinLobby(JoinLobbyRequest request)
    {
      if (request.Password != this.Password)
      {
        return new JoinLobbyResult()
        {
          Success = false,
          Details = "Password was not correct.",

          LobbyId = this.Id,
        };
      }

      long accessToken;
      do
      {
        accessToken = this.RandomLong;
      }
      while (this.AccessTokens.Contains(accessToken));

      if (!this.Broadcaster.TryAssociateWithConnection(accessToken, request.ConnectionNumber))
      {
        return new JoinLobbyResult()
        {
          Success = false,
          Details = "Connection number was not found to map to a valid open connection to the server.",

          LobbyId = this.Id,
        };
      }

      this.AccessTokens.Add(accessToken);
      this.playerCount++;

      this.Broadcaster.Ping();

      return new JoinLobbyResult()
      {
        AccessToken = accessToken,
        LobbyId = this.Id,
      };
    }

    /// <inheritdoc/>
    public void LeaveLobby(LocalRequest request)
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
    public void StartGame(StartGameRequest request)
    {
      throw new System.NotImplementedException();
    }

    /// <inheritdoc/>
    public void StartRound(LocalRequest request)
    {
      throw new System.NotImplementedException();
    }

    /// <summary>
    /// Add observer to this Lobby.
    /// </summary>
    /// <param name="observer">Observer to notify of events in this <see cref="Lobby"/>.</param>
    public void AddObserver(IObserver observer)
    {
      this.Observers.Add(observer);
    }

    /// <summary>
    /// Request that this <see cref="Lobby"/> closes itself.
    /// </summary>
    private void OnNoPlayerTimerElapsed(object sender, ElapsedEventArgs e)
    {
      this.Close();
    }

    /// <summary>
    /// Request that this <see cref="Lobby"/> closes itself.
    /// </summary>
    public void Close()
    {
      foreach (IObserver observer in this.Observers)
      {
        observer.OnClose(this.Id);
      }
    }

    /// <summary>
    /// Observer that reacts to a selection of events that can befall a <see cref="Lobby"/>.
    /// </summary>
    public interface IObserver
    {
      /// <summary>
      /// Observer gets an opportunity to react to a <see cref="Lobby"/> closing.
      /// </summary>
      /// <param name="lobbyId">ID of lobby that closed.</param>
      void OnClose(long lobbyId);
    }
  }
}
