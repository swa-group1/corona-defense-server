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
    private readonly EcsFilter<BoardPositionComponent> towerFilter = null;
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
        // Check if tile is valid in stage, eg inside stage and not on blocked tile .
        Stage.Tile requestTile = new Stage.Tile() { X = request.XPosition, Y = request.YPosition };
        if (!game.Stage.IsValidTowerTile(requestTile))
        {
          continue;
        }

        // Check if tower already exists in same tile.
        foreach (int i in this.towerFilter)
        {
          if (this.towerFilter.Get1(i).Position == requestTile)
          {
            continue;
          }
        }

        // Create tower
        EcsEntity tower = this.world.NewEntity();
        ref TowerComponent towerComponent = ref tower.Get<TowerComponent>();
        towerComponent.ProjectileSpeed = 1d;
        towerComponent.ProjectileSpriteNumber = 1;
        towerComponent.ReloadTime = 1d;
        towerComponent.TowerSpriteNumber = 1;
        ref BoardPositionComponent towerPosition = ref tower.Get<BoardPositionComponent>();
        towerPosition.Position = requestTile;

        // Broadcast changes
        game.Broadcaster.TowerPosition((short)tower.GetInternalId(), (byte)request.TowerTypeNumber, (byte)request.XPosition, (byte)request.YPosition);
      }
    }
  }
}
