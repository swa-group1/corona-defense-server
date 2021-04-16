using BackEnd.Game.Systems;
using Leopotam.Ecs;

namespace BackEnd.Game
{
  internal class EcsContainer
  {
    private const int TickNumber = 20;

    private EcsWorld world;
    private EcsSystems systems;

    public EcsContainer()
    {
      this.world = new EcsWorld();
      this.systems = new EcsSystems(this.world);

      this.systems.Add(new GameInitializeSystem(1d / TickNumber));
      this.systems.Add(new PlaceTowerSystem());
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
