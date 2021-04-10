// <copyright file="CreateLobbyController.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using API.Schemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
  /// <summary>
  /// Controller for confirming that the client with a specific access token has connected to the socket channel.
  /// </summary>
  [ApiController]
  [Route("[controller]")]
  public class ActivateClientController : ControllerBase
  {
    private readonly ILogger<ActivateClientController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ActivateClientController"/> class.
    /// </summary>
    /// <param name="logger">Logger of this <see cref="ActivateClientController"/>.</param>
    public ActivateClientController(ILogger<ActivateClientController> logger)
    {
      this.logger = logger;
    }

    [HttpPatch]
    public RequestResult Patch([Required] long lobbyId, [Required] long accessToken)
    {
      return new RequestResult();
    }
  }
}
