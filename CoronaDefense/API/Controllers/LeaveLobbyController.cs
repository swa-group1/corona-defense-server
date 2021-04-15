// <copyright file="LeaveLobbyController.cs" company="NTNU: SWA group 1 (2021)">
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
  /// Controller for requesting joining a lobby.
  /// </summary>
  [ApiController]
  [Route("[controller]")]
  public class LeaveLobbyController : ControllerBase
  {
    private readonly ILogger<LeaveLobbyController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="LeaveLobbyController"/> class.
    /// </summary>
    /// <param name="logger">Logger of this <see cref="LeaveLobbyController"/>.</param>
    public LeaveLobbyController(ILogger<LeaveLobbyController> logger)
    {
      this.logger = logger;
    }

    [HttpPatch]
    public RequestResult Patch([Required] long lobbyId, [Required] long accessToken)
    {
      return API.Instance.LeaveLobbyHandler.ProcessRequest(
        new LocalRequest()
        {
          LobbyId = lobbyId,
          AccessToken = accessToken,
        }
      );
    }
  }
}
