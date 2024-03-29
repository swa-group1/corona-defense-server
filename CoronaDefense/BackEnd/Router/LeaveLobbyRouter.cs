﻿// <copyright file="LeaveLobbyRouter.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Communication.API.Requests;
using BackEnd.Communication.API.Schemas;

namespace BackEnd.Router
{
  /// <summary>
  /// <see cref="LocalRequestRouter{TRequest,TResult}"/> for requests to leave a lobby.
  /// </summary>
  /// <remarks>
  /// This object is part of the Traffic / Router Layer – Layer 2.
  /// </remarks>
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
