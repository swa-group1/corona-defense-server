// <copyright file="ApiEndpoint.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd
{
  using System;

  /// <summary>
  /// Receives requests from clients.
  /// </summary>
  internal partial class ApiEndpoint
  {
    /// <summary>
    /// Attach an observer to the back-end endpoint.
    /// </summary>
    /// <param name="observer"> The observer to add.</param>
    internal void AttachObserver(Observer observer)
    {
      throw new NotImplementedException("Not implemented");
    }

    /// <summary>
    /// Notify all attached observers.
    /// </summary>
    internal void NotifyObservers()
    {
    }
  }
}
