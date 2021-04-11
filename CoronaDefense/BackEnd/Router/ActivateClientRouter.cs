// <copyright file="ActivateClientRouter.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using API.Requests;
using API.Schemas;

namespace BackEnd.Router
{
  /// <summary>
  /// <see cref="LocalRequestRouter"/> for requests to activate clients.
  /// </summary>
  internal class ActivateClientRouter : LocalRequestRouter<LocalRequest, RequestResult>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ActivateClientRouter"/> class.
    /// </summary>
    /// <param name="router"><see cref="Router"/> whose lookup table should be utilized.</param>
    public ActivateClientRouter(Router router)
      : base(router)
    {
    }

    /// <inheritdoc/>
    protected override void ExecuteRequest(IReceiver receiver, LocalRequest request)
    {
      receiver.ActivateClient(request);
    }
  }
}
