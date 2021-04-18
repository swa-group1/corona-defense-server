// <copyright file="EndFightRoundSystem.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Game.Components;
using Leopotam.Ecs;

namespace BackEnd.Game.Systems
{
  /// <summary>
  /// System to reload towers.
  /// </summary>
  internal class EndFightRoundSystem : IEcsRunSystem
  {
    private const int MoneyPerRound = 250;

    private readonly EcsContainer container;
    private readonly EcsFilter<EnemyComponent> enemyFilter = null;
    private readonly EcsFilter<GameComponent> gameFilter = null;
    private readonly EcsFilter<PlayerComponent> playerFilter = null;

    /// <summary>
    /// Initializes a new instance of the <see cref="EndFightRoundSystem"/> class.
    /// </summary>
    /// <param name="container"><see cref="EcsContainer"/> that the new <see cref="EndFightRoundSystem"/> is in and that it should stop when no enemies are found.</param>
    public EndFightRoundSystem(EcsContainer container)
    {
      this.container = container;
    }

    /// <inheritdoc/>
    public void Run()
    {
      if (0 < this.enemyFilter.GetEntitiesCount())
      {
        return;
      }

      // Money
      ref PlayerComponent player = ref this.playerFilter.Get1(0);
      player.Balance += MoneyPerRound;

      // Running
      this.container.Running = false;

      // Reset time
      this.gameFilter.Get1(0).Time = 0d;
    }
  }
}
