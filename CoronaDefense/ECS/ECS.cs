// <copyright file="ECS.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace ECS
{
  using System;
  using System.Collections.Generic;

  /// <summary>
  /// Main object to interact with an ECS system.
  /// </summary>
  internal class ECS
  {
    /// <summary>
    /// Max size of chunks for <see cref="Archetype"/>s in this <see cref="ECS"/>.
    /// </summary>
    private readonly int chunkSize = 16;

    /// <summary>
    /// Map from <see cref="TypeSet"/>s to <see cref="Archetype"/>s.
    /// </summary>
    private Dictionary<TypeSet, Archetype> archetypes = new Dictionary<TypeSet, Archetype>();

    /// <summary>
    /// Initializes a new instance of the <see cref="ECS"/> class.
    /// </summary>
    public ECS()
    {
      this.CreateArchetype();
    }

    /// <summary>
    /// Add component to supplied <paramref name="entity"/>. This includes switching its archetype, moving the entity and reordering chunks.
    /// </summary>
    /// <param name="entity">Integer ID of entity to add component to.</param>
    /// <param name="component">Component to add.</param>
    /// <typeparam name="T">Struct type of component to add.</typeparam>
    internal void AddComponent<T>(int entity, T component)
      where T : struct
    {
      throw new NotImplementedException();
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
    /// <param name="componentTypes"></param>
    public void CreateArchetype(params Type[] componentTypes)
    {
      TypeSet typeSet = new TypeSet(componentTypes);
      this.CreateArchetype(typeSet);
    }

    /// <summary>
    /// Create an <see cref="Archetype"/> for this <see cref="ECS"/>.
    /// </summary>
    /// <param name="typeSet"><see cref="TypeSet"/> of <see cref="Archetype"/> to create.</param>
    public void CreateArchetype(TypeSet typeSet)
    {
      Archetype archetype = new Archetype(this.chunkSize, typeSet);
      this.archetypes[typeSet] = archetype;
    }

    /// <summary>
    /// Add component to supplied <paramref name="entity"/>. This includes switching its archetype, moving the entity and reordering chunks.
    /// </summary>
    /// <param name="entity">Integer ID of entity to add component to.</param>
    /// <param name="components">Components to add.</param>
    public void AddComponent(int entity, params object[] components)
    {
      throw new NotImplementedException();
    }
    
    /// Delete an entity from this <see cref="ECS"/>.
    /// </summary>
    /// <param name="entity">Integer ID of entity to delete.</param>
    internal void DeleteEntity(int entity)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Remove entity from archetype chunks in this <see cref="ECS"/>.
    /// </summary>
    /// <param name="entity">Integer ID of entity to remove.</param>
    private void RemoveEntityFromArchetype(int entity)
    {
      throw new NotImplementedException();
    }
  }
}
