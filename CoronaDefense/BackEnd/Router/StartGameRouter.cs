// <copyright file="StartGameRouter.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using API.Requests;
using API.Schemas;

namespace BackEnd.Router
{
  /// <summary>
  /// <see cref="LocalRequestRouter"/> for requests to start a game.
  /// </summary>
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
