// <copyright file="EndRoundSystem.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Game.Components;
using Leopotam.Ecs;

namespace BackEnd.Game.Systems
{
  /// <summary>
  /// System to reload towers.
  /// </summary>
  internal class EndRoundSystem : IEcsRunSystem
  {
    private const int MoneyPerRound = 250;

    private readonly EcsContainer container;
    private readonly EcsFilter<EnemyComponent, PathPositionComponent> enemyFilter = null;
    private readonly EcsFilter<GameComponent> gameFilter = null;
    private readonly EcsFilter<PlayerComponent> playerFilter = null;
    private readonly EcsFilter<TowerComponent> towerFilter = null;

    /// <summary>
    /// Initializes a new instance of the <see cref="EndRoundSystem"/> class.
    /// </summary>
    /// <param name="container"><see cref="EcsContainer"/> that the new <see cref="EndRoundSystem"/> is in and that it should stop when no enemies are found.</param>
    public EndRoundSystem(EcsContainer container)
    {
      this.container = container;
    }

    /// <inheritdoc/>
    public void Run()
    {
      ref GameComponent game = ref this.gameFilter.Get1(0);
      ref PlayerComponent player = ref this.playerFilter.Get1(0);

      if (player.Health <= 0)
      {
        // Player died
        this.container.HasPlayerDied = true;

        // Broadcast last animation for all remaining enemies
        foreach (int i in this.enemyFilter)
        {
          ref EnemyComponent enemy = ref this.enemyFilter.Get1(i);
          ref PathPositionComponent enemyPosition = ref this.enemyFilter.Get2(i);
          game.Broadcaster.PathToPathAnimation(
            (byte)enemy.SpriteNumber,
            (float)enemy.PreviousImpactPosition,
            (float)enemyPosition.LengthTraveled,
            (float)enemy.PreviousImpactTime,
            (float)game.Time,
            0x01
          );
        }
      }
      else if (this.enemyFilter.GetEntitiesCount() == 0)
      {
        // Player survived round
        player.Balance += MoneyPerRound;
        game.Broadcaster.MoneyAnimation(player.Balance, (float)game.Time);
      }
      else
      {
        return;
      }

      this.container.Running = false;

      // Reset time
      game.Time += game.TickDuration;
      game.Broadcaster.AnimationConfirmation((float)game.Time);
      game.Time = 0d;

      // Calculate score
      this.container.Score = 0;
      this.container.Score += player.Balance;
      this.container.Score += 5 * player.Health;
      this.container.Score += player.PopCount;
      foreach (int i in this.towerFilter)
      {
        ref TowerComponent tower = ref this.towerFilter.Get1(i);
        this.container.Score += (int)(tower.MediumCost * game.TowerCostFactor * game.TowerSaleFactor);
      }
    }
  }
}
