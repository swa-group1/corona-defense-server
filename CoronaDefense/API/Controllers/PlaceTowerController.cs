// <copyright file="PlaceTowerController.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using API.Requests;
using API.Schemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
  /// <summary>
  /// Controller for starting a around in a lobby.
  /// </summary>
  [ApiController]
  [Route("[controller]")]
  public class PlaceTowerController : ControllerBase
  {
    private readonly ILogger<PlaceTowerController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlaceTowerController"/> class.
    /// </summary>
    /// <param name="logger">Logger of this <see cref="PlaceTowerController"/>.</param>
    public PlaceTowerController(ILogger<PlaceTowerController> logger)
    {
      this.logger = logger;
    }

    [HttpPatch]
    public RequestResult Patch(long lobbyId, long accessToken, int x, int y)
    {
      return API.Instance.PlaceTowerHandler.ProcessRequest(
        new PlaceTowerRequest()
        {
          LobbyId = lobbyId,
          AccessToken = accessToken,
          XPosition = x,
          YPosition = y,
        }
      );
    }
  }
}