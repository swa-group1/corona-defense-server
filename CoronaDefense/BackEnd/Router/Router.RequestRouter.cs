// <copyright file="Router.RequestRouter.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Communication.API;

namespace BackEnd.Router
{
  internal partial class Router
  {
    /// <summary>
    /// Object that routes requests of a certain type for a <see cref="Router"/>.
    /// </summary>
    /// <remarks>
    /// This object is part of the Traffic / Router Layer – Layer 2.
    /// </remarks>
    /// <typeparam name="TRequest">Type of request to route.</typeparam>
    /// <typeparam name="TResult">Type of output endpoint expects.</typeparam>
    internal abstract class RequestRouter<TRequest, TResult> : IRequestHandler<TRequest, TResult>
    {
      /// <summary>
      /// Standard message to display when a lobby with supplied lobby ID was not found.
      /// </summary>
      protected const string InvalidLobbyIdMessage = "Lobby with lobby ID was not found.";

      private Router Router { get; }

      /// <summary>
      /// Initializes a new instance of the <see cref="RequestRouter{TRequest, TResult}"/> class.
      /// </summary>
      /// <param name="router"><see cref="Router"/> whose lookup table should be utilized.</param>
      protected RequestRouter(Router router)
      {
        this.Router = router;
      }

      /// <summary>
      /// Extract a lobby ID from the supplied <paramref name="request"/>.
      /// </summary>
      /// <param name="request">Request to extract lobby ID from.</param>
      /// <returns>The lobby ID.</returns>
      protected abstract long ExtractLobbyID(TRequest request);

      /// <summary>
      /// Execute <paramref name="request"/>, conserving output.
      /// </summary>
      /// <param name="receiver"><see cref="IReceiver"/> that request should be forwarded to.</param>
      /// <param name="request">Request that should be executed.</param>
      /// <returns>Result from receiver.</returns>
      protected abstract TResult ForwardRequest(IReceiver receiver, TRequest request);

      /// <inheritdoc/>
      TResult IRequestHandler<TRequest, TResult>.ProcessRequest(TRequest request)
      {
        long lobbyId = this.ExtractLobbyID(request);
        if (!this.Router.receivers.TryGetValue(lobbyId, out IReceiver receiver))
        {
          return this.GenerateInvalidLobbyIdResult(request);
        }

        return this.ForwardRequest(receiver, request);
      }

      /// <summary>
      /// Generate a result indicating that the lobby ID was not found to be associated with any lobbies.
      /// </summary>
      /// <remarks>
      /// It is recommended to make us of the <see cref="InvalidLobbyIdMessage"/> constant.
      /// </remarks>
      /// <param name="request">Request that has invalid lobby ID.</param>
      /// <returns>A result that indicates that the <paramref name="request"/> had invalid lobby ID.</returns>
      protected abstract TResult GenerateInvalidLobbyIdResult(TRequest request);
    }
  }
}
