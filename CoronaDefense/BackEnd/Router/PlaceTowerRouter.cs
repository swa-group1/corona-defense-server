﻿// <copyright file="PlaceTowerRouter.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Communication.API.Requests;
using BackEnd.Communication.API.Schemas;

namespace BackEnd.Router
{
  /// <summary>
  /// <see cref="LocalRequestRouter"/> for requests to place a tower.
  /// </summary>
  /// <remarks>
  /// This object is part of the Traffic / Router Layer – Layer 2.
  /// </remarks>
  internal class PlaceTowerRouter : LocalRequestRouter<PlaceTowerRequest, RequestResult>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="PlaceTowerRouter"/> class.
    /// </summary>
    /// <param name="router"><see cref="Router"/> whose lookup table should be utilized.</param>
    public PlaceTowerRouter(Router router)
      : base(router)
    {
    }

    /// <inheritdoc/>
    protected override void ExecuteRequest(IReceiver receiver, PlaceTowerRequest request)
    {
      receiver.PlaceTower(request);
    }
  }
}
