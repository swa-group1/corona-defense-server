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
  internal class PlaceTowerSystem : IEcsSystem
  {
    private readonly EcsFilter<GameComponent> gameFilter = null;
    private readonly EcsFilter<PlayerComponent> playerFilter = null;
    private readonly EcsFilter<BoardPositionComponent> towerFilter = null;
    private readonly EcsWorld world = null;

    /// <summary>
    /// Process supplied <see cref="PlaceTowerRequest"/>.
    /// </summary>
    /// <param name="request">Request to process.</param>
    public void PlaceTower(PlaceTowerRequest request)
    {
      ref GameComponent game = ref this.gameFilter.Get1(0);
      ref PlayerComponent player = ref this.playerFilter.Get1(0);

      // Check if tile is valid in stage, eg inside stage and not on blocked tile .
      Stage.Tile requestTile = new Stage.Tile() { X = request.XPosition, Y = request.YPosition };
      if (!game.Stage.IsValidTowerTile(requestTile))
      {
        return;
      }

      // Check if tower already exists in same tile.
      foreach (int i in this.towerFilter)
      {
        if (this.towerFilter.Get1(i).Position == requestTile)
        {
          return;
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

      // Reduce balance
      player.Balance -= 50 * request.TowerTypeNumber;

      // Broadcast changes
      game.Broadcaster.TowerPosition((short)tower.GetInternalId(), (byte)request.TowerTypeNumber, (byte)request.XPosition, (byte)request.YPosition);
      game.Broadcaster.MoneyUpdate(player.Balance);
    }
  }
}
