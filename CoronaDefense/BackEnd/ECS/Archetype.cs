// <copyright file="Archetype.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.ECS
{
  using System;
  using System.Collections.Generic;

  /// <summary>
  /// Class whose instances store a set of entities with the same component types attached.
  /// </summary>
  internal class Archetype
  {
    /// <summary>
    /// Map from component types to lists with component chunks.
    /// </summary>
    private Dictionary<Type, List<object[]>> chunks = new Dictionary<Type, List<object[]>>();

    /// <summary>
    /// Map from entity IDs to indices in chunks.
    /// </summary>
    private Dictionary<int, int> identifierIndex = new Dictionary<int, int>();

    /// <summary>
    /// Number of entities currently stored in this <see cref="Archetype"/>.
    /// </summary>
    private int numberOfEntities = 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="Archetype"/> class.
    /// </summary>
    /// <param name="componentTypes">List of component types in arbitrary order.</param>
    public Archetype(params Type[] componentTypes)
    {
      foreach (Type componentType in componentTypes)
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
    public void AddEntity(int entity, params object[] components)
    {
      throw new NotImplementedException();
    }
  }
}
