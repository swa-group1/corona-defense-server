// <copyright file="ECS.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.ECS
{
  using System;

  /// <summary>
  /// Main object to interact with an ECS system.
  /// </summary>
  internal class ECS
  {
    /// <summary>
    /// Create an entity in this <see cref="ECS"/> without any component.
    /// </summary>
    /// <returns><see cref="int"/> identifier for created entity.</returns>
    internal int CreateEntity()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Add component to supplied <paramref name="entity"/>. This includes switching its archetype, moving the entity and reordering chunks.
    /// </summary>
    /// <param name="entity">Integer ID of entity to add component to.</param>
    /// <param name="component">Component to add.</param>
    internal void AddComponent(int entity, IComponent component)
    {
      throw new NotImplementedException();
    }
  }
}
