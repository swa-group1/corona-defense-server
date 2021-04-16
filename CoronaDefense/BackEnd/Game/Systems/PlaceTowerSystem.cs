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
  /// System to place towers in game on request.
  /// </summary>
  internal class PlaceTowerSystem : IEcsRunSystem
  {
    private readonly EcsFilter<GameComponent> gameFilter = null;
    private readonly ConcurrentQueue<PlaceTowerRequest> inputQueue;
    private readonly EcsWorld world = null;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlaceTowerSystem"/> class.
    /// </summary>
    /// <param name="inputQueue">Queue to take input requests from.</param>
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
        // Validate request
        if (request.XPosition < 0 || game.Stage.XSize <= request.XPosition)
        {
          return;
        }

        if (request.YPosition < 0 || game.Stage.YSize <= request.YPosition)
        {
          return;
        }

        // Create tower
        EcsEntity tower = this.world.NewEntity();
        ref TowerComponent towerComponent = ref tower.Get<TowerComponent>();
        towerComponent.ProjectileSpeed = 1d;
        towerComponent.ProjectileSpriteNumber = 1;
        towerComponent.ReloadTime = 1d;
        towerComponent.TowerSpriteNumber = 1;
        ref BoardPositionComponent towerPosition = ref tower.Get<BoardPositionComponent>();
        towerPosition.Position = new Stage.Tile() { X = request.XPosition, Y = request.YPosition };

        // Broadcast changes
        game.Broadcaster.TowerPosition((short)tower.GetInternalId(), (byte)request.TowerTypeNumber, (byte)request.XPosition, (byte)request.YPosition);
      }
    }
  }
}
