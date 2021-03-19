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
    /// List of entity IDs.
    /// </summary>
    private readonly List<int> entities = new List<int>();

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
      this.entities.Add(initialEntity);
      foreach (IComponent component in components)
      {
        this.chunks[component.GetType()] = new Chunk(component);
      }
      this.numberOfEntities++;
    }

    /// <summary>
    /// Add supplied <paramref name="entity"/> to this <see cref="Archetype"/>.
    /// </summary>
    /// <remarks>
    /// Undefined behaviour will happen if the components do not match up with the component types in this <see cref="Archetype"/>.
    /// </remarks>
    /// <param name="entity">Entity to add.</param>
    /// <param name="components">Exhaustive list of components attached to the <paramref name="entity"/>.</param>
    /// <returns></returns>
    public bool TryAddEntity(int entity, IEnumerable<IComponent> components)
    {
      if (this.identifierIndex.ContainsKey(entity))
      {
        return false;
      }

      this.entities.Add(entity);
      foreach (IComponent component in components)
      {
        chunks[component.GetType()].components.AddRange(component.ToBytes());
      }
      this.numberOfEntities++;
      return true;
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

      components = new List<IComponent>(this.chunks.Count);
      foreach (Chunk chunk in this.chunks.Values)
      {
        int chunkIndex = index * chunk.componentSize; // Convert raw index to index within chunk.
        byte[] bytes = new byte[chunk.componentSize]; // Create space for byte array.
        chunk.components.CopyTo(chunkIndex, bytes, 0, chunk.componentSize); // Fill byte array.
        IComponent component = chunk.FromBytes(bytes); // Convert bytes to component
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
      
      this.numberOfEntities--;
    
      // Move data
      foreach (Chunk chunk in this.chunks.Values)
      {
        int indexInChunk = index * chunk.componentSize; // Convert raw index to index within chunk.
        int lastIndexInChunk = this.numberOfEntities * chunk.componentSize; // Convert index of last component to index within chunk.
        byte[] lastComponent = byte[chunk.componentSize]; // Create buffer for last component.
        chunk.components.CopyTo(lastIndexInChunk, lastComponent, 0, chunk.componentSize); // Copy last component.
        foreach (byte data in lastComponent)
        {
          chunk.components[indexInChunk++] = data;
        }
        chunk.components.RemoveRange(lastIndexInChunk, chunk.componentSize);
      }

      // Change index and entity list
      int lastEntity = this.entities[this.numberOfEntities]
      this.identifierIndex.Remove(entity);
      this.identifierIndex[lastEntity] = index;
      this.entities[index] = lastEntity;
      this.entities.RemoveAt(this.numberOfEntities);

      return true;
    }
  }
}
