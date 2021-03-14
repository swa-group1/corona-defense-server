// <copyright file="APIEndpoint.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.APIEndpoint
{
  using System;

  /// <summary>
  /// Receives requests from clients through a REST-api.
  /// </summary>
  internal class APIEndpoint
  {
    /// <summary>
    /// Attach an <see cref="IObserver"/> to this <see cref="APIEndpoint"/>.
    /// </summary>
    /// <param name="observer"> The observer to add.</param>
    public void AttachObserver(IObserver observer)
    {
      throw new NotImplementedException();
    }
  }
}
