// <copyright file="ReloadSystem.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Game.Components;
using Leopotam.Ecs;

namespace BackEnd.Game.Systems
{
  /// <summary>
  /// System to reload towers.
  /// </summary>
  internal class ReloadSystem : IEcsRunSystem
  {
    private readonly EcsFilter<GameComponent> game = null;
    private readonly EcsFilter<TowerComponent>.Exclude<ReloadedTag> reloadingTowers = null;

    /// <inheritdoc/>
    public void Run()
    {
      ref GameComponent gameComponent = ref this.game.Get1(0);

      foreach (int i in this.reloadingTowers)
      {
        ref EcsEntity entity = ref this.reloadingTowers.GetEntity(i);
        ref TowerComponent tower = ref this.reloadingTowers.Get1(i);

        tower.TimeUntilReloaded -= gameComponent.TickDuration;
        if (tower.TimeUntilReloaded <= 0)
        {
          // Add ReloadedTag
          _ = entity.Get<ReloadedTag>();
        }
      }
    }
  }
}
