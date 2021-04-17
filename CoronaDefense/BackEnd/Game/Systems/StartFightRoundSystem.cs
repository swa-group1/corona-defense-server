// <copyright file="StartFightRoundSystem.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using API.Requests;
using BackEnd.Game.Components;
using Leopotam.Ecs;
using System.Collections.Concurrent;

namespace BackEnd.Game.Systems
{
  /// <summary>
  /// System to start rounds.
  /// </summary>
  internal class StartFightRoundSystem : IEcsRunSystem
  {
    private readonly EcsFilter enemyFilter = null;
    private readonly EcsFilter<GameComponent> gameFilter = null;
    private readonly ConcurrentQueue<LocalRequest> inputQueue;
    private readonly EcsWorld world = null;

    /// <summary>
    /// Initializes a new instance of the <see cref="StartFightRoundSystem"/> class.
    /// </summary>
    /// <param name="inputQueue">Queue to take input requests from.</param>
    public StartFightRoundSystem(ConcurrentQueue<LocalRequest> inputQueue)
    {
      this.inputQueue = inputQueue;
    }

    /// <inheritdoc/>
    public void Run()
    {
      if (0 < this.enemyFilter.GetEntitiesCount())
      {
        return;
      }

      // Look for request to start new round
      if (!this.inputQueue.TryDequeue(out _))
      {
        return;
      }

      // Reset time
      GameComponent game = this.gameFilter.Get1(0);
      game.Time = 0d;

      // Send start round message
      game.Broadcaster.FightRound((short)game.RoundNumber);

      // Add enemies
      double entryTime = 1d;
      for (int i = 1; i <= 10; i++)
      {
        entryTime += 0.5d;

        EcsEntity enemyEntity = this.world.NewEntity();
        ref PathSpeedComponent enemySpeed = ref enemyEntity.Get<PathSpeedComponent>();
        enemySpeed.Speed = 1d;
        ref PathPositionComponent enemyPosition = ref enemyEntity.Get<PathPositionComponent>();
        enemyPosition.LengthTraveled = -enemySpeed.Speed * entryTime;
        ref EnemyComponent enemy = ref enemyEntity.Get<EnemyComponent>();
        enemy.PreviousImpactPosition = 0d;
        enemy.PreviousImpactTime = entryTime;
      }

      // Remove duplicate requests
      this.inputQueue.Clear();
    }
  }
}
