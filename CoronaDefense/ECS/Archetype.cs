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
    private readonly Dictionary<Type, List<IComponent[]>> chunks = new Dictionary<Type, List<IComponent[]>>();

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
        this.chunks[componentType] = new List<IComponent[]>();
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

    /// <summary>
    /// Convert supplied <paramref name="index"/> to a pair of indices to be used in chunks.
    /// </summary>
    /// <param name="index">Index to convert to index pair.</param>
    /// <param name="chunkIndex">Index of the chunk the original <paramref name="index"/> is in.</param>
    /// <param name="entityIndex">Index of the entity within the chunk.</param>
    private void ConvertIndex(int index, out int chunkIndex, out int entityIndex)
    {
      chunkIndex = index / this.chunkSize;
      entityIndex = index % this.chunkSize;
    }

    /// <summary>
    /// Attempt to get the components of supplied <paramref name="entity"/>.
    /// </summary>
    /// <remarks>
    /// This method is quite slow if the entity is present. Do not use it just to determine if an entity is present in this <see cref="Archetype"/>.
    /// </remarks>
    /// <param name="entity">Entity to get components of.</param>
    /// <param name="components">Returns the components of requested <paramref name="entity"/> if supplied <paramref name="entity"/> was present in this <see cref="Archetype"/>, <see langword="null"/> otherwise.</param>
    /// <returns><see langword="true"/> if supplied <paramref name="entity"/> was present in this <see cref="Archetype"/>.</returns>
    public bool TryGetEntityComponents(int entity, out IComponent[] components)
    {
      if (!this.identifierIndex.TryGetValue(entity, out int index))
      {
        components = null;
        return false;
      }

      components = new IComponent[this.chunks.Count];
      this.ConvertIndex(index, out int chunkIndex, out int entityIndex);

      index = 0; // Reuse of variable for index in component array.
      foreach (List<IComponent[]> chunkList in this.chunks.Values)
      {
        components[index++] = chunkList[chunkIndex][entityIndex];
      }

      return true;
    }

    /// <summary>
    /// Attempt to remove an entity.
    /// </summary>
    /// <param name="entity">Identifier of entity to remove.</param>
    /// <returns><see langword="true"/> if the supplied <paramref name="entity"/> was present and was removed.</returns>
    public bool TryRemoveEntity(int entity)
    {
      if (!this.identifierIndex.TryGetValue(entity, out int index))
      {
        return false;
      }

      int lastIndex = --this.numberOfEntities;
      this.ConvertIndex(index, out int chunkIndex, out int entityIndex);
      this.ConvertIndex(lastIndex, out int lastChunkIndex, out int lastEntityIndex);

      foreach (List<IComponent[]> chunkList in this.chunks.Values)
      {
        chunkList[chunkIndex][entityIndex] = chunkList[lastChunkIndex][lastEntityIndex];
      }

      return true;
    }
  }
}
