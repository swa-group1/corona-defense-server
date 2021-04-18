// <copyright file="GameInitializeSystem.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Game.Components;
using Leopotam.Ecs;

namespace BackEnd.Game.Systems
{
  /// <summary>
  /// Game to initialize game constants in the ECS world.
  /// </summary>
  internal class GameInitializeSystem : IEcsPreInitSystem
  {
    private readonly EcsWorld world = null;

    private readonly GameComponent gameSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="GameInitializeSystem"/> class.
    /// </summary>
    /// <param name="gameSettings">Settings to initialize game with.</param>
    public GameInitializeSystem(GameComponent gameSettings)
    {
      this.gameSettings = gameSettings;
    }

    /// <inheritdoc/>
    public void PreInit()
    {
      _ = this.world.NewEntity().Replace(this.gameSettings);
    }
  }
}
