// <copyright file="LobbyListController.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using API.Schemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace API.Controllers
{
  /// <summary>
  /// Controller for getting a list of available lobbies.
  /// </summary>
  [ApiController]
  [Route("[controller]")]
  public class LobbyListController : ControllerBase
  {
    private readonly ILogger<LobbyListController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="LobbyListController"/> class.
    /// </summary>
    /// <param name="logger">Logger of this <see cref="LobbyListController"/>.</param>
    public LobbyListController(ILogger<LobbyListController> logger)
    {
      this.logger = logger;
    }

    [HttpGet]
    public LobbyList Get()
    {
      return new LobbyList()
      {
        Lobbies = new List<Lobby>()
        {
          new Lobby() { Id = 1, Name = "Tarjei", PlayerCount = 99 },
          new Lobby() { Id = 2, Name = "Sultan", PlayerCount = 0 },
        },
      };
    }
  }
}
