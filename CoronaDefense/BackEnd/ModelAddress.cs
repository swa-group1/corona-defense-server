// <copyright file="ModelAddress.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd
{
  using System;

  /// <summary>
  /// An unique address associated with each different <see cref="ModelInstance"/>.
  /// </summary>
  public readonly struct ModelAddress
  {
    /// <summary>
    /// Internal backing generator.
    /// </summary>
    private static readonly Random random = new Random();

    /// <summary>
    /// Create a new random <see cref="ModelAddress"/>.
    /// </summary>
    /// <returns>The generated <see cref="ModelAddress"/>.</returns>
    public static ModelAddress GetRandom()
    {
      byte[] buffer = new byte[8];
      random.NextBytes(buffer);
      long address = BitConverter.ToInt64(buffer, 0);
      return new ModelAddress(address);
    }

    /// <summary>
    /// The address of this <see cref="ModelAddress"/>.
    /// </summary>
    public readonly long Address;

    private ModelAddress(long address)
    {
      this.Address = address;
    }
  }
}
