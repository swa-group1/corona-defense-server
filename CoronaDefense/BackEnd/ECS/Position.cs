// <copyright file="IComponent.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.ECS
{
  internal readonly struct Position : IComponent
  {
    public readonly int x;
    public readonly int y;

    public Position(int x, int y)
    {
      this.x = x;
      this.y = y;
    }

    public move(int x, int y) {
      return new Position(this.x + x, this.y + y);
    }
  }
}