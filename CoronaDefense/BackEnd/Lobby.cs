// <copyright file="Lobby.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using API.Requests;
using API.Schemas;
using BackEnd.Game;
using BackEnd.Router;
using System;
using System.Collections.Generic;
using System.Timers;

namespace BackEnd
{
  /// <summary>
  /// Model of a game-instance. Contains all the state for one running instance of the game.
  /// </summary>
  internal class Lobby : IDisposable, IReceiver
  {
    /// <summary>
    /// Default time before a <see cref="Lobby"/> should close itself because of inactivity.
    /// </summary>
    private const int LobbyTimeOut = 2 * 60 * 1000;

    /// <summary>
    /// Gets the set of access tokens of clients currently connected to this <see cref="Lobby"/>.
    /// </summary>
    private HashSet<long> AccessTokens { get; } = new HashSet<long>();

    private Broadcaster Broadcaster { get; }

    private bool Disposed { get; set; } = false;

    private StartGameRequest.Difficulties Difficulty { get; set; }

    private EcsContainer EcsContainer { get; set; }

    /// <summary>
    /// Gets the ID of this <see cref="Lobby"/>.
    /// </summary>
    public long Id { get; }

    /// <summary>
    /// Gets or sets the current state of the lobby.
    /// </summary>
    private Mode LobbyMode { get; set; } = Mode.LobbyMode;

    /// <summary>
    /// Gets the name of this <see cref="Lobby"/>.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets or sets the number of rounds.
    /// </summary>
    private int NumberOfRounds { get; set; }

    private List<IObserver> Observers { get; } = new List<IObserver>();

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

    private short RoundNumber { get; set; } = 1;

    /// <summary>
    /// Initializes a new instance of the <see cref="Lobby"/> class.
    /// </summary>
    /// <param name="name">Name of the new <see cref="Lobby"/>.</param>
    /// <param name="password">Password of the new <see cref="Lobby"/>.</param>
    /// <param name="connectionBroker"><see cref="ConnectionBroker"/> that this <see cref="Lobby"/> should get open connections from.</param>
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
        accessToken = ServerRandom.RandomLong;
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

      _ = this.AccessTokens.Add(accessToken);
      this.PlayerCount++;

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
      if (this.AccessTokens.Remove(request.AccessToken))
      {
        this.Broadcaster.DisconnectClient(request.AccessToken);
        this.PlayerCount--;
      }
    }

    /// <inheritdoc/>
    public void PlaceTower(PlaceTowerRequest request)
    {
      if (this.LobbyMode != Mode.InputMode)
      {
        return;
      }

      this.EcsContainer.PlaceTowerSystem.PlaceTower(request);
    }

    /// <inheritdoc/>
    public void SellTower(SelltowerRequest request)
    {
      throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public void StartGame(StartGameRequest request)
    {
      if (this.LobbyMode != Mode.LobbyMode)
      {
        return;
      }

      this.Difficulty = request.Difficulty;
      this.LobbyMode = Mode.InputMode;
      this.RoundNumber = 1;

      this.Broadcaster.GameMode(request.StageNumber);
      this.Broadcaster.InputRound(this.RoundNumber);

      EnemyDefinitions enemies = EnemyDefinitions.Parse(StorageAPI.DownloadEnemies());
      RoundDefinitions rounds = RoundDefinitions.Parse(StorageAPI.DownloadRounds());
      Stage stage = Stage.Parse(StorageAPI.DownloadStage(request.StageNumber));
      TowerDefinitions towers = TowerDefinitions.Parse(StorageAPI.DownloadTowers());
      this.NumberOfRounds = rounds.Rounds.Count;
      this.EcsContainer = new EcsContainer(
        this.Broadcaster,
        request.Difficulty,
        enemies,
        rounds,
        stage,
        towers
      );
    }

    /// <inheritdoc/>
    public void StartRound(LocalRequest request)
    {
      if (this.LobbyMode != Mode.InputMode)
      {
        return;
      }

      this.LobbyMode = Mode.FightMode;
      this.Broadcaster.FightRound(this.RoundNumber);

      this.EcsContainer.PlaceEnemySystem.PlaceEnemies();
      this.EcsContainer.ProcessFightRound();
      if (this.EcsContainer.HasPlayerDied)
      {
        this.Broadcaster.EndGame(false, 0, 0);
      }
      else if (this.RoundNumber == this.NumberOfRounds)
      {
        this.Broadcaster.EndGame(true, 0, 0);
      }
      else
      {
        this.RoundNumber++;

        this.LobbyMode = Mode.InputMode;
        this.Broadcaster.InputRound(this.RoundNumber);
        return;
      }

      this.LobbyMode = Mode.GameOver;
      this.Dispose();
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
      this.Dispose();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
      if (this.Disposed)
      {
        return;
      }

      foreach (IObserver observer in this.Observers)
      {
        observer.OnClose(this.Id);
      }

      this.Broadcaster.Dispose();
      this.EcsContainer.Dispose();
    }

    private enum Mode
    {
      /// <summary>
      /// During fight mode, a game is in progress and there are enemies on the stage.
      /// </summary>
      FightMode,

      /// <summary>
      /// Game is over.
      /// </summary>
      GameOver,

      /// <summary>
      /// During input mode, a game is in progress and there are no enemies on the stage. Input is expected.
      /// </summary>
      InputMode,

      /// <summary>
      /// During lobby mode, the game has not yet started.
      /// </summary>
      LobbyMode,
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
