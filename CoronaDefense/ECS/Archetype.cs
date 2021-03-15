// <copyright file="Archetype.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;

namespace ECS
{
  /// <summary>
  /// Class whose instances store a set of entities with the same component types attached.
  /// </summary>
  internal class Archetype
  {
    /// <summary>
    /// Map from component types to lists with component chunks.
    /// </summary>
    private readonly Dictionary<Type, List<object[]>> chunks = new Dictionary<Type, List<object[]>>();

    /// <summary>
    /// Max size of chunks in this <see cref="Archetype"/>.
    /// </summary>
    private readonly int chunkSize;

    /// <summary>
    /// Set of components types in this <see cref="Archetype"/>.
    /// </summary>
    private readonly TypeSet componentTypes;

    /// <summary>
    /// Map from entity IDs to indices in chunks.
    /// </summary>
    private readonly Dictionary<int, int> identifierIndex = new Dictionary<int, int>();

    /// <summary>
    /// Number of entities currently stored in this <see cref="Archetype"/>.
    /// </summary>
    private int numberOfEntities = 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="Archetype"/> class.
    /// </summary>
    /// <param name="chunkSize">Max size of chunks in the new <see cref="Archetype"/>.</param>
    /// <param name="componentTypes">List of component types in arbitrary order.</param>
    public Archetype(int chunkSize, TypeSet componentTypes)
    {
      this.componentTypes = componentTypes;
      foreach (Type componentType in this.componentTypes)
      {
        this.chunks[componentType] = new List<object[]>();
      }
    }

    /// <summary>
    /// Add supplied <paramref name="entity"/> to this <see cref="Archetype"/>.
    /// </summary>
    /// <remarks>
    /// Undefined behaviour will happen if the components do not match up with the component types in this <see cref="Archetype"/>.
    /// </remarks>
    /// <param name="entity">Entity to add.</param>
    /// <param name="components">Exhaustive list of components attached to the <paramref name="entity"/>.</param>
    public void AddEntity(int entity, params IComponent[] components)
    {
      throw new NotImplementedException();
    }

    private void MoveEntity(int )

    // TODO: Documentation.
    public void RemoveEntity(int entity)
    {
      
    }

    // TODO: Documentation.
    /// <remarks>
    /// This method is quite slow.
    /// </remarks>
    public bool TryGetEntity(int entity, out IComponent[] components)
    {
      if (!this.identifierIndex.TryGetValue(entity, out int index))
      {
        components = null;
        return false;
      }

      components = new IComponent[this.chunks.Count];
      int chunkNumber = index / this.chunkSize;
      int entityNumber = index % this.chunkSize;
      foreach ()
    }
  }
}
