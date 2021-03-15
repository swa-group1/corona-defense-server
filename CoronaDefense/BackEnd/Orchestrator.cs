// <copyright file="Orchestrator.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using API.APIEndpoint;
using System;

namespace BackEnd
{
  /// <summary>
  /// A class describing a back-end orchestrator that handles global client-requests not connected to a specific <see cref="ModelInstance"/>s. This includes creating new <see cref="ModelInstance"/>s.
  /// </summary>
  internal class Orchestrator : IObserver
  {
    /// <inheritdoc/>
    public void OnGlobalMessage(IGlobalMessage message)
    {
      throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public void OnLocalMessage(ILocalMessage message)
    {
      throw new NotImplementedException();
    }
  }
}
