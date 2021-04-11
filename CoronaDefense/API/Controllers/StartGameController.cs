// <copyright file="StartRoundController.cs" company="NTNU: SWA group 1 (2021)">
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
  public class StartGameController : ControllerBase
  {
    private readonly ILogger<StartGameController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="StartGameController"/> class.
    /// </summary>
    /// <param name="logger">Logger of this <see cref="StartGameController"/>.</param>
    public StartGameController(ILogger<StartGameController> logger)
    {
      this.logger = logger;
    }

    [HttpPatch]
    public RequestResult Patch(long lobbyId, long accessToken, byte stageNumber, StartGameRequest.Difficulties difficulty)
    {
      return API.Instance.StartGameHandler.ProcessRequest(
        new StartGameRequest()
        {
          LobbyId = lobbyId,
          AccessToken = accessToken,
          StageNumber = stageNumber,
          Difficulty = difficulty,
        }
      );
    }
  }
}
