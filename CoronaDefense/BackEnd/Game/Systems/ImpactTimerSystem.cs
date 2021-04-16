// <copyright file="ImpactTimerSystem.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Game.Components;
using Leopotam.Ecs;

namespace BackEnd.Game.Systems
{
  /// <summary>
  /// 
  /// </summary>
  internal class ImpactTimerSystem : IEcsRunSystem
  {
    private readonly EcsFilter<GameComponent> gameFilter = null;
    private readonly EcsFilter<HealthComponent, ImpactTimerComponent> doomedFilter = null;

    /// <inheritdoc/>
    public void Run()
    {
      ref GameComponent game = ref this.gameFilter.Get1(0);
      
      foreach (int doomedIndex in this.doomedFilter)
      {
        // Reduce all timers
        ref ImpactTimerComponent timers = ref doomedFilter.Get2(doomedIndex);

        for (int timerIndex = 0; timerIndex < timers.ImpactTimers.Count; ++timerIndex)
        {
          timers.ImpactTimers[timerIndex] -= game.TickDuration;
          if (0 < timers.ImpactTimers[timerIndex])
          {
            continue;
          }

          ref HealthComponent health = ref doomedFilter.Get1(doomedIndex);
          health.HealthPoints -= 1;
          
          if (health.HealthPoints <= 0) {
            // TODO DIE
          }

          // TODO ANIMATION
        }
      }
    }
  }
}
