// <copyright file="ECS.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.ECS
{
  using System;
  using System.Collections.Generic;

  /// <summary>
  /// Main object to interact with an ECS system.
  /// </summary>
  internal class ECS
  {
    /// <summary>
    ///
    /// </summary>
    public const int CHUNKSIZE = 16;
    
    private Dictionary<string, Archetype> archetypes = new Dictionary<string, Archetype>();
    
    public ECS()
    {
      this.CreateArchetype();
    }

    /// <summary>
    /// Create an entity in this <see cref="ECS"/> without any component.
    /// </summary>
    /// <returns><see cref="int"/> identifier for created entity.</returns>
    public int CreateEntity()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Create an archetype and add it to the Archetype dictionary, using the concatenation of the archetype's component hash codes as its key.
    /// </summary>
    public void CreateArchetype(params Type[] componentTypes)
    {
      archetypeKey = "";
      foreach (Type componentType in componentTypes)
      {
        archetypeKey += componentType.GetHashCode();
      }
      archetypes[archetypeKey] = new Archetype(componentTypes);
    }

    /// <summary>
    /// Add component to supplied <paramref name="entity"/>. This includes switching its archetype, moving the entity and reordering chunks.
    /// </summary>
    /// <param name="entity">Integer ID of entity to add component to.</param>
    /// <param name="component">Component to add.</param>
    public void AddComponent(int entity, IComponent component)
    {
      throw new NotImplementedException();
    }
  }
}
