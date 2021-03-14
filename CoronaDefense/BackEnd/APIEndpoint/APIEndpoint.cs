// <copyright file="APIEndpoint.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.APIEndpoint
{
  using System;

  /// <summary>
  /// Receives requests from clients through a REST-api and exposes such events to attached <see cref="IObserver{T}"/>s.
  /// </summary>
  internal class APIEndpoint : IAPIEndpoint
  {
    /// <summary>
    /// <para>Attach an <see cref="IObserver"/> to this <see cref="APIEndpoint"/>.</para>
    /// <para>Events generated from the REST-api will be sent through the <see cref="observer"/>.</para>
    /// </summary>
    /// <param name="observer"> The observer to add.</param>
    public void AttachObserver(IObserver observer)
    {
      throw new NotImplementedException();
    }
  }
}
