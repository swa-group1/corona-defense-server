// <copyright file="IAPIEndpoint.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace API.APIEndpoint
{
  /// <summary>
  /// Object that <see cref="IObserver"/>s can attach to. The main implementation of this interface, <see cref="APIEndpoint"/> also receives request from the internet and sends them to <see cref="IObserver"/>s.
  /// </summary>
  public interface IAPIEndpoint
  {
    /// <summary>
    /// Attach an <see cref="IObserver"/> to this <see cref="IAPIEndpoint"/>.
    /// </summary>
    /// <param name="observer"> The observer to add.</param>
    void AttachObserver(IObserver observer);
  }
}
