// <copyright file="ServerRandom.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using System;

namespace BackEnd
{
  /// <summary>
  /// Class of objects that intent to use random numbers server-side in a secure manner. Without a central random number generator, different parts of the server creating their <see cref="System.Random"/> objects at the same time would result in perfectly synced up numbers, which is a huge privacy concern.
  /// </summary>
  public class ServerRandom
  {
    /// <summary>
    /// Gets the shared backing-generator.
    /// </summary>
    private static Random Random { get; } = new Random();

    /// <summary>
    /// Gets a random <see cref="long"/>.
    /// </summary>
    protected static long RandomLong
    {
      get
      {
        byte[] buffer = new byte[8];
        Random.NextBytes(buffer);
        long address = BitConverter.ToInt64(buffer, 0);
        return address;
      }
    }
  }
}
