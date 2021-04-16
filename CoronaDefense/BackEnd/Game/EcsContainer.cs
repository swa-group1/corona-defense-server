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
    /// Initializes a new instance of the <see cref="EcsContainer"/> class.
    /// </summary>
    /// <param name="broadcaster"></param>
    /// <param name="stage"></param>
    public EcsContainer(Broadcaster broadcaster, Stage stage)
    {
      this.world = new EcsWorld();
      this.systems = new EcsSystems(this.world);

      // Init
      this.systems.Add(new GameInitializeSystem(broadcaster, stage, 1d / TickNumber));

      // Input
      this.systems.Add(new PlaceTowerSystem(this.PlaceTowerRequests));

      // Preframe
      this.systems.Add(new TimeSystem());

      // Frame
      this.systems.Add(new PathMoveSystem());
      this.systems.Add(new ReloadSystem());

      // Debug
      this.systems.Add(new TowerReloadTimePrintSystem());

      this.systems.Init();
    }
  }
}
