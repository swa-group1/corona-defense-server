// <copyright file="PlayerInitializeSystem.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Game.Components;
using Leopotam.Ecs;

namespace BackEnd.Game.Systems
{
  /// <summary>
  /// Game to initialize player.
  /// </summary>
  internal class PlayerInitializeSystem : IEcsPreInitSystem
  {
    private const int StartHealth = 100;
    private const int StartBalance = 500;

    private readonly EcsWorld world = null;

    /// <inheritdoc/>
    public void PreInit()
    {
      ref PlayerComponent player = ref this.world.NewEntity().Get<PlayerComponent>();
      player.Health = StartHealth;
      player.Balance = StartBalance;
    }
  }
}
