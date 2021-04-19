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
    private readonly EcsFilter<EnemyComponent> enemyFilter = null;
    private readonly EcsFilter<GameComponent> gameFilter = null;
    private readonly EcsFilter<PlayerComponent> playerFilter = null;

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
      game.Time += 5 * game.TickDuration;
      game.Broadcaster.AnimationConfirmation((float)game.Time);
      game.Time = 0d;
    }
  }
}
