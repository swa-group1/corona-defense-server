// <copyright file="PlaceEnemySystem.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Game.Components;
using Leopotam.Ecs;
using System.Collections.Generic;

namespace BackEnd.Game.Systems
{
  /// <summary>
  /// System to start rounds.
  /// </summary>
  internal class PlaceEnemySystem : IEcsSystem
  {
    private readonly EcsFilter<GameComponent> gameFilter = null;
    private int nextRound = 0;
    private readonly RoundDefinitions rounds;
    private readonly EcsWorld world = null;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlaceEnemySystem"/> class.
    /// </summary>
    /// <param name="roundDefinitions">Round definitions to utilize.</param>
    public PlaceEnemySystem(RoundDefinitions roundDefinitions)
    {
      this.rounds = roundDefinitions;
    }

    /// <summary>
    /// Place enemies.
    /// </summary>
    public void PlaceEnemies()
    {
      GameComponent game = this.gameFilter.Get1(0);

      double entryTime = 1d;

      IList<RoundDefinitions.RoundSection> sections = this.rounds.Rounds[this.nextRound];
      this.nextRound++;
      foreach (RoundDefinitions.RoundSection section in sections)
      {
        for (int i = 0; i < section.Count; i++)
        {
          entryTime += section.DeltaTime;

          if (section.EnemyType == "None")
          {
            break;
          }

          if (!game.EnemyTypeMap.TryGetValue(section.EnemyType, out EnemyType enemyType))
          {
            break;
          }

          EcsEntity enemyEntity = this.world.NewEntity();
          ref EnemyComponent enemy = ref enemyEntity.Get<EnemyComponent>();
          enemy.NextType = enemyType.NextType;
          enemy.PlayerDamage = enemyType.Health;
          enemy.SpriteNumber = enemyType.SpriteNumber;

          ref ImpactComponent impactComponent = ref enemyEntity.Get<ImpactComponent>();
          impactComponent.PreviousImpactPosition = 0d;
          impactComponent.PreviousImpactTime = entryTime;

          ref PathSpeedComponent enemySpeed = ref enemyEntity.Get<PathSpeedComponent>();
          enemySpeed.Speed = enemyType.Speed;

          ref PathPositionComponent enemyPosition = ref enemyEntity.Get<PathPositionComponent>();
          enemyPosition.LengthTraveled = -enemySpeed.Speed * entryTime;

          ref ProjectedHealthComponent enemyProjectedHealth = ref enemyEntity.Get<ProjectedHealthComponent>();
          enemyProjectedHealth.ProjectedHealthPoints = enemyType.Health;
        }
      }
    }
  }
}
