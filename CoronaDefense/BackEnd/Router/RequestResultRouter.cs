// <copyright file="RequestResultRouter.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using API.Requests;
using API.Schemas;

namespace BackEnd.Router
{
  /// <summary>
  /// Standard <see cref="Router.RequestRouter{TRequest,TResult}"/> for requests of type <see cref="LocalRequest"/> and with <see cref="RequestResult"/> output.
  /// </summary>
  /// <typeparam name="TRequest">Type of request to route.</typeparam>
  internal abstract class RequestResultRouter<TRequest> : Router.RequestRouter<TRequest, RequestResult>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="RequestResultRouter{TRequest}"/> class.
    /// </summary>
    /// <param name="router"><see cref="Router"/> whose lookup table should be utilized.</param>
    protected RequestResultRouter(Router router)
      : base(router)
    {
    }

    /// <summary>
    /// Execute <paramref name="request"/> without output.
    /// </summary>
    /// <param name="receiver"><see cref="IReceiver"/> that should execute request.</param>
    /// <param name="request"><see cref="LocalRequest"/> that should be executed.</param>
    protected abstract void ExecuteRequest(IReceiver receiver, TRequest request);

    /// <inheritdoc/>
    protected override RequestResult ForwardRequest(IReceiver receiver, TRequest request)
    {
      this.ExecuteRequest(receiver, request);

      return new RequestResult()
      {
        Success = true,
        Details = "Request to lobby was transmitted successfully. Note, this does not mean the request was carried out without problems.",
      };
    }

    /// <inheritdoc/>
    protected override RequestResult GenerateInvalidLobbyIdResult(TRequest request)
    {
      return new RequestResult()
      {
        Success = false,
        Details = InvalidLobbyIdMessage,
      };
    }
  }
}
