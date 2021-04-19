// <copyright file="StartGameController.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Communication.API.Requests;
using BackEnd.Communication.API.Schemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Communication.API.Controllers
{
  /// <summary>
  /// Controller for starting a around in a lobby.
  /// </summary>
  /// <remarks>
  /// This object is part of the Communication Layer – Layer 1.
  /// </remarks>
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
    public RequestResult Patch([Required] long lobbyId, [Required] long accessToken, [Required] byte stageNumber, [Required] StartGameRequest.Difficulties difficulty)
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
