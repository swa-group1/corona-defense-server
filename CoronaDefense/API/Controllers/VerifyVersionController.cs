// <copyright file="VerifyVersionController.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using API.Schemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
  /// <summary>
  /// Controller for verifying if client version is compatible with the server version.
  /// </summary>
  [ApiController]
  [Route("[controller]")]
  public class VerifyVersionController : ControllerBase
  {
    private readonly ILogger<VerifyVersionController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="VerifyVersionController"/> class.
    /// </summary>
    /// <param name="logger">Logger of this <see cref="VerifyVersionController"/>.</param>
    public VerifyVersionController(ILogger<VerifyVersionController> logger)
    {
      this.logger = logger;
    }

    [HttpGet]
    public VerifyVersionResult Get([Required] string version)
    {
      return new VerifyVersionResult()
      {
          ValidVersion = true,
      };
    }
  }
}
