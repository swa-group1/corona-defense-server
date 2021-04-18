// <copyright file="PlaceEnemySystem.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Game.Components;
using Leopotam.Ecs;

namespace BackEnd.Game.Systems
{
  /// <summary>
  /// System to start rounds.
  /// </summary>
  internal class PlaceEnemySystem : IEcsSystem
  {
    private readonly EcsWorld world = null;

    /// <summary>
    /// Place enemies.
    /// </summary>
    public void PlaceEnemies()
    {
      double entryTime = 1d;
      for (int i = 1; i <= 10; i++)
      {
        entryTime += 0.5d;

        EcsEntity enemyEntity = this.world.NewEntity();
        ref EnemyComponent enemy = ref enemyEntity.Get<EnemyComponent>();
        enemy.PreviousImpactPosition = 0d;
        enemy.PreviousImpactTime = entryTime;
        ref HealthComponent enemyHealth = ref enemyEntity.Get<HealthComponent>();
        enemyHealth.HealthPoints = 1;
        ref PathSpeedComponent enemySpeed = ref enemyEntity.Get<PathSpeedComponent>();
        enemySpeed.Speed = 1d;
        ref PathPositionComponent enemyPosition = ref enemyEntity.Get<PathPositionComponent>();
        enemyPosition.LengthTraveled = -enemySpeed.Speed * entryTime;
        ref ProjectedHealthComponent enemyProjectedHealth = ref enemyEntity.Get<ProjectedHealthComponent>();
        enemyProjectedHealth.ProjectedHealthPoints = 1;
      }
    }
  }
}
