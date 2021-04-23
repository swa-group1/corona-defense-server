// <copyright file="Lobby.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Communication;
using BackEnd.Communication.API.Requests;
using BackEnd.Communication.API.Schemas;
using BackEnd.Orchestrator;
using BackEnd.Router;
using System;
using System.Collections.Generic;
using System.Timers;

namespace BackEnd.Game
{
  /// <summary>
  /// Model of a game-instance. Contains all the state for one running instance of the game.
  /// </summary>
  /// <remarks>
  /// This object is part of the Game Layer – Layer 3.
  /// </remarks>
  internal class Lobby : IDisposable, IReceiver
  {
    /// <summary>
    /// Default time before a <see cref="Lobby"/> should close itself because of inactivity.
    /// </summary>
    private const int LobbyTimeOut = 5 * 60 * 1000;

    /// <summary>
    /// Gets the set of access tokens of clients currently connected to this <see cref="Lobby"/>.
    /// </summary>
    private HashSet<long> AccessTokens { get; } = new HashSet<long>();

    private Broadcaster Broadcaster { get; }

    /// <summary>
    /// Gets or sets a value indicating whether there are currently clients that are yet to receive the full game state because they joined during a fight round.
    /// </summary>
    private bool DelayedUpdate { get; set; } = false;

    private bool Disposed { get; set; } = false;

    private StartGameRequest.Difficulties Difficulty { get; set; }

    private EcsContainer EcsContainer { get; set; }

    /// <summary>
    /// Gets the ID of this <see cref="Lobby"/>.
    /// </summary>
    public long Id { get; }

    /// <summary>
    /// Gets or sets timer used to time out lobbies after inactivity.
    /// </summary>
    private Timer InactivityTimer { get; set; }

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
    /// Gets the number of players, or clients, currently connected to this <see cref="Lobby"/>.
    /// </summary>
    public int PlayerCount { get; private set; } = 0;

    private short RoundNumber { get; set; } = 1;

    /// <summary>
    /// Gets router that this <see cref="Lobby"/> is attached to.
    /// </summary>
    private Router.Router Router { get; }

    /// <summary>
    /// Gets padlock used to limit the processing of requests in a lobby to one at a time.
    /// </summary>
    private object Padlock { get; } = new object();

    /// <summary>
    /// Initializes a new instance of the <see cref="Lobby"/> class.
    /// </summary>
    /// <param name="name">Name of the new <see cref="Lobby"/>.</param>
    /// <param name="password">Password of the new <see cref="Lobby"/>.</param>
    /// <param name="connectionBroker"><see cref="ConnectionBroker"/> that this <see cref="Lobby"/> should get open connections from.</param>
    /// <param name="router">Router the new <see cref="Lobby"/> should connect to.</param>
    public Lobby(string name, string password, ConnectionBroker connectionBroker, Router.Router router)
    {
      this.Broadcaster = new Broadcaster(connectionBroker);
      this.Id = router.Register(this);
      this.Name = name;
      this.Password = password;
      this.Router = router;

      this.ResetInactivityTimer();
    }

    /// <inheritdoc/>
    public JoinLobbyResult JoinLobby(JoinLobbyRequest request)
    {
      try
      {
        lock (this.Padlock)
        {
          if (this.LobbyMode == Mode.GameOver)
          {
            return new JoinLobbyResult()
            {
              Success = false,
              Details = "Lobby has been closed.",
            };
          }

          if (request.Password != this.Password)
          {
            return new JoinLobbyResult()
            {
              Success = false,
              Details = "Password was not correct.",

              LobbyId = this.Id,
            };
          }

          // Find access token
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

          if (this.LobbyMode == Mode.InputMode)
          {
            this.EcsContainer.UpdateClientSystem.UpdateClients();
          }
          else if (this.LobbyMode == Mode.FightMode)
          {
            this.DelayedUpdate = true;
          }

          return new JoinLobbyResult()
          {
            AccessToken = accessToken,
            LobbyId = this.Id,
          };
        }
      }
      finally
      {
        this.ResetInactivityTimer();
      }
    }

