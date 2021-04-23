// <copyright file="HurtPlayerSystem.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Game.Components;
using Leopotam.Ecs;
using System;

namespace BackEnd.Game.Systems
{
  /// <summary>
  /// System to hurt player when enemies exit the stage.
  /// </summary>
  internal class HurtPlayerSystem : IEcsRunSystem
  {
    private readonly EcsFilter<EnemyComponent, ImpactComponent, PathPositionComponent> enemies = null;
    private readonly EcsFilter<GameComponent> game = null;
    private readonly EcsFilter<PlayerComponent> playerFilter = null;

    /// <inheritdoc/>
    public void Run()
    {
      ref GameComponent game = ref this.game.Get1(0);

      foreach (int enemyIndex in this.enemies)
      {
        ref PathPositionComponent pathPositionComponent = ref this.enemies.Get3(enemyIndex);

        if (!game.Stage.IsPastStage(pathPositionComponent.LengthTraveled))
        {
          continue;
        }

        // Hurt player
        ref EnemyComponent enemyComponent = ref this.enemies.Get1(enemyIndex);
        ref PlayerComponent player = ref this.playerFilter.Get1(0);
        player.Health = Math.Max(0, player.Health - enemyComponent.PlayerDamage);
        game.Broadcaster.HealthAnimation((short)player.Health, (float)game.Time);

        // Send animation
        ref ImpactComponent impactComponent = ref this.enemies.Get2(enemyIndex);
        game.Broadcaster.PathToPathAnimation(
            (byte)enemyComponent.SpriteNumber,
            (float)impactComponent.PreviousImpactPosition,
            (float)pathPositionComponent.LengthTraveled,
            (float)impactComponent.PreviousImpactTime,
            (float)game.Time,
            0x00
        );

        // Remove enemy
        this.enemies.GetEntity(enemyIndex).Destroy();
      }
    }
  }
}
