// <copyright file="IComponent.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.ECS
{
  public readonly struct Position : IComponent
  {
    public readonly int x;
    public readonly int y;

    public Position(int x, int y)
    {
      this.x = x;
      this.y = y;
    }
  }
}