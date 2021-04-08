// <copyright file="StartRoundController.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

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
  public class StartRoundController : ControllerBase
  {
    private readonly ILogger<StartRoundController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="StartRoundController"/> class.
    /// </summary>
    /// <param name="logger">Logger of this <see cref="StartRoundController"/>.</param>
    public StartRoundController(ILogger<StartRoundController> logger)
    {
      this.logger = logger;
    }

    [HttpPatch]
    public RequestResult Patch(long lobbyId, long accessToken)
    {
      return new RequestResult();
    }
  }
}
