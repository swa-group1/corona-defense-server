// <copyright file="ECS.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;

namespace ECS
{
  /// <summary>
  /// Main object to interact with an ECS system.
  /// </summary>
  public class ECS
  {
    /// <summary>
    /// Map from <see cref="TypeSet"/>s to <see cref="Archetype"/>s.
    /// </summary>
    private readonly Dictionary<TypeSet, Archetype> archetypes = new Dictionary<TypeSet, Archetype>();

    /// <summary>
    /// The ID of the next entity created by this <see cref="ECS"/>.
    /// </summary>
    private int nextEntityId = 0;

    /// <summary>
    /// Takes an entity and its components, finds the <see cref="TypeSet"/> from those components, and adds the entity to the archetype corresponding to that <see cref="TypeSet"/>.
    /// </summary>
    private void AddEntityToArchetype(int entity, ICollection<IComponent> components)
    {
      TypeSet typeSet = new TypeSet(components.GetTypes());
      if (this.archetypes.TryGetValue(typeSet, out Archetype archetype))
      {
        // Archetype did exist beforehand.
        archetype.AddEntity(entity, components);
      }
      else
      {
        // Archetype did not exist.
        this.archetypes[typeSet] = new Archetype(entity, components);
      }
    }

    /// <summary>
    /// Add component to supplied <paramref name="entity"/>. This includes switching its archetype, moving the entity and reordering chunks.
    /// </summary>
    /// <remarks>
    /// This method is quite slow.
    /// </remarks>
    /// <param name="entity">Integer ID of entity to add component to.</param>
    /// <param name="components">Components to add.</param>
    public void AddComponents(int entity, IEnumerable<IComponent> components)
    {
      // Find archetype and components of old entity
      ICollection<IComponent> oldComponents = null;
      Archetype oldArchetype = null;
      foreach (Archetype archetype in this.archetypes.Values)
      {
        if (archetype.TryGetEntityComponents(entity, out oldComponents))
        {
          oldArchetype = archetype;
          break;
        }
      }

      if (oldArchetype == null)
      {
        return;
      }

      // Remove from old archetype
      // No need to check return as the entity is guaranteed to be in the archetype.
      oldArchetype.TryRemoveEntity(entity);

      this.AddEntityToArchetype(entity, oldComponents.Concat(components).ToList());
    }

    /// <summary>
    /// Create an entity in this <see cref="ECS"/> supplied <paramref name="components"/>.
    /// </summary>
    /// <param name="components">Components of entity to create.</param>
    /// <returns><see cref="int"/> identifier for created entity.</returns>
    public int CreateEntity(ICollection<IComponent> components)
    {
      int id = this.nextEntityId++;
      this.AddEntityToArchetype(id, components);
      return id;
    }

    /// <summary>
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
