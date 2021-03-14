// <copyright file="Service.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace ECS
{
  /// <summary>
  /// System that operates on data in <see cref="ECS"/>.
  /// </summary>
  /// <remarks>Name of class is <see cref="Service"/>, not system, to avoid name conflict with .NET <see cref="System"/>.</remarks>
  internal abstract class Service
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Service"/> class.
    /// </summary>
    protected Service()
    {
    }
  }
}
