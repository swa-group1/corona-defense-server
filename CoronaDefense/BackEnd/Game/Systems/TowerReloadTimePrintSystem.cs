// <copyright file="TowerReloadTimePrintSystem.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Game.Components;
using Leopotam.Ecs;
using System;

namespace BackEnd.Game.Systems
{
  internal class TowerReloadTimePrintSystem : IEcsRunSystem
  {
    private readonly EcsFilter<TowerComponent> towers = null;

    /// <inheritdoc/>
    public void Run()
    {
      foreach (int i in this.towers)
      {
        ref TowerComponent towerComponent = ref this.towers.Get1(i);
        Console.WriteLine(towerComponent.TimeUntilReloaded);
      }

      Console.WriteLine();
    }
  }
}
