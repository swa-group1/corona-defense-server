// <copyright file="Orchestrator.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd
{
  /// <summary>
  /// A class describing a back-end orchestrator that handles global client-requests not connected to <see cref="ModelInstance"/>s. This includes creating new <see cref="ModelInstance"/>s.
  /// </summary>
  internal class Orchestrator : APIEndpoint.IObserver
  {
    /// <inheritdoc/>
    public void OnGlobalMessage(APIEndpoint.IGlobalMessage message)
    {
      throw new System.NotImplementedException();
    }

    /// <inheritdoc/>
    public void OnLocalMessage(APIEndpoint.ILocalMessage message)
    {
      throw new System.NotImplementedException();
    }
  }
}
