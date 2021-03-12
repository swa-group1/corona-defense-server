// <copyright file="IComponent.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.ECS
{
  using System;
  using System.Collections.Generic;

  internal class Archetype
  {
    public Dictionary<Type, List<IComponent[]>> chunks = new Dictionary<Type, List<IComponent[]>>();
    public Dictionary<int, int> identifierIndex = new Dictionary<int, int>();
    public int numberOfEntities = 0;

    public Archetype(params Type[] componentTypes)
    {
      foreach (Type componentType in componentTypes)
      {
        this.chunks[componentType] = new List<IComponent[]>();
      }
    }

    public void AddEntity(int entity, params IComponent[] components)
    {

    }
  }
}
