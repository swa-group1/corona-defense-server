using API.Requests;
using BackEnd.Game.Systems;
using Leopotam.Ecs;
using System.Collections.Concurrent;

namespace BackEnd.Game
{
  /// <summary>
  /// Container for a ECS world.
  /// </summary>
  internal class EcsContainer
  {
    private const int TickNumber = 20;

    private readonly EcsWorld world;
    private readonly EcsSystems systems;

    /// <summary>
    /// Gets queue of <see cref="PlaceTowerRequest"/>s intended for this <see cref="EcsContainer"/>.
    /// </summary>
    public ConcurrentQueue<PlaceTowerRequest> PlaceTowerRequests { get; } = new ConcurrentQueue<PlaceTowerRequest>();

    /// <summary>
    /// Gets queue of <see cref="LocalRequest"/>s that signal that round should start.
    /// </summary>
    public ConcurrentQueue<LocalRequest> StartRoundRequests { get; } = new ConcurrentQueue<LocalRequest>();

    /// <summary>
    /// Initializes a new instance of the <see cref="EcsContainer"/> class.
    /// </summary>
    /// <param name="broadcaster"><see cref="Broadcaster"/> to send game messages to.</param>
    /// <param name="stage"><see cref="Stage"/> to play on.</param>
    public EcsContainer(Broadcaster broadcaster, Stage stage)
    {
      this.world = new EcsWorld();
      this.systems = new EcsSystems(this.world);

      // Init
      _ = this.systems.Add(new GameInitializeSystem(broadcaster, stage, 1d / TickNumber));
      _ = this.systems.Add(new PlayerInitializeSystem());

      // Input
      _ = this.systems.Add(new PlaceTowerSystem(this.PlaceTowerRequests));
      _ = this.systems.Add(new StartFightRoundSystem(this.StartRoundRequests));

      // Preframe
      _ = this.systems.Add(new TimeSystem());

      // Frame
      _ = this.systems.Add(new PathMoveSystem());
      _ = this.systems.Add(new ReloadSystem());
      _ = this.systems.Add(new HurtPlayerSystem());

      this.systems.Init();
    }
  }
}
