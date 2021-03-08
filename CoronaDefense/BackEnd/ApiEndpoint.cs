// <copyright file="ApiEndpoint.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd
{
  using System;

  /// <summary>
  /// Handles requests from clients.
  /// </summary>
  internal class ApiEndpoint
  {
    /// <summary>
    /// Attach an observer to the BackEnd endpoint.
    /// </summary>
    /// <param name="observer"> The observer to add.</param>
    public void AttachObserver(Observer observer)
    {
      throw new NotImplementedException("Not implemented");
    }

    /// <summary>
    /// Notify all attached observers.
    /// </summary>
    public void NotifyObservers()
    {
    }

    /// <summary>
    /// Observers of the ApiEndpoint.
    /// </summary>
    internal abstract class Observer
    {
      /// <summary>
      /// What to do when notified by the ApiEndpoint.
      /// </summary>
      public abstract void GetNotified();
    }
  }
}
