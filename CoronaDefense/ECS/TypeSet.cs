// <copyright file="TypeSet.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace ECS
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Linq;

  /// <summary>
  /// Set of types
  /// </summary>
  internal class TypeSet : IEnumerable<Type>
  {
    private readonly HashSet<Type> types;

    /// <summary>
    /// Initializes a new instance of the <see cref="TypeSet"/> class.
    /// </summary>
    /// <param name="types">List of types to be part of new <see cref="TypeSet"/>. Duplicates are removed.</param>
    public TypeSet(params Type[] types)
    {
      this.types = new HashSet<Type>(types);
    }

    /// <inheritdoc/>
    /// <remarks>This implementation is relatively costly.</remarks>
    public override bool Equals(object obj)
    {
      if (obj == null)
      {
        return false;
      }

      if (obj is not TypeSet other)
      {
        return false;
      }

      return this.types.SetEquals(other.types);
    }

    /// <inheritdoc/>
    public IEnumerator<Type> GetEnumerator()
    {
      return ((IEnumerable<Type>)this.types).GetEnumerator();
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.types.GetEnumerator();
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return this.types.Aggregate(
        0,
        delegate(int current, Type type) { return current ^ type.GetHashCode(); }
      );
    }

    /// <inheritdoc/>
    public override string ToString()
    {
      return $"{nameof(TypeSet)} {{ {string.Join<Type>(", ", this.types)} }}";
    }
  }
}
