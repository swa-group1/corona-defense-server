// <copyright file="ProjectedHealthComponent.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.Game.Components
{
  /// <summary>
  /// Component containing a number of health points.
  /// </summary>
  /// <remarks>
  /// This component should never have the value 0. Instead, the lack of the component altogether symbolizes this.
  /// </remarks>
  internal struct ProjectedHealthComponent
  {
    /// <summary>
    /// Projected number of health points after all projectiles in the air have hit.
    /// </summary>
    public int ProjectedHealthPoints;
  }
}
