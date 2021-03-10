// <copyright file="Router.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd
{
  /// <summary>
  /// Class with objects that route messages from <see cref="ApiEndpoint"/> to <see cref="ModelInstance"/>s.
  /// </summary>
  internal class Router : ApiEndpoint.IObserver
  {
    /// <inheritdoc/>
    void ApiEndpoint.IObserver.GetNotified()
    {
      throw new System.NotImplementedException();
    }
  }
}
