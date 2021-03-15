// <copyright file="ECS.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;

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
    /// Max size of chunks for <see cref="Archetype"/>s in this <see cref="ECS"/>.
    /// </summary>
    private readonly int chunkSize = 16;

    /// <summary>
    /// The ID of the next entity created by this <see cref="ECS"/>
    /// </summary>
    private int nextEntityID = 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="ECS"/> class.
    /// </summary>
    public ECS()
    {
      // Creates an Archetype with no component types.
      this.GetArchetype(new TypeSet());
    }

    /// <summary>
    /// Takes an entity and its components, finds the <see cref="TypeSet"/> from those components, and adds the entity to the archetype corresponding to that <see cref="TypeSet"/>.
    /// </summary>
    public void AddEntityToArchetype(int entity, params IComponent[] components)
    {
      TypeSet typeSet = new TypeSet(components);
      GetArchetype(typeSet).addEntity(entity, components);
    }

    /// <summary>
    /// Add component to supplied <paramref name="entity"/>. This includes switching its archetype, moving the entity and reordering chunks.
    /// </summary>
    /// <remarks>
    /// This method is quite slow.
    /// </remarks>
    /// <param name="entity">Integer ID of entity to add component to.</param>
    /// <param name="components">Components to add.</param>
    public void AddComponents(int entity, params IComponent[] components)
    {
      // Find archetype and components of old entity
      Archetype oldArchetype;
      IComponent[] oldComponents;
      foreach (Archetype archetype in this.archetypes.Values) {
        if (archetype.TryGetEntity(entity, out oldComponents)) {
          oldArchetype = archetype;
          break
        }
      }
      
      // Remove from old archetype
      oldArchetype.RemoveEntity(entity);

      // Add to new archetype with method call.
      Archetypes newArchetype = GetArchetype(new Typeset(components))
    }

    /// <summary>
    /// Create an entity in this <see cref="ECS"/> without any component.
    /// </summary>
    /// <returns><see cref="int"/> identifier for created entity.</returns>
    public int CreateEntity(params IComponent[] components)
    {
      this.AddEntityToArchetype(nextEntityID++, components);
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
    /// Takes a <see cref="TypeSet"/> and checks if an <see cref="Archetype"/> already exists for it, if not, it creates a new one.
    /// </summary>
    public Archetype GetArchetype(TypeSet typeSet)
    {
      Archetype archetype;
      if (!this.archetypes.TryGetValue(typeSet, out archetype))
      {
        archetype = this.CreateArchetype(typeSet);
      }
      return archetype;
    }

    /// <summary>
    /// Takes a <see cref="TypeSet"/> and creates a new <see cref="Archetype"/> from it, adding it to the archetypes map
    /// </summary>
    public Archetype CreateArchetype(TypeSet typeSet)
    {
      Archetype archetype = new Archetype(this.chunkSize, typeSet);
      this.archetypes[typeSet] = archetype;
      return archetype;
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
