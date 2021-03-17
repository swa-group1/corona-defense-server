// <copyright file="HP.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using ECS;

namespace BackEnd.Components
{
  /// <summary>
  /// Component for health
  /// </summary>
  internal readonly struct HP : IComponent<HP>
  {
    /// <summary>
    /// Byte value of current HP (0-255).
    /// </summary>
    public readonly byte Hp;

    /// <summary>
    /// Initializes a new instance of the <see cref="HP"/> struct.
    /// </summary>
    /// <param name="hp">Initial byte value of HP.</param>
    public HP(byte hp)
    {
      this.Hp = hp;
    }

    /// <summary>
    /// Get a new <see cref="HP"> by adding a small positive integer to the amount of health points.
    /// </summary>
    /// <param name="left">Original amount of health points.</param>
    /// <param name="right">Increase HP by this value.</param>
    /// <returns> New HP with updated value.</returns>
    public static HP operator +(HP left, byte right)
    {
      if (change < 255 - Hp)
      {
        return new HP(this.Hp + hp);
      } else {
        return new HP(255);
      }
    }

    /// <summary>
    /// Get a new <see cref="HP"> by subtracting a small positive integer from the amount of health points.
    /// </summary>
    /// <param name="left">Original amount of health points.</param>
    /// <param name="right">Decrease HP by this value.</param>
    /// <returns> New HP with updated value.</returns>
    public static HP operator -(HP left, byte right)
    {
      if (this.Hp < change) {
        return new HP(0);
      } else {
        return new HP(this.Hp + change);
      }
    }

    /// <inheritdoc/>
    public int Size { get { return 1; } }

    /// <inheritdoc/>
    public byte[] ToBytes()
    {
      return new byte[] { this.Hp };
    }

    /// <inheritdoc/>
    public HP FromBytes(byte[] bytes)
    {
      return new HP(bytes[0]);
    }
  }
}
