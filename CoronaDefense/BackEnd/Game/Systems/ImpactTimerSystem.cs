// <copyright file="ImpactTimerSystem.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Game.Components;
using Leopotam.Ecs;
using System;

namespace BackEnd.Game.Systems
{
  /// <summary>
  /// System that processes impact timers and impact.
  /// </summary>
  internal class ImpactTimerSystem : IEcsRunSystem
  {
    private readonly EcsFilter<GameComponent> gameFilter = null;
    private readonly EcsFilter<EnemyComponent, ImpactComponent, ImpactTimerComponent, PathPositionComponent, PathSpeedComponent> doomedFilter = null;
    private readonly EcsFilter<PlayerComponent> playerFilter = null;

    /// <inheritdoc/>
    public void Run()
    {
      ref GameComponent game = ref this.gameFilter.Get1(0);

      foreach (int doomedIndex in this.doomedFilter)
      {
        // Reduce all timers
        ref ImpactTimerComponent timers = ref this.doomedFilter.Get3(doomedIndex);
        bool destroyed = false;

        for (int timerIndex = 0; timerIndex < timers.ImpactTimers.Count; ++timerIndex)
        {
          timers.ImpactTimers[timerIndex] -= game.TickDuration;
          if (timers.ImpactTimers[timerIndex] > 0)
          {
            continue;
          }

          // Send enemy animaion
          ref EnemyComponent enemy = ref this.doomedFilter.Get1(doomedIndex);
          ref ImpactComponent impactComponent = ref this.doomedFilter.Get2(doomedIndex);
          ref PathPositionComponent doomedPosition = ref this.doomedFilter.Get4(doomedIndex);
          game.Broadcaster.PathToPathAnimation(
            (byte)enemy.SpriteNumber,
            (float)impactComponent.PreviousImpactPosition,
            (float)doomedPosition.LengthTraveled,
            (float)impactComponent.PreviousImpactTime,
            (float)game.Time,
            0x00
          );

          // Update impact information
          impactComponent.PreviousImpactPosition = doomedPosition.LengthTraveled;
          impactComponent.PreviousImpactTime = game.Time;

          // Money
          ref PlayerComponent player = ref this.playerFilter.Get1(0);
          player.Balance += 1;
          player.PopCount += 1;
          game.Broadcaster.MoneyAnimation(
            player.Balance,
            (float)game.Time
          );

          // Remove timer
          timers.ImpactTimers.RemoveAt(timerIndex);
          --timerIndex;

          // Update enemy type
          if (game.EnemyTypeMap.TryGetValue(enemy.NextType, out EnemyType enemyType))
          {
            // Convert to next type
            enemy.NextType = enemyType.NextType;
            enemy.PlayerDamage = enemyType.Health;
            enemy.SpriteNumber = enemyType.SpriteNumber;
            this.doomedFilter.Get5(doomedIndex).Speed = enemyType.Speed;
          }
          else
          {
            // Enemy has no health left
            destroyed = true;
            this.doomedFilter.GetEntity(doomedIndex).Destroy();
            break;
          }
        }

        // Remove timer component if entity still exists and no timers are left
        if (!destroyed && timers.ImpactTimers.Count == 0)
        {
          this.doomedFilter.GetEntity(doomedIndex).Del<ImpactTimerComponent>();
        }
      }
    }
  }
}
