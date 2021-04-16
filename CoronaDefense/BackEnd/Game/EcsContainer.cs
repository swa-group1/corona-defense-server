using API.Requests;
using BackEnd.Game.Systems;
using Leopotam.Ecs;
using System.Collections.Concurrent;

namespace BackEnd.Game
{
  internal class EcsContainer
  {
    private const int TickNumber = 20;

    private EcsWorld world;
    private EcsSystems systems;

    public ConcurrentQueue<PlaceTowerRequest> PlaceTowerRequests { get; } = new ConcurrentQueue<PlaceTowerRequest>();

    public EcsContainer(Broadcaster broadcaster)
    {
      this.world = new EcsWorld();
      this.systems = new EcsSystems(this.world);

      this.systems.Add(new GameInitializeSystem(broadcaster, 1d / TickNumber));
      this.systems.Add(new PlaceTowerSystem(this.PlaceTowerRequests));
      this.systems.Add(new PathMoveSystem());
      this.systems.Add(new ReloadSystem());
      this.systems.Add(new TowerReloadTimePrintSystem());

      this.systems.Init();
    }

    public void TestRun(int ticks)
    {
      for (; 0 < ticks; --ticks)
      {
        this.systems.Run();
      }
    }
  }
}
