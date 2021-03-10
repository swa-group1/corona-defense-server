// <copyright file="Worker.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.ECS
{
  /// <summary>
  /// System that operates on data in <see cref="ECS"/>.
  /// </summary>
  /// <remarks>Name of class is <see cref="Worker"/>, not system, to avoid name conflict with .NET <see cref="System"/>.</remarks>
  internal abstract class Worker
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Worker"/> class.
    /// </summary>
    protected Worker()
    {
    }
  }
}
