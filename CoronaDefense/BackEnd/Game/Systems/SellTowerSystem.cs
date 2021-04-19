// <copyright file="SellTowerSystem.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Communication.API.Requests;
using BackEnd.Game.Components;
using Leopotam.Ecs;

namespace BackEnd.Game.Systems
{
  /// <summary>
  /// System to sell towers in game on request.
  /// </summary>
  internal class SellTowerSystem : IEcsSystem
  {
    private readonly EcsFilter<GameComponent> gameFilter;
    private readonly EcsFilter<PlayerComponent> playerFilter;
    private readonly EcsFilter<TowerComponent> towerFilter;

    /// <summary>
    /// Process supplied <see cref="SelltowerRequest"/>.
    /// </summary>
    /// <param name="request">Request to process.</param>
    public void SellTower(SelltowerRequest request)
    {
      foreach (int i in this.towerFilter)
      {
        ref EcsEntity towerEntity = ref this.towerFilter.GetEntity(i);
        if (request.TowerId != (short)towerEntity.GetInternalId())
        {
          continue;
        }

        // Perform changes
        ref GameComponent game = ref this.gameFilter.Get1(0);
        ref PlayerComponent player = ref this.playerFilter.Get1(0);
        ref TowerComponent tower = ref this.towerFilter.Get1(i);
        player.Balance += (int)(tower.MediumCost * game.TowerCostFactor * game.TowerSaleFactor);
        towerEntity.Destroy();

        // Broadcast changes
        game.Broadcaster.TowerRemoved((short)request.TowerId);
        game.Broadcaster.MoneyUpdate(player.Balance);

        // Discontinue search for tower
        return;
      }
    }
  }
}
