// <copyright file="HP.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using ECS;

namespace BackEnd.Components
{
  /// <summary>
  /// Component for health.
  /// </summary>
  internal readonly struct HP : IComponent
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
    /// Get a new <see cref="HP"/> by adding a small positive integer to the amount of health points.
    /// </summary>
    /// <param name="left">Original amount of health points.</param>
    /// <param name="right">Increase HP by this value.</param>
    /// <returns> New HP with updated value.</returns>
    public static HP operator +(HP left, byte right)
    {
      if (right < 255 - left.Hp)
      {
        return new HP((byte)(left.Hp + right));
      }

      return new HP(255);
    }

    /// <summary>
    /// Get a new <see cref="HP"/> by subtracting a small positive integer from the amount of health points.
    /// </summary>
    /// <param name="left">Original amount of health points.</param>
    /// <param name="right">Decrease HP by this value.</param>
    /// <returns> New HP with updated value.</returns>
    public static HP operator -(HP left, byte right)
    {
      if (left.Hp < right)
      {
        return new HP(0);
      }

      return new HP((byte)(left.Hp + right));
    }

    /// <inheritdoc/>
    public int Size
    {
      get { return 1; }
    }

    /// <inheritdoc/>
    public byte[] ToBytes()
    {
      return new byte[] { this.Hp };
    }

    /// <inheritdoc/>
    public IComponent FromBytes(byte[] bytes)
    {
      return new HP(bytes[0]);
    }
  }
}
