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
    private readonly Dictionary<string, EnemyDefinitions.EnemyType> enemyTypes;
    private int nextRound = 0;
    private readonly RoundDefinitions rounds;
    private readonly EcsWorld world = null;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlaceEnemySystem"/> class.
    /// </summary>
    /// <param name="enemyDefinitions">Enemy definitions to utilize.</param>
    /// <param name="roundDefinitions">Round definitions to utilize.</param>
    public PlaceEnemySystem(EnemyDefinitions enemyDefinitions, RoundDefinitions roundDefinitions)
    {
      this.enemyTypes = new Dictionary<string, EnemyDefinitions.EnemyType>();
      foreach (EnemyDefinitions.EnemyType enemyType in enemyDefinitions.EnemyTypes)
      {
        this.enemyTypes.Add(enemyType.Name, enemyType);
      }

      this.rounds = roundDefinitions;
    }

    /// <summary>
    /// Place enemies.
    /// </summary>
    public void PlaceEnemies()
    {
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

          if (!this.enemyTypes.TryGetValue(section.EnemyType, out EnemyDefinitions.EnemyType enemyType))
          {
            break;
          }

          EcsEntity enemyEntity = this.world.NewEntity();
          ref EnemyComponent enemy = ref enemyEntity.Get<EnemyComponent>();
          enemy.PreviousImpactPosition = 0d;
          enemy.PreviousImpactTime = entryTime;

          ref HealthComponent enemyHealth = ref enemyEntity.Get<HealthComponent>();
          enemyHealth.HealthPoints = enemyType.Health;

          ref PathSpeedComponent enemySpeed = ref enemyEntity.Get<PathSpeedComponent>();
          enemySpeed.Speed = enemyType.Speed;

          ref PathPositionComponent enemyPosition = ref enemyEntity.Get<PathPositionComponent>();
          enemyPosition.LengthTraveled = -enemySpeed.Speed * entryTime;

          ref ProjectedHealthComponent enemyProjectedHealth = ref enemyEntity.Get<ProjectedHealthComponent>();
          enemyProjectedHealth.ProjectedHealthPoints = enemyHealth.HealthPoints;
        }
      }
    }
  }
}