    /// <inheritdoc/>
    public void LeaveLobby(LocalRequest request)
    {
      try
      {
        lock (this.Padlock)
        {
          if (this.LobbyMode == Mode.GameOver)
          {
            return;
          }

          if (this.AccessTokens.Remove(request.AccessToken))
          {
            this.Broadcaster.DisconnectClient(request.AccessToken);
            this.PlayerCount--;
          }
        }
      }
      finally
      {
        this.ResetInactivityTimer();
      }
    }

    /// <inheritdoc/>
    public void PlaceTower(PlaceTowerRequest request)
    {
      try
      {
        lock (this.Padlock)
        {
          if (this.LobbyMode != Mode.InputMode)
          {
            return;
          }

          this.EcsContainer.PlaceTowerSystem.PlaceTower(request);
        }
      }
      finally
      {
        this.ResetInactivityTimer();
      }
    }

    /// <inheritdoc/>
    public void SellTower(SelltowerRequest request)
    {
      try
      {
        lock (this.Padlock)
        {
          if (this.LobbyMode != Mode.InputMode)
          {
            return;
          }

          this.EcsContainer.SellTowerSystem.SellTower(request);
        }
      }
      finally
      {
        this.ResetInactivityTimer();
      }
    }

    private void ResetInactivityTimer()
    {
      this.StopInactivityTimer();
      Timer timer = new Timer()
      {
        AutoReset = false,
        Interval = LobbyTimeOut,
      };
      timer.Elapsed += this.OnNoPlayerTimerElapsed;
      timer.Start();
      this.InactivityTimer = timer;
    }

    /// <inheritdoc/>
    public void StartGame(StartGameRequest request)
    {
      try
      {
        lock (this.Padlock)
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
          this.EcsContainer.UpdateClientSystem.UpdateClients();
        }
      }
      finally
      {
        this.ResetInactivityTimer();
      }
    }

    /// <inheritdoc/>
    public void StartRound(LocalRequest request)
    {
      lock (this.Padlock)
      {
        this.ResetInactivityTimer();

        if (this.LobbyMode != Mode.InputMode)
        {
          return;
        }

        // Inform lobby and clients about the starting fight round
        this.LobbyMode = Mode.FightMode;
        this.Broadcaster.FightRound(this.RoundNumber);

        // Place enemies and process the fight round
        this.EcsContainer.PlaceEnemySystem.PlaceEnemies();
        this.EcsContainer.ProcessFightRound();

        // Process aftermath
        bool didWin;
        if (this.EcsContainer.HasPlayerDied)
        {
          // Player lost all health points
          didWin = false;

          // Default to end game logic below
        }
        else if (this.RoundNumber == this.NumberOfRounds)
        {
          // Player survived all rounds
          didWin = true;

          // Default to end game logic below
        }
        else
        {
          // Player is not dead nor is all rounds done
          this.RoundNumber++;

          this.LobbyMode = Mode.InputMode;
          this.Broadcaster.InputRound(this.RoundNumber);

          // Update clients who joined during the round on game state.
          if (this.DelayedUpdate)
          {
            this.EcsContainer.UpdateClientSystem.UpdateClients();
            this.DelayedUpdate = false;
          }

          // Skip end game logic below
          return;
        }

        int placement;
        if (this.Difficulty == StartGameRequest.Difficulties.HARD)
        {
          placement = HighscoreListManager.Instance.RegisterScore(this.Name, this.EcsContainer.Score);
        }
        else
        {
          placement = -1;
        }

        this.Broadcaster.EndGame(didWin, placement, this.EcsContainer.Score);

        this.LobbyMode = Mode.GameOver;
        this.Dispose();
      }
    }

    private void StopInactivityTimer()
    {
      this.InactivityTimer?.Stop();
      this.InactivityTimer = null;
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
    /// This <see cref="Lobby"/> should close itself when inactivity timer runs out.
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

      this.Disposed = true;

      foreach (IObserver observer in this.Observers)
      {
        observer.OnClose(this.Id);
      }

      this.Router.Remove(this.Id);

      this.AccessTokens.Clear();
      this.Broadcaster.Dispose();
      this.PlayerCount = 0;

      this.EcsContainer?.Dispose();
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
