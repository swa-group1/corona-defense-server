// <copyright file="StartRoundRouter.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Communication.API.Requests;
using BackEnd.Communication.API.Schemas;

namespace BackEnd.Router
{
  /// <summary>
  /// <see cref="LocalRequestRouter"/> for requests to start a round.
  /// </summary>
  internal class StartRoundRouter : LocalRequestRouter<LocalRequest, RequestResult>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="StartRoundRouter"/> class.
    /// </summary>
    /// <param name="router"><see cref="Router"/> whose lookup table should be utilized.</param>
    public StartRoundRouter(Router router)
      : base(router)
    {
    }

    /// <inheritdoc/>
    protected override void ExecuteRequest(IReceiver receiver, LocalRequest request)
    {
      receiver.StartRound(request);
    }
  }
}
