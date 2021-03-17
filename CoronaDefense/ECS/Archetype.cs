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
    /// Map from component types to lists with components encoded as <see langword="byte"/>s.
    /// </summary>
    private readonly Dictionary<Type, Chunk> chunks = new Dictionary<Type, Chunk>();

    /// <summary>
    /// Map from entity IDs to indices in component lists.
    /// </summary>
    private readonly Dictionary<int, int> identifierIndex = new Dictionary<int, int>();

    /// <summary>
    /// Number of entities currently stored in this <see cref="Archetype"/>.
    /// </summary>
    private int numberOfEntities = 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="Archetype"/> class.
    /// </summary>
    // TODO: add params doc.
    public Archetype(int initialEntity, IEnumerable<IComponent> components)
    {
      foreach (IComponent component in components)
      {
        this.chunks[component.GetType()] = new Chunk(component);
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
    public void AddEntity(int entity, IEnumerable<IComponent> components)
    {
      throw new NotImplementedException();
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
    public bool TryGetEntityComponents(int entity, out ICollection<IComponent> components)
    {
      if (!this.identifierIndex.TryGetValue(entity, out int index))
      {
        components = null;
        return false;
      }

      List<IComponent> components = new List<IComponent>(this.chunks.Count);
      foreach (KeyValuePair<Type, Chunk> pair in this.chunks)
      {
        int index = this.identifierIndex[entity] * pair.Value.componentSize;
        byte[] bytes = new byte[pair.Value.componentSize];
        pair.Value.components.CopyTo(index, bytes, 0, pair.Value.componentSize);
        IComponent component = ((IComponent)Activator.CreateInstance(pair.Key)).FromBytes(bytes);
        components.Add(component);
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
