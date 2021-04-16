// <copyright file="PlaceTowerSystem.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Game.Components;
using Leopotam.Ecs;

namespace BackEnd.Game.Systems
{
  internal class PlaceTowerSystem : IEcsInitSystem 
  {
    private readonly EcsWorld world = null;

    public void Init()
    {
      this.world.NewEntity().Replace(new TowerComponent()
      {
        TimeUntilReloaded = 3d,
      });
      this.world.NewEntity().Replace(new TowerComponent()
      {
        TimeUntilReloaded = 1d,
      });
      this.world.NewEntity().Replace(new TowerComponent()
      {
        TimeUntilReloaded = 2d,
      });
    }
  }
}
