using System;
using System.Collections.Generic;
using System.Linq;

namespace ECS
{
  /// <summary>
  /// Category intended for data <see langword="struct"/>s used in <see cref="ECS"/>.
  /// </summary>
  public interface IComponent<T>
    where T : IComponent<T>
  {
    /// <summary>
    /// How many <see langword="byte"/>s the component takes up.
    /// </summary>
    /// <remarks>
    /// Two instances of the same type should never return different sizes.
    /// </remarks>
    int Size { get; }
    
    /// <summary>
    /// Converts the component to an array of <see langword="byte"/>s to be stored in an archetype.
    /// </summary>
    byte[] ToBytes();
    
    /// <summary>
    /// Converts an array of bytes with length <see cref="Size"/> to an instance of type (TODO: type ref)
    /// </summary>
    /// <remarks>
    /// It should be assumed that the incoming <see langword="byte"/> array is of length <see cref="Size"/>; this will be handled by <see cref="Archetype"/>.
    /// </remarks>
    T FromBytes(byte[] bytes);
  }
}
