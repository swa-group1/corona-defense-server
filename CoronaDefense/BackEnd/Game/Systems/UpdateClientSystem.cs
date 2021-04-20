// <copyright file="UpdateClientSystem.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Game.Components;
using Leopotam.Ecs;

namespace BackEnd.Game.Systems
{
  /// <summary>
  /// System that updates new clients on the game state.
  /// </summary>
  internal class UpdateClientSystem : IEcsSystem
  {
    private readonly EcsFilter<GameComponent> gameFilter = null;
    private readonly EcsFilter<PlayerComponent> playerFilter = null;
    private readonly EcsFilter<BoardPositionComponent, TowerComponent> towerFilter = null;

    /// <summary>
    /// Broadcast all messages necessary to describe the full game state.
    /// </summary>
    public void UpdateClients()
    {
      ref readonly GameComponent game = ref this.gameFilter.Get1(0);
      ref readonly PlayerComponent player = ref this.playerFilter.Get1(0);

      game.Broadcaster.HealthUpdate((short)player.Health);
      game.Broadcaster.MoneyUpdate((short)player.Balance);
      foreach (int i in this.towerFilter)
      {
        ref readonly EcsEntity towerEntity = ref this.towerFilter.GetEntity(i);
        ref readonly BoardPositionComponent towerPosition = ref this.towerFilter.Get1(i);
        ref readonly TowerComponent tower = ref this.towerFilter.Get2(i);
        game.Broadcaster.TowerPosition((short)towerEntity.GetInternalId(), (byte)tower.TowerType, (byte)towerPosition.Position.X, (byte)towerPosition.Position.Y);
      }
    }
  }
}
