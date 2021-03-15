using System;
using System.Collections.Generic;
using System.Linq;

namespace ECS
{
  /// <summary>
  /// Category intended for data <see langword="struct"/>s used in <see cref="ECS"/>.
  /// </summary>
  public interface IComponent
  {
  }

  /// <summary>
  /// Extension methods for usage of <see cref="IComponent"/>.
  /// </summary>
  public static class IComponentExtension
  {
    /// <summary>
    /// Create an <see cref="IEnumerable{T}"/> with the types of the supplied <paramref name="components"/>.
    /// </summary>
    /// <param name="components"><see cref="IComponent"/>s to get <see cref="Type"/>s of.</param>
    /// <returns>Types of <paramref name="components"/>.</returns>
    public static IEnumerable<Type> GetTypes(this IEnumerable<IComponent> components)
    {
      return components.Select(delegate(IComponent component) { return component.GetType(); });
    }
  }
}
