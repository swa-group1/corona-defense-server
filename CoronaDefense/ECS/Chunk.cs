// <copyright file="Chunk.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace ECS
{
  /// <summary>
  /// Collection of components of the same type.
  /// </summary>
  public class Chunk
  {
    /// <summary>
    /// Component to be used as template for this <see cref="Chunk"/>. The component type is extracted from this component.
    /// </summary>
    private readonly IComponent sampleComponent;

    /// <summary>
    /// Gets <see cref="IComponent"/>, in the form of a sequence of bytes, currently stored in this <see cref="Chunk"/>.
    /// </summary>
    public List<byte> Components { get; }

    /// <summary>
    /// Gets size in bytes of the type of <see cref="IComponent"/>s saved in this <see cref="Chunk"/>.
    /// </summary>
    public int ComponentSize { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Chunk"/> class.
    /// </summary>
    /// <param name="component">Initial <see cref="IComponent"/> of <see cref="Chunk"/> to extract type of component saved from.</param>
    public Chunk(IComponent component)
    {
      this.sampleComponent = component;
      this.Components = new List<byte>(component.ToBytes());
      this.ComponentSize = component.Size;
    }

    /// <summary>
    /// Create a <see cref="IComponent"/> of the type stored in this <see cref="Chunk"/> from supplied <see cref="byte"/>s.
    /// </summary>
    /// <param name="bytes">A array of bytes that represent the <see cref="IComponent"/> to be created or realized and whose layout and size depend on the type of component.</param>
    /// <returns>The realized <see cref="IComponent"/>.</returns>
    public IComponent FromBytes(byte[] bytes)
    {
      return this.sampleComponent.FromBytes(bytes);
    }
  }
}
