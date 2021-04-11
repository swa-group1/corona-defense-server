﻿// <copyright file="LocalRequestRouter.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using API.Requests;
using API.Schemas;

namespace BackEnd.Router
{
  /// <summary>
  /// Standard <see cref="Router.RequestRouter{TRequest,TResult}"/> for requests that are of a type that is a subclass of <see cref="LocalRequest"/> and with output that is a subclass of <see cref="RequestResult"/>.
  /// </summary>
  /// <typeparam name="TRequest">Local request type to route.</typeparam>
  internal abstract class LocalRequestRouter<TRequest, TResult> : RequestResultRouter<TRequest, TResult>
    where TRequest : LocalRequest
    where TResult : RequestResult, new()
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