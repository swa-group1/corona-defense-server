// <copyright file="PlaceTowerSystem.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Communication.API.Requests;
using BackEnd.Game.Components;
using Leopotam.Ecs;
using System.Collections.Generic;

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
    private readonly Dictionary<int, TowerDefinitions.Tower> towers;
    private readonly EcsWorld world = null;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlaceTowerSystem"/> class.
    /// </summary>
    /// <param name="towerDefinitions">List of available tower definitions.</param>
    public PlaceTowerSystem(TowerDefinitions towerDefinitions)
    {
      this.towers = new Dictionary<int, TowerDefinitions.Tower>();
      foreach (TowerDefinitions.Tower tower in towerDefinitions.Towers)
      {
        this.towers.Add(tower.TypeNumber, tower);
      }
    }

    /// <summary>
    /// Process supplied <see cref="PlaceTowerRequest"/>.
    /// </summary>
    /// <param name="request">Request to process.</param>
    public void PlaceTower(PlaceTowerRequest request)
    {
      if (!this.towers.TryGetValue(request.TowerTypeNumber, out TowerDefinitions.Tower towerDefinition))
      {
        return;
      }

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

      int actualPrice = (int)(towerDefinition.MediumCost * game.TowerCostFactor);

      // Credit check
      if (player.Balance < actualPrice)
      {
        return;
      }

      // Create tower
      EcsEntity tower = this.world.NewEntity();
      ref TowerComponent towerComponent = ref tower.Get<TowerComponent>();
      towerComponent.MediumCost = towerDefinition.MediumCost;
      towerComponent.ProjectileSpeed = towerDefinition.ProjectileSpeed;
      towerComponent.ProjectileSpriteNumber = towerDefinition.ProjectileSpriteNumber;
      towerComponent.Range = towerDefinition.Range;
      towerComponent.ReloadTime = towerDefinition.ReloadTime;
      towerComponent.TowerSpriteNumber = towerDefinition.TowerSpriteNumber;
      towerComponent.TowerType = towerDefinition.TypeNumber;
      ref BoardPositionComponent towerPosition = ref tower.Get<BoardPositionComponent>();
      towerPosition.Position = requestTile;

      // Reduce balance
      player.Balance -= actualPrice;

      // Broadcast changes
      game.Broadcaster.TowerPosition((short)tower.GetInternalId(), (byte)request.TowerTypeNumber, (byte)request.XPosition, (byte)request.YPosition);
      game.Broadcaster.MoneyUpdate(player.Balance);
    }
  }
}
