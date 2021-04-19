// <copyright file="LocalRequestRouter.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Communication.API.Requests;
using BackEnd.Communication.API.Schemas;

namespace BackEnd.Router
{
  /// <summary>
  /// Standard <see cref="Router.RequestRouter{TRequest,TResult}"/> for requests that are of a type that is a subclass of <see cref="LocalRequest"/> and with output that is a subclass of <see cref="RequestResult"/>.
  /// </summary>
  /// <remarks>
  /// This object is part of the Traffic / Router Layer – Layer 2.
  /// </remarks>
  /// <typeparam name="TRequest">Local request type to route.</typeparam>
  /// <typeparam name="TResult">Type of result of request.</typeparam>
  internal abstract class LocalRequestRouter<TRequest, TResult> : RequestResultRouter<TRequest, TResult>
    where TRequest : LocalRequest
    where TResult : RequestResult, new()
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="LocalRequestRouter{TRequest,TResult}"/> class.
    /// </summary>
    /// <param name="router"><see cref="Router"/> whose lookup table should be utilized.</param>
    protected LocalRequestRouter(Router router)
      : base(router)
    {
    }

    /// <inheritdoc/>
    protected override long ExtractLobbyID(TRequest request)
    {
      return request.LobbyId;
    }
  }
}
