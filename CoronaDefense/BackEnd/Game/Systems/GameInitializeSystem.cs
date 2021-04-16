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

    private readonly Broadcaster broadcaster;
    private readonly Stage stage;
    private readonly double tickDuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="GameInitializeSystem"/> class.
    /// </summary>
    /// <param name="broadcaster"><see cref="Broadcaster"/> to be used to send messages to clients.</param>
    /// <param name="stage"><see cref="Stage"/> of this game.</param>
    /// <param name="tickDuration">Duration of tick in seconds.</param>
    public GameInitializeSystem(Broadcaster broadcaster, Stage stage, double tickDuration)
    {
      this.broadcaster = broadcaster;
      this.stage = stage;
      this.tickDuration = tickDuration;
    }

    /// <inheritdoc/>
    public void PreInit()
    {
      ref GameComponent game = ref this.world.NewEntity().Get<GameComponent>();
      game.Broadcaster = this.broadcaster;
      game.Stage = this.stage;
      game.TickDuration = this.tickDuration;
      game.Time = 0d;
    }
  }
}
