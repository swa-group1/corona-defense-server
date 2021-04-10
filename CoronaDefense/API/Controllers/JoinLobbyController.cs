// <copyright file="JoinLobbyController.cs" company="NTNU: SWA group 1 (2021)">
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
  public class JoinLobbyController : ControllerBase
  {
    private readonly ILogger<JoinLobbyController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="JoinLobbyController"/> class.
    /// </summary>
    /// <param name="logger">Logger of this <see cref="JoinLobbyController"/>.</param>
    public JoinLobbyController(ILogger<JoinLobbyController> logger)
    {
      this.logger = logger;
    }

    [HttpPatch]
    public JoinLobbyResult Patch([Required] long lobbyId, [Required] string password)
    {
      return API.Instance.JoinLobbyHandler.ProcessRequest(
        new JoinLobbyRequest()
        {
          LobbyId = lobbyId,
          Password = password,
        }
      );
    }
  }
}
