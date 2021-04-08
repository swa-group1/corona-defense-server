// <copyright file="SellTowerController.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using API.Schemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace API.Controllers
{
  /// <summary>
  /// Controller for starting a around in a lobby.
  /// </summary>
  [ApiController]
  [Route("[controller]")]
  public class SellTowerController : ControllerBase
  {
    private readonly ILogger<SellTowerController> logger;

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="logger"></param>
    public SellTowerController(ILogger<SellTowerController> logger)
    {
      this.logger = logger;
    }

    [HttpPatch]
    public RequestResult Patch(long lobbyId, long accessToken, int towerId)
    {
      return new RequestResult();
    }
  }
}
