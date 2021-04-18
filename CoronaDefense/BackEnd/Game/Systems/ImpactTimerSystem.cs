// <copyright file="ImpactTimerSystem.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Game.Components;
using Leopotam.Ecs;

namespace BackEnd.Game.Systems
{
  /// <summary>
  /// System that processes impact timers and impact.
  /// </summary>
  internal class ImpactTimerSystem : IEcsRunSystem
  {
    private readonly EcsFilter<GameComponent> gameFilter = null;
    private readonly EcsFilter<EnemyComponent, HealthComponent, ImpactTimerComponent, PathPositionComponent> doomedFilter = null;
    private readonly EcsFilter<PlayerComponent> playerFilter = null;

    /// <inheritdoc/>
    public void Run()
    {
      ref GameComponent game = ref this.gameFilter.Get1(0);

      foreach (int doomedIndex in this.doomedFilter)
      {
        // Reduce all timers
        ref ImpactTimerComponent timers = ref this.doomedFilter.Get3(doomedIndex);

        for (int timerIndex = 0; timerIndex < timers.ImpactTimers.Count; ++timerIndex)
        {
          timers.ImpactTimers[timerIndex] -= game.TickDuration;
          if (timers.ImpactTimers[timerIndex] > 0)
          {
            continue;
          }

          // Remove timer
          timers.ImpactTimers.RemoveAt(timerIndex);
          --timerIndex;

          // Reduce health
          ref HealthComponent health = ref this.doomedFilter.Get2(doomedIndex);
          health.HealthPoints -= 1;

          ref EnemyComponent enemy = ref this.doomedFilter.Get1(doomedIndex);
          ref PathPositionComponent doomedPosition = ref this.doomedFilter.Get4(doomedIndex);
          game.Broadcaster.PathToPathAnimation(
            0x01,
            (float)enemy.PreviousImpactPosition,
            (float)doomedPosition.LengthTraveled,
            (float)enemy.PreviousImpactTime,
            (float)game.Time,
            0x00
          );

          // Money
          ref PlayerComponent player = ref this.playerFilter.Get1(0);
          player.Balance += 1;
          game.Broadcaster.MoneyAnimation(
            player.Balance,
            (float)game.Time
          );

          // Remove doomed when no health points left
          if (health.HealthPoints <= 0)
          {
            this.doomedFilter.GetEntity(doomedIndex).Destroy();
          }
        }
      }
    }
  }
}
