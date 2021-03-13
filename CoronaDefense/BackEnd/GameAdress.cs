// <copyright file="GameAdress.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd
{
  using System;

  /// <summary>
  /// An unique address associated with each different game instance.
  /// </summary>
  public readonly struct GameAdress
  {
    /// <summary>
    /// The address of this <see cref="GameAdress"/>.
    /// </summary>
    public readonly long GameAdress;
    private static Random random;

    /// <summary>
    /// Initializes a new instance of the <see cref="GameAdress"/>. struct.
    /// </summary>
    public GameAdress()
    {
      this.GameAdress = random.Next() + random.Next();
    }
  }
}
