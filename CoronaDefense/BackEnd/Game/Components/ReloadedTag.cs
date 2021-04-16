// <copyright file="ReloadedTag.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using Leopotam.Ecs;

namespace BackEnd.Game.Components
{
  /// <summary>
  /// Tag used to indicate that a tower has finished reloading.
  /// </summary>
  internal struct ReloadedTag : IEcsIgnoreInFilter
  {
  }
}
