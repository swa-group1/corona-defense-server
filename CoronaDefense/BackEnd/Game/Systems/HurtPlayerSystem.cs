// <copyright file="HurtPlayerSystem.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Game.Components;
using Leopotam.Ecs;

namespace BackEnd.Game.Systems
{
  /// <summary>
  /// System to hurt player when enemies exit the stage.
  /// </summary>
  internal class HurtPlayerSystem : IEcsRunSystem
  {
    private readonly EcsFilter<GameComponent> game = null;
    private readonly EcsFilter<EnemyComponent, HealthComponent, PathPositionComponent> enemies = null;
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
        ref HealthComponent enemyHealth = ref this.enemies.Get2(enemyIndex);
        ref PlayerComponent player = ref this.playerFilter.Get1(0);
        player.Health -= enemyHealth.HealthPoints;

        // Send animation
        ref EnemyComponent enemyComponent = ref this.enemies.Get1(enemyIndex);
        game.Broadcaster.PathToPathAnimation(
            0x01,
            (short)(enemyComponent.PreviousImpactPosition * 20),
            (short)(pathPositionComponent.LengthTraveled * 20),
            (short)(enemyComponent.PreviousImpactTime * 20),
            (short)(game.Time * 20),
            0x01
        );

        // Remove enemy
        this.enemies.GetEntity(enemyIndex).Destroy();
      }
    }
  }
}
