// <copyright file="StartGameRouter.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Communication.API.Requests;
using BackEnd.Communication.API.Schemas;

namespace BackEnd.Router
{
  /// <summary>
  /// <see cref="LocalRequestRouter"/> for requests to start a game.
  /// </summary>
  /// <remarks>
  /// This object is part of the Traffic / Router Layer � Layer 2.
  /// </remarks>
  internal class StartGameRouter : LocalRequestRouter<StartGameRequest, StartGameResult>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="StartGameRouter"/> class.
    /// </summary>
    /// <param name="router"><see cref="Router"/> whose lookup table should be utilized.</param>
    public StartGameRouter(Router router)
      : base(router)
    {
    }

    /// <inheritdoc/>
    protected override void ExecuteRequest(IReceiver receiver, StartGameRequest request)
    {
      receiver.StartGame(request);
    }
  }
}
