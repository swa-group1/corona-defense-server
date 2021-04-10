// <copyright file="LocalRequestRouter.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using API.Requests;
using API.Schemas;

namespace BackEnd.Router
{
  /// <summary>
  /// Standard <see cref="Router.RequestRouter{TRequest,TResult}"/> for requests of type <see cref="LocalRequest"/> and with <see cref="RequestResult"/> output.
  /// </summary>
  internal abstract class LocalRequestRouter : LocalRequestRouter<LocalRequest>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="LocalRequestRouter"/> class.
    /// </summary>
    /// <param name="router"><see cref="Router"/> whose lookup table should be utilized.</param>
    protected LocalRequestRouter(Router router)
      : base(router)
    {
    }
  }

  /// <summary>
  /// Standard <see cref="Router.RequestRouter{TRequest,TResult}"/> for requests that are of a type that is a subclass to <see cref="LocalRequest"/> and with <see cref="RequestResult"/> output.
  /// </summary>
  /// <typeparam name="TRequest">Local request type to route.</typeparam>
  internal abstract class LocalRequestRouter<TRequest> : RequestResultRouter<TRequest>
    where TRequest : LocalRequest
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="LocalRequestRouter{TRequest}"/> class.
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
