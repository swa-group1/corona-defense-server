// <copyright file="PlaceTowerSystem.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using API.Requests;
using BackEnd.Game.Components;
using Leopotam.Ecs;
using System.Collections.Concurrent;

namespace BackEnd.Game.Systems
{
  /// <summary>
  /// 
  /// </summary>
  internal class PlaceTowerSystem : IEcsRunSystem
  {
    private readonly EcsFilter<GameComponent> gameFilter = null;
    private readonly ConcurrentQueue<PlaceTowerRequest> inputQueue;
    private readonly EcsWorld world = null;

    public PlaceTowerSystem(ConcurrentQueue<PlaceTowerRequest> inputQueue)
    {
      this.inputQueue = inputQueue;
    }

    /// <inheritdoc/>
    public void Run()
    {
      GameComponent game = this.gameFilter.Get1(0);
      while (this.inputQueue.TryDequeue(out PlaceTowerRequest request))
      {
        EcsEntity tower = this.world.NewEntity();
        ref TowerComponent towerComponent = ref tower.Get<TowerComponent>();
        towerComponent.ProjectileSpeed = 1d;
        towerComponent.ProjectileSpriteNumber = 1;
        towerComponent.ReloadTime = 1d;
        towerComponent.TowerSpriteNumber = 1;
        ref BoardPositionComponent towerPosition = ref tower.Get<BoardPositionComponent>();
        towerPosition.X = request.XPosition;
        towerPosition.Y = request.YPosition;

        game.Broadcaster.TowerPosition((short)tower.GetInternalId(), (byte)request.TowerTypeNumber, (byte)request.XPosition, (byte)request.YPosition);
      }

    }
  }
}
