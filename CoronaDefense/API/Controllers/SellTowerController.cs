// <copyright file="SellTowerController.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using API.Requests;
using API.Schemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

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
    /// Initializes a new instance of the <see cref="SellTowerController"/> class.
    /// </summary>
    /// <param name="logger">Logger of this <see cref="SellTowerController"/>.</param>
    public SellTowerController(ILogger<SellTowerController> logger)
    {
      this.logger = logger;
    }

    [HttpPatch]
    public RequestResult Patch([Required] long lobbyId, [Required] long accessToken, [Required] int towerId)
    {
      return API.Instance.SellTowerHandler.ProcessRequest(
        new SelltowerRequest()
        {
          LobbyId = lobbyId,
          AccessToken = accessToken,
          TowerId = towerId,
        }
      );
    }
  }
}
