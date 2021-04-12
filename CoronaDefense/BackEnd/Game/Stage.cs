// <copyright file="Stage.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using Newtonsoft.Json;
using System.Collections.Generic;

namespace BackEnd.Game
{
  // ReSharper disable ClassNeverInstantiated.Global
  // ReSharper disable CollectionNeverUpdated.Global
  // ReSharper disable UnusedAutoPropertyAccessor.Global
  // ↑ The stage class is created with reflection, so the init setters, Tile class, and IList fields are actually in use.

  /// <summary>
  /// Data class for stages.
  /// </summary>
  internal class Stage
  {
    /// <summary>
    /// Gets a unique number of this stage.
    /// </summary>
    public int Number { get; init; }

    /// <summary>
    /// Gets the display name of this stage.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Gets the width of the path extending from the base path.
    /// </summary>
    public int PathSize { get; init; }

    /// <summary>
    /// Gets the number of tiles before the first visible tile.
    /// </summary>
    public int PathStart { get; init; }

    /// <summary>
    /// Gets the number of tiles before the first tile on the trailing end of the stage that is not visible.
    /// </summary>
    public int PathEnd { get; init; }

    /// <summary>
    /// Gets the number of tile columns in x direction.
    /// </summary>
    public int XSize { get; init; }

    /// <summary>
    /// Gets the number of tile rows in y direction.
    /// </summary>
    public int YSize { get; init; }

    /// <summary>
    /// Gets tiles that towers can not occupy.
    /// </summary>
    public IList<Tile> BlockedTiles { get; init; }

    /// <summary>
    /// Gets points that the path passes through.
    /// </summary>
    public IList<Tile> PathPoints { get; init; }

    /// <summary>
    /// Create a new <see cref="Stage"/> object.
    /// </summary>
    /// <param name="jsonContent">JSON text to parse into <see cref="Stage"/>.</param>
    /// <returns>The parsed <see cref="Stage"/>.</returns>
    public static Stage Parse(string jsonContent)
    {
      return JsonConvert.DeserializeObject<Stage>(jsonContent);
    }

    /// <summary>
    /// Reference to a specific tile on the game board.
    /// </summary>
    public class Tile
    {
      /// <summary>
      /// Gets the X coordinate of this <see cref="Tile"/>.
      /// </summary>
      public int X { get; init; }

      /// <summary>
      /// Gets the Y coordinate of this <see cref="Tile"/>.
      /// </summary>
      public int Y { get; init; }

      /// <inheritdoc/>
      public override string ToString()
      {
        return $"Tile {{ X: {this.X}, Y: {this.Y} }}";
      }
    }
  }
}
