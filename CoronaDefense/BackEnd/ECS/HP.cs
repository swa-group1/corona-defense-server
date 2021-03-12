// <copyright file="IComponent.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.ECS
{
  internal readonly struct HP : IComponent
  {
    public readonly int hp;

    public HP(int hp)
    {
      this.hp = hp;
    }
  }
}