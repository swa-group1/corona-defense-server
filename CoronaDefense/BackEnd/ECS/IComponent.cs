// <copyright file="IComponent.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.ECS
{
  /// <summary>
  /// Interface for components in <see cref="ECS"/>.
  /// </summary>
  public interface IComponent
  {
    /// <summary>
    /// Gets the unique name of this component type.
    /// </summary>
    string TypeName { get; }
  }
}
