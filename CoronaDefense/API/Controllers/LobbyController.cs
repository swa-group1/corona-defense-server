// <copyright file="LobbyController.cs" company="NTNU: SWA group 1 (2021)">
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
  /// Controller for getting a lobby with supplied ID.
  /// </summary>
  /// <remarks>
  /// This object is part of the Communication Layer – Layer 1.
  /// </remarks>
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
    public LobbyResult Get([Required] long id)
    {
      return API.Instance.LobbyHandler.ProcessRequest(
        new LobbyRequest()
        {
          Id = id,
        }
      );
    }
  }
}
