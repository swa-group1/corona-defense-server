using System;
using System.Collections.Generic;
using System.Linq;

namespace ECS
{
  /// <summary>
  /// Interface for components in <see cref="ECS"/>.
  /// </summary>
  public interface IComponent
  {
    /// <summary>
    /// Gets how many <see langword="byte"/>s the component takes up.
    /// </summary>
    /// <remarks>
    /// Two instances of the same type should never return different sizes.
    /// </remarks>
    int Size { get; }

    /// <summary>
    /// Converts the component to an array of <see langword="byte"/>s to be stored in an archetype.
    /// </summary>
    /// <returns>This <see cref="IComponent"/> encoded in a <see langword="byte"/> array.</returns>
    byte[] ToBytes();

    /// <summary>
    /// Converts an array of <see langword="byte"/>s that always has length <see cref="Size"/> to an <see cref="IComponent"/> of the same type.
    /// </summary>
    /// <param name="bytes">Input to convert into a <see cref="IComponent"/>.</param>
    /// <returns>A <see cref="IComponent"/> with the same type as this <see cref="IComponent"/>.</returns>
    IComponent FromBytes(byte[] bytes);
  }

  public static class IComponentExtension
  {
    public static IEnumerable<Type> GetTypes(this IEnumerable<IComponent> components)
    {
      foreach (IComponent component in components)
      {
        yield return component.GetType();
      }
    }
  }
}
