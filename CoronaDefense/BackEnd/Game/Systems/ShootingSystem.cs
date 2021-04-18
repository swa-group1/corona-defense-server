// <copyright file="ShootingSystem.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Game.Components;
using Leopotam.Ecs;
using System;
using System.Collections.Generic;

namespace BackEnd.Game.Systems
{
  /// <summary>
  /// System to reload towers.
  /// </summary>
  internal class ShootingSystem : IEcsRunSystem
  {
    private const double IterativeTolerance = 0.1d;

    private readonly EcsFilter<GameComponent> gameFilter = null;
    private readonly EcsFilter<BoardPositionComponent, TowerComponent, ReloadedTag> towers = null;
    private readonly EcsFilter<PathPositionComponent, PathSpeedComponent, HealthComponent, ProjectedHealthComponent> targetFilter = null;

    /// <inheritdoc/>
    public void Run()
    {
      ref GameComponent game = ref this.gameFilter.Get1(0);

      // Find lengths traveled sorted from large to small.
      List<(int Index, double Position)> targetPositions = new List<(int Index, double Position)>();
      foreach (int i in this.targetFilter)
      {
        targetPositions.Add((i, this.targetFilter.Get1(i).LengthTraveled));
      }

      targetPositions.Sort((a, b) => { return b.Position.CompareTo(a.Position); }); // Sort descending

      // For each target, find tower that can hit it.
      foreach ((int index, double position) in targetPositions)
      {
        Stage.Point targetPosition = game.Stage.GetPointAlongPath(position);

        if (2 * position < game.Stage.PathLength)
        {
          if (!game.Stage.IsOnStage(targetPosition))
          {
            // First tower found that is before stage. No more targets should be processed.
            break;
          }
        }

        if (!this.TryFindShooter(targetPosition, out int towerIndex))
        {
          continue;
        }

        // Find impact time
        ref TowerComponent towerComponent = ref this.towers.Get2(towerIndex);
        ref PathSpeedComponent targetSpeed = ref this.targetFilter.Get2(index);
        ref BoardPositionComponent towerPosition = ref this.towers.Get1(towerIndex);
        this.FindImpactTime(
          position,
          targetSpeed.Speed,
          IterativeTolerance,
          towerPosition.Position,
          towerComponent.ProjectileSpeed,
          out double impactPosition,
          out double timeUntilImpact
        );

        // Start reload time
        ref EcsEntity towerEntity = ref this.towers.GetEntity(towerIndex);
        towerComponent.TimeUntilReloaded = towerComponent.ReloadTime;
        towerEntity.Del<ReloadedTag>();

        // Enemy damage
        ref EcsEntity targetEntity = ref this.targetFilter.GetEntity(index);
        if (targetEntity.Has<ImpactTimerComponent>())
        {
          targetEntity.Get<ImpactTimerComponent>().ImpactTimers.Add(timeUntilImpact);
        }
        else
        {
          targetEntity.Get<ImpactTimerComponent>().ImpactTimers = new List<double>() { timeUntilImpact };
        }

        ref ProjectedHealthComponent projectedHealth = ref this.targetFilter.Get4(index);
        projectedHealth.ProjectedHealthPoints -= 1;
        if (projectedHealth.ProjectedHealthPoints <= 0)
        {
          targetEntity.Del<ProjectedHealthComponent>();
        }

        // TODO Queue tower animation

        // Queue animation for projectile
        game.Broadcaster.BoardToPathAnimation(
          0x01,
          (byte)towerPosition.Position.X,
          (byte)towerPosition.Position.Y,
          (float)impactPosition,
          (float)game.Time,
          (float)(game.Time + timeUntilImpact),
          0x00
        );
      }
    }

    /// <summary>
    /// Calculate information about impact given supplied target and tower.
    /// </summary>
    /// <param name="targetPosition">Length along path that target is currently at.</param>
    /// <param name="targetSpeed">Distance per second traveled along path by target.</param>
    /// <param name="tolerance">The allowed distance between the projectile and the target on impact.</param>
    /// <param name="towerTile">Tile where tower is located.</param>
    /// <param name="projectileSpeed">Distance per second traveled by projectiles shot by tower.</param>
    /// <param name="impactPosition">The target's position when the projectile will hit it.</param>
    /// <param name="impactTime">How much time will pass before the projectile will hit the target.</param>
    /// <param name="maxIterations">The maximum number of iterations to run.</param>
    private void FindImpactTime(
      double targetPosition,
      double targetSpeed,
      double tolerance,
      Stage.Tile towerTile,
      double projectileSpeed,
      out double impactPosition,
      out double impactTime,
      int maxIterations = 4
    )
    {
      ref GameComponent game = ref this.gameFilter.Get1(0);

      Stage.Point towerPoint = (Stage.Point)towerTile;
      double distance = 0d;
      impactTime = 0d;
      for (int i = 0; i < maxIterations; i++)
      {
        Stage.Point projectedTargetPoint = game.Stage.GetPointAlongPath(targetPosition + distance);
        double projectileDistance = Stage.Point.Distance(projectedTargetPoint, towerPoint);
        impactTime = projectileDistance / projectileSpeed; // Update the time with the duration of projectile flight given old distance.
        double newDistance = impactTime * targetSpeed;

        // Check if within tolerance
        if (Math.Abs(distance - newDistance) < tolerance)
        {
          impactPosition = targetPosition + newDistance;
          return;
        }

        // Update values
        distance = newDistance;
      }

      impactPosition = targetPosition + distance;
    }

    private bool TryFindShooter(Stage.Point position, out int towerIndex)
    {
      foreach (int i in this.towers)
      {
        BoardPositionComponent towerPosition = this.towers.Get1(i);
        TowerComponent tower = this.towers.Get2(i);
        double squareDistance = Stage.Point.SquareDistance((Stage.Point)towerPosition.Position, position);
        if (squareDistance <= tower.Range * tower.Range)
        {
          towerIndex = i;
          return true;
        }
      }

      towerIndex = -1;
      return false;
    }
  }
}
