// <copyright file="Orchestrator.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd
{
  /// <summary>
  /// A class describing a back-end orchestrator that handles global client-requests not connected to <see cref="ModelInstance"/>s. This includes creating new <see cref="ModelInstance"/>s.
  /// </summary>
  internal class Orchestrator : ApiEndpoint.IObserver
  {
    /// <inheritdoc />
    void ApiEndpoint.IObserver.GetNotified()
    {
      throw new System.NotImplementedException();
    }
  }
}
