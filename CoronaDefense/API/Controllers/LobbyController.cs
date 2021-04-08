// <copyright file="LobbyController.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using API.Schemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
  /// <summary>
  /// Controller for getting a lobby with supplied ID.
  /// </summary>
  [ApiController]
  [Route("[controller]")]
  public class LobbyController : ControllerBase
  {
    private readonly ILogger<LobbyController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="LobbyController"/> class.
    /// </summary>
    /// <param name="logger">Logger of this <see cref="LobbyController"/>.</param>
    public LobbyController(ILogger<LobbyController> logger)
    {
      this.logger = logger;
    }

    [HttpGet]
    public LobbyResult Get([Required] int id)
    {
      return new LobbyResult()
      {
        Lobby = new Lobby()
        {
          Id = id,
          Name = "Tarjei's lobby",
          PlayerCount = 99,
        },
      };
    }
  }
}
