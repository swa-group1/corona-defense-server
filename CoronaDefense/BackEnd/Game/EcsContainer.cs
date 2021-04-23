// <copyright file="EcsContainer.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Communication;
using BackEnd.Communication.API.Requests;
using BackEnd.Game.Components;
using BackEnd.Game.Systems;
using Leopotam.Ecs;
using System;
using System.Collections.Generic;

namespace BackEnd.Game
{
  /// <summary>
  /// Container for a ECS world.
  /// </summary>
  internal class EcsContainer : IDisposable
  {
    private const int TickNumber = 20;

    private readonly EcsWorld world;
    private readonly EcsSystems systems;

    /// <summary>
    /// Gets or sets a value indicating whether the player has died.
    /// </summary>
    public bool HasPlayerDied { get; set; }

    /// <summary>
    /// Sets a value indicating whether this <see cref="EcsContainer"/> should continue processing a fight round.
    /// </summary>
    public bool Running { private get; set; }

    /// <summary>
    /// Gets system used to spawn enemies at the start of each round.
    /// </summary>
    public PlaceEnemySystem PlaceEnemySystem { get; }

    /// <summary>
    /// Gets system used to place towers on the board.
    /// </summary>
    public PlaceTowerSystem PlaceTowerSystem { get; }

    /// <summary>
    /// Gets or sets final score set when the game is over.
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// Gets system used to sell towers.
    /// </summary>
    public SellTowerSystem SellTowerSystem { get; }

    /// <summary>
    /// Gets system used to update new clients on the game state.
    /// </summary>
    public UpdateClientSystem UpdateClientSystem { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EcsContainer"/> class.
    /// </summary>
    /// <param name="broadcaster"><see cref="Broadcaster"/> to send game messages to.</param>
    /// <param name="difficulty">Difficulty of this game instance.</param>
    /// <param name="enemies">Enemy definitions to utilize.</param>
    /// <param name="rounds">Round definitions to utilize.</param>
    /// <param name="stage"><see cref="Stage"/> to play on.</param>
    /// <param name="towers">Tower definitions to utilize.</param>
    public EcsContainer(
      Broadcaster broadcaster,
      StartGameRequest.Difficulties difficulty,
      EnemyDefinitions enemies,
      RoundDefinitions rounds,
      Stage stage,
      TowerDefinitions towers
    )
    {
      this.world = new EcsWorld();
      this.systems = new EcsSystems(this.world);

      Dictionary<string, EnemyType> enemyTypeMap = new Dictionary<string, EnemyType>();
      foreach (EnemyType enemyType in enemies.EnemyTypes)
      {
        enemyTypeMap.Add(enemyType.Name, enemyType);
      }

      GameComponent gameSettings = new GameComponent()
      {
        Broadcaster = broadcaster,
        EnemyTypeMap = enemyTypeMap,
        Stage = stage,
        TickDuration = 1d / TickNumber,
        Time = 0d,
      };
      switch (difficulty)
      {
        case StartGameRequest.Difficulties.EASY:
          gameSettings.TowerCostFactor = 0.8d;
          gameSettings.TowerSaleFactor = 0.9d;
          break;
        case StartGameRequest.Difficulties.MEDIUM:
          gameSettings.TowerCostFactor = 1.0d;
          gameSettings.TowerSaleFactor = 0.8d;
          break;
        case StartGameRequest.Difficulties.HARD:
          gameSettings.TowerCostFactor = 1.2d;
          gameSettings.TowerSaleFactor = 0.6d;
          break;
      }

      // Init
      _ = this.systems.Add(new GameInitializeSystem(gameSettings));
      _ = this.systems.Add(new PlayerInitializeSystem());

      // Input
      this.PlaceEnemySystem = new PlaceEnemySystem(rounds);
      _ = this.systems.Add(this.PlaceEnemySystem);
      this.PlaceTowerSystem = new PlaceTowerSystem(towers);
      _ = this.systems.Add(this.PlaceTowerSystem);
      this.SellTowerSystem = new SellTowerSystem();
      _ = this.systems.Add(this.SellTowerSystem);
      this.UpdateClientSystem = new UpdateClientSystem();
      _ = this.systems.Add(this.UpdateClientSystem);

      // Pre-frame
      _ = this.systems.Add(new TimeSystem());

      // Frame
      _ = this.systems.Add(new ShootingSystem());
      _ = this.systems.Add(new ImpactTimerSystem());
      _ = this.systems.Add(new ReloadSystem());
      _ = this.systems.Add(new PathMoveSystem());
      _ = this.systems.Add(new HurtPlayerSystem());

      // Post-frame
      _ = this.systems.Add(new EndRoundSystem(this));

      // Debug

      _ = this.systems.ProcessInjects();
      this.systems.Init();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
      this.world.Destroy();
    }

    /// <summary>
    /// Process until all enemies are gone. It is assumed that towers and enemies are already in place.
    /// </summary>
    public void ProcessFightRound()
    {
      this.Running = true;
      while (this.Running)
      {
        this.systems.Run();
      }
    }
  }
}
