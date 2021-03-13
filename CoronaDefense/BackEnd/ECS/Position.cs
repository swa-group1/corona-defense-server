// <copyright file="Position.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.ECS
{
  internal readonly struct Position
  {
    /// <summary>
    /// X-coordinate of position.
    /// </summary>
    public readonly int X;

    /// <summary>
    /// Y-coordinate of position.
    /// </summary>
    public readonly int Y;

    /// <summary>
    /// Initializes a new instance of the <see cref="Position"/> struct.
    /// </summary>
    /// <param name="x">X-coordinate. </param>
    /// <param name="y">Y-coordinate. </param>
    public Position(int x, int y)
    {
      this.X = x;
      this.Y = y;
    }

    /// <summary>
    /// Get a new position based on the old one.
    /// </summary>
    /// <param name="x"> Move x parameter by. </param>
    /// <param name="y">Move y parameter by. </param>
    /// <returns>New position. </returns>
    public Position Move(int x, int y)
    {
      return new Position(this.X + x, this.Y + y);
    }
  }
}
