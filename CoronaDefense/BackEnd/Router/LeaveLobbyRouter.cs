// <copyright file="LeaveLobbyRouter.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using API.Requests;
using API.Schemas;

namespace BackEnd.Router
{
  /// <summary>
  /// <see cref="LocalRequestRouter"/> for requests to leave a lobby.
  /// </summary>
  internal class LeaveLobbyRouter : LocalRequestRouter<LocalRequest, RequestResult>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="LeaveLobbyRouter"/> class.
    /// </summary>
    /// <param name="router"><see cref="Router"/> whose lookup table should be utilized.</param>
    public LeaveLobbyRouter(Router router)
      : base(router)
    {
    }

    /// <inheritdoc/>
    protected override void ExecuteRequest(IReceiver receiver, LocalRequest request)
    {
      receiver.LeaveLobby(request);
    }
  }
}
