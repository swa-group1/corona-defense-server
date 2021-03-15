// <copyright file="BoardPosition.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.Components
{
  using ECS.IComponent;

  /// <summary>
  /// Data <see langword="struct"/> that represents a position on the board.
  /// </summary>
  internal readonly struct BoardPosition : IComponent
  {
    /// <summary>
    /// X-coordinate of this <see cref="BoardPosition"/>.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public readonly int X;

    /// <summary>
    /// Y-coordinate of this <see cref="BoardPosition"/>.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public readonly int Y;

    /// <summary>
    /// Initializes a new instance of the <see cref="BoardPosition"/> struct.
    /// </summary>
    /// <param name="x">X-coordinate.</param>
    /// <param name="y">Y-coordinate.</param>
    public BoardPosition(int x, int y)
    {
      this.X = x;
      this.Y = y;
    }

    /// <summary>
    /// Get a new <see cref="BoardPosition"/> based on the old one.
    /// </summary>
    /// <param name="x"> Move x parameter by.</param>
    /// <param name="y">Move y parameter by.</param>
    /// <returns>New position. </returns>
    public BoardPosition Move(int x, int y)
    {
      return new BoardPosition(this.X + x, this.Y + y);
    }
  }
}
