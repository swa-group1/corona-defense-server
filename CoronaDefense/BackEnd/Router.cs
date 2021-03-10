// <copyright file="Router.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd
{
  /// <summary>
  /// Class with objects that route messages from <see cref="ApiEndpoint"/> to <see cref="Router.IReceiver"/>s.
  /// </summary>
  internal partial class Router : ApiEndpoint.IObserver
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Router"/> class.
    /// </summary>
    /// <param name="api"> The Api Endpoint to connect to. </param>
    public Router(ApiEndpoint api)
    {
      api.AttachObserver(this);
    }

    /// <inheritdoc/>
    void ApiEndpoint.IObserver.GetNotified()
    {
      throw new System.NotImplementedException();
    }
    
    /// <summary>
    /// Register <see cref="IReceiver"/> as a possible destination for messages routed through it.
    /// </summary>
    /// <param name="receiver">Receiver to register.</param>
    public void Register(IReceiver receiver)
    {
      throw new System.NotImplementedException();
    }
  }
}
