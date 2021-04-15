// <copyright file="Stage.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using Newtonsoft.Json;
using System;
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
    /// Constant with half the number of steps in one tile.
    /// </summary>
    private const int HalfOfStepsInTile = 10;

    /// <summary>
    /// The number of steps in one tile.
    /// </summary>
    private static int StepsInTile = 2 * HalfOfStepsInTile;

    /// <summary>
    /// Gets the cumulative number of steps until the start of each line segment.
    /// </summary>
    private List<int> CumulativeStepsPerLineSegment { get; } = new List<int>();

    /// <summary>
    /// Gets the affine functions for X-values in line segments from the described path.
    /// </summary>
    private List<AffineLine> LineSegmentsX { get; } = new List<AffineLine>();

    /// <summary>
    /// Gets the affine functions for Y-values in line segments from the described path.
    /// </summary>
    private List<AffineLine> LineSegmentsY { get; } = new List<AffineLine>();

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
    /// Backing-field of <see cref="PathPoints"/>.
    /// </summary>
    private IList<Tile> pathPoints;

    /// <summary>
    /// Gets points that the path passes through.
    /// </summary>
    public IList<Tile> PathPoints
    {
      get
      {
        return this.pathPoints;
      }

      init
      {
        this.pathPoints = value;
        this.CalculatePath();
      }
    }

    /// <summary>
    /// Process path and create caches so frequent calls to some <see cref="Stage"/> methods can be executed faster.
    /// </summary>
    /// <remarks>
    /// This is only supposed to be called when <see cref="PathPoints"/> is set.
    /// </remarks>
    private void CalculatePath()
    {
      this.CumulativeStepsPerLineSegment.Clear();
      this.LineSegmentsX.Clear();
      this.LineSegmentsY.Clear();

      List<CardinalDirection> directions = new List<CardinalDirection>();
      List<int> lengths = new List<int>();
      List<bool> positivities = new List<bool>();

      for (int i = 1; i < this.PathPoints.Count; i++)
      {
        Tile first = this.PathPoints[i - 1];
        Tile second = this.PathPoints[i];

        int deltaX = second.X - first.X;
        int deltaY = second.Y - first.Y;

        // Ensure path line moves only in x or y direction.
        if (deltaX * deltaY != 0)
        {
          throw new NotSupportedException("Stages with paths that do not follow the grid lines are not supported.");
        }

        int sum = deltaX + deltaX;

        // Check that not both deltas are zero.
        if (sum == 0)
        {
          // When both are zero, the two path points are identical and this section can be skipped.
          this.PathPoints.RemoveAt(i);
          i--;
          continue;
        }

        if (deltaX != 0)
        {
          // Movement is along the X-axis.
          directions.Add(0 < deltaX ? CardinalDirection.East : CardinalDirection.West);
        }
        else
        {
          // Movement is along the Y-axis.
          directions.Add(0 < deltaY ? CardinalDirection.North : CardinalDirection.South);
        }

        lengths.Add(Math.Abs(sum));
        positivities.Add(0 < sum);
      }

      directions.Add(Reverse(directions[directions.Count - 1]));
      lengths.Add(0);
      positivities.Add(!positivities[positivities.Count - 1]);

      int HalfOfStepsInBend = HalfOfStepsInTile * this.PathSize;

      int cumulativeSteps = 0;

      CardinalDirection previousDirection = Reverse(directions[0]);
      (double x, double y) previousPoint;
      bool previousPositivity = !positivities[0];
      for (int i = 0, max = directions.Count - 1; i < max; i++)
      {
        int steps = StepsInTile * lengths[i];

        (double x, double y) firstPoint;
        if (Reverse(previousDirection) == directions[i] || previousDirection == directions[i])
        {
          // Turn or continuation
          firstPoint = Offset(this.pathPoints[i], HalfOfStepsInBend, PositiveNormal(directions[i]));
        }
        else
        {
          // Must be bend
          firstPoint = Offset(this.pathPoints[i], HalfOfStepsInBend, CardinalDirection.East, CardinalDirection.North);
          steps += positivities[i] ? -HalfOfStepsInBend : HalfOfStepsInBend;
        }

        this.CumulativeStepsPerLineSegment.Add(cumulativeSteps);
        this.LineSegmentsX.Add(new AffineLine(cumulativeSteps, this.pathPoints[i - 1].X, nextCumulativeSteps, this.pathPoints[i].X));
        this.LineSegmentsY.Add(new AffineLine(cumulativeSteps, this.pathPoints[i - 1].Y, nextCumulativeSteps, this.pathPoints[i].Y));

        previousDirection = directions[i];
        previousPositivity = positivities[i];
      }

      return;

      this.CumulativeStepsPerLineSegment.Add(0);
      this.LineSegmentsX.Add(new AffineLine(0, 0, 1, 0));
      this.LineSegmentsY.Add(new AffineLine(0, 0, 1, 0));

      for (int i = 1; i < this.PathPoints.Count; i++)
      {
        int deltaX = this.PathPoints[i].X - this.PathPoints[i - 1].X;
        int deltaY = this.PathPoints[i].Y - this.PathPoints[i - 1].Y;

        

        // Check that not both deltas are zero.
        if (sum == 0)
        {
          // When both are zero, the two path points are identical and this section can be skipped.
          continue;
        }

        int nextCumulativeSteps = cumulativeSteps + sum * StepsInTile + (0 < sum ? HalfOfStepsInTile : -HalfOfStepsInTile) * this.PathSize;
        this.CumulativeStepsPerLineSegment.Add(cumulativeSteps);

        double halfTile = 0.5d * this.PathSize;
        this.LineSegmentsX.Add(new AffineLine(cumulativeSteps, this.pathPoints[i - 1].X, nextCumulativeSteps, this.pathPoints[i].X));
        this.LineSegmentsY.Add(new AffineLine(cumulativeSteps, this.pathPoints[i - 1].Y, nextCumulativeSteps, this.pathPoints[i].Y));
      }
    }

    // TODO
    private static (double x, double y) Offset(Tile tile, double magnitude, params CardinalDirection[] directions)
    {
      double x = tile.X;
      double y = tile.Y;
      foreach (CardinalDirection direction in directions)
      {
        switch (direction)
        {
        case CardinalDirection.East:
          x += magnitude;
          break;
        case CardinalDirection.North:
          y += magnitude;
          break;
        case CardinalDirection.South:
          y -= magnitude;
          break;
        case CardinalDirection.West:
          x -= magnitude;
          break;
        default:
          break;
        }
      }

      return (x, y);
    }

    /// <summary>
    /// Create a new <see cref="Stage"/> object.
    /// </summary>
    /// <param name="jsonContent">JSON text to parse into <see cref="Stage"/>.</param>
    /// <returns>The parsed <see cref="Stage"/>.</returns>
    public static Stage Parse(string jsonContent)
    {
      return JsonConvert.DeserializeObject<Stage>(jsonContent);
    }

    // TODO
    private static CardinalDirection PositiveNormal(CardinalDirection direction)
    {
      switch (direction)
      {
        case CardinalDirection.East:
          return CardinalDirection.North;
        case CardinalDirection.North:
          return CardinalDirection.East;
        case CardinalDirection.South:
          return CardinalDirection.East;
        case CardinalDirection.West:
          return CardinalDirection.North;
        default:
          return CardinalDirection.North;
      }
    }

    // TODO
    private static CardinalDirection Reverse(CardinalDirection direction)
    {
      switch (direction)
      {
        case CardinalDirection.East:
          return CardinalDirection.West;
        case CardinalDirection.North:
          return CardinalDirection.South;
        case CardinalDirection.South:
          return CardinalDirection.North;
        case CardinalDirection.West:
          return CardinalDirection.East;
        default:
          return CardinalDirection.North;
      }
    }

    /// <summary>
    /// Affine line segment. 
    /// </summary>
    private class AffineLine
    {
      /// <summary>
      /// Gets the slope value of line.
      /// </summary>
      private double A { get; }

      /// <summary>
      /// Gets the contant value of line.
      /// </summary>
      private double B { get; }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="step0"></param>
      /// <param name="value0"></param>
      /// <param name="step1"></param>
      /// <param name="value1"></param>
      public AffineLine(int step0, double value0, int step1, double value1)
      {
        this.A = (value1 - value0) / (step1 - step0);
        this.B = value0 - this.A * step0; 
      }

      /// <summary>
      /// Evaluate the value at a certain point.
      /// <summary>
      /// <param name="step">The step value to evaluate.</param>
      public double Evaluate(int step)
      {
        return this.A * step + this.B;
      }
    }

    private enum CardinalDirection
    {
      East,
      North,
      South,
      West,
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
