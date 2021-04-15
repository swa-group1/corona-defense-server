// <copyright file="JoinLobbyRouter.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using API.Requests;
using API.Schemas;

namespace BackEnd.Router
{
  /// <summary>
  /// <see cref="BackEnd.Router.Router.RequestRouter{TRequest,TResult}"/> for requests to join a lobby.
  /// </summary>
  internal class JoinLobbyRouter : Router.RequestRouter<JoinLobbyRequest, JoinLobbyResult>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="JoinLobbyRouter"/> class.
    /// </summary>
    /// <param name="router"><see cref="Router"/> whose lookup table should be utilized.</param>
    public JoinLobbyRouter(Router router)
      : base(router)
    {
    }

    /// <inheritdoc/>
    protected override long ExtractLobbyID(JoinLobbyRequest request)
    {
      return request.LobbyId;
    }

    /// <inheritdoc/>
    protected override JoinLobbyResult ForwardRequest(IReceiver receiver, JoinLobbyRequest request)
    {
      return receiver.JoinLobby(request);
    }

    /// <inheritdoc/>
    protected override JoinLobbyResult GenerateInvalidLobbyIdResult(JoinLobbyRequest request)
    {
      return new JoinLobbyResult()
      {
        Success = false,
        Details = InvalidLobbyIdMessage,

        AccessToken = 0,
        LobbyId = request.LobbyId,
      };
    }
  }
}
