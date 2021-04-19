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
  /// Controller for creating a new lobby.
  /// </summary>
  /// <remarks>
  /// This object is part of the Communication Layer – Layer 1.
  /// </remarks>
  [ApiController]
  [Route("[controller]")]
  public class CreateLobbyController : ControllerBase
  {
    private readonly ILogger<CreateLobbyController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateLobbyController"/> class.
    /// </summary>
    /// <param name="logger">Logger of this <see cref="CreateLobbyController"/>.</param>
    public CreateLobbyController(ILogger<CreateLobbyController> logger)
    {
      this.logger = logger;
    }

    [HttpPost]
    public CreateLobbyResult Post([Required] string name, [Required] string password)
    {
      return API.Instance.CreateLobbyHandler.ProcessRequest(
        new Requests.CreateLobbyRequest()
        {
          Name = name,
          Password = password,
        }
      );
    }
  }
}
