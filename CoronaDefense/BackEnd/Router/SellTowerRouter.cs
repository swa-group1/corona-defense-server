// <copyright file="SellTowerRouter.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using API.Requests;
using API.Schemas;

namespace BackEnd.Router
{
  /// <summary>
  /// <see cref="LocalRequestRouter"/> for requests to sell towers.
  /// </summary>
  internal class SellTowerRouter : LocalRequestRouter<SelltowerRequest, RequestResult>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="SellTowerRouter"/> class.
    /// </summary>
    /// <param name="router"><see cref="Router"/> whose lookup table should be utilized.</param>
    public SellTowerRouter(Router router)
      : base(router)
    {
    }

    /// <inheritdoc/>
    protected override void ExecuteRequest(IReceiver receiver, SelltowerRequest request)
    {
      receiver.SellTower(request);
    }
  }
}
