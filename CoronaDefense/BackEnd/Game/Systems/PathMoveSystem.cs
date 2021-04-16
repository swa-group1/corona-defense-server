// <copyright file="PathMoveSystem.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Game.Components;
using Leopotam.Ecs;

namespace BackEnd.Game.Systems
{
  /// <summary>
  /// System to reload towers.
  /// </summary>
  internal class PathMoveSystem : IEcsRunSystem
  {
    private readonly EcsFilter<GameComponent> game = null;
    private readonly EcsFilter<PathPositionComponent, PathSpeedComponent> movers = null;

    /// <inheritdoc/>
    public void Run()
    {
      ref GameComponent gameComponent = ref game.Get1(0);

      foreach (int i in this.movers)
      {
        ref PathPositionComponent pathPositionComponent = ref this.movers.Get1(i);
        ref PathSpeedComponent PathSpeedComponent = ref this.movers.Get2(i);

        pathPositionComponent.LengthTraveled += gameComponent.TickDuration*PathSpeedComponent.Speed;
      }
    }
  }
}
