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
    /// TODO
    /// </summary>
    private List<double> CumulativePathLengths { get; } = new List<double>();

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
    private IList<Point> pathPoints;

    /// <summary>
    /// Gets points that the path passes through.
    /// </summary>
    public IList<Point> PathPoints
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
      this.CumulativePathLengths.Clear();
      this.LineSegmentsX.Clear();
      this.LineSegmentsY.Clear();

      // Adding edge-case logic for requesting a point before the path.
      this.CumulativePathLengths.Add(0d);
      this.LineSegmentsX.Add(AffineLine.Constant(this.PathPoints[0].X));
      this.LineSegmentsY.Add(AffineLine.Constant(this.PathPoints[0].Y));

      double cumulativePathLength = 0d;
      for (int i = 1; i < this.PathPoints.Count; i++)
      {
        Point first = this.PathPoints[i - 1];
        Point second = this.PathPoints[i];
        double deltaX = second.X - first.X;
        double deltaY = second.Y - first.Y;
        double newCumulativePathLength = cumulativePathLength + Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));

        this.LineSegmentsX.Add(new AffineLine(cumulativePathLength, first.X, newCumulativePathLength, second.X));
        this.LineSegmentsY.Add(new AffineLine(cumulativePathLength, first.Y, newCumulativePathLength, second.Y));

        cumulativePathLength = newCumulativePathLength;
        this.CumulativePathLengths.Add(cumulativePathLength);
      }

      // Adding edge-case logic for requesting a point after path.
      this.CumulativePathLengths.Add(double.MaxValue);
      this.LineSegmentsX.Add(AffineLine.Constant(this.PathPoints[this.PathPoints.Count - 1].X));
      this.LineSegmentsY.Add(AffineLine.Constant(this.PathPoints[this.PathPoints.Count - 1].Y));
    }

    public Point GetPointAlongPath(double length)
    {
      int insertIndex = this.CumulativePathLengths.BinarySearch(length);
      insertIndex = insertIndex < 0 ? ~insertIndex : insertIndex;
      return new Point()
      {
        X = this.LineSegmentsX[insertIndex].Evaluate(length),
        Y = this.LineSegmentsY[insertIndex].Evaluate(length),
      };
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

    /// <summary>
    /// Affine line segment.
    /// </summary>
    private class AffineLine
    {
      /// <summary>
      /// Gets the slope value of line.
      /// </summary>
      private double A { get; init; }

      /// <summary>
      /// Gets the contant value of line.
      /// </summary>
      private double B { get; init; }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="pathLength0"></param>
      /// <param name="value0"></param>
      /// <param name="pathLength1"></param>
      /// <param name="value1"></param>

      // TODO
      private AffineLine()
      {
      }

      // TODO
      public AffineLine(double pathLength0, double value0, double pathLength1, double value1)
      {
        this.A = (value1 - value0) / (pathLength1 - pathLength0);
        this.B = value0 - this.A * pathLength0; 
      }

      public static AffineLine Constant(double contant)
      {
        return new AffineLine()
        {
          A = 0,
          B = contant,
        };
      }

      /// <summary>
      /// Evaluate the value at a certain point.
      /// <summary>
      /// <param name="step">The step value to evaluate.</param>
      public double Evaluate(double length)
      {
        return this.A * length + this.B;
      }
    }

    /// <summary>
    /// reference to a specific point on the game board.
    /// </summary>
    public class Point 
    {
      /// <summary>
      /// Gets the X coordinate of this <see cref="Point"/>.
      /// </summary>
      public double X { get; init; }

      /// <summary>
      /// Gets the Y coordinate of this <see cref="Point"/>.
      /// </summary>
      public double Y { get; init; }

       /// <inheritdoc/>
      public override string ToString()
      {
        return $"Point {{ X: {this.X}, Y: {this.Y} }}";
      }

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
