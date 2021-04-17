// <copyright file="PathMoveSystem.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Game.Components;
using Leopotam.Ecs;

namespace BackEnd.Game.Systems
{
  /// <summary>
  /// System to move entities with speed along the path.
  /// </summary>
  internal class PathMoveSystem : IEcsRunSystem
  {
    private readonly EcsFilter<GameComponent> game = null;
    private readonly EcsFilter<PathPositionComponent, PathSpeedComponent> movers = null;

    /// <inheritdoc/>
    public void Run()
    {
      ref GameComponent game = ref this.game.Get1(0);

      foreach (int moverIndex in this.movers)
      {
        ref PathPositionComponent pathPositionComponent = ref this.movers.Get1(moverIndex);
        ref PathSpeedComponent pathSpeedComponent = ref this.movers.Get2(moverIndex);

        pathPositionComponent.LengthTraveled += game.TickDuration * pathSpeedComponent.Speed;
      }
    }
  }
}
