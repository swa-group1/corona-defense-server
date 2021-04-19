// <copyright file="HighScoreListController.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Communication.API.Schemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BackEnd.Communication.API.Controllers
{
  /// <summary>
  /// Controller for requesting high scores.
  /// </summary>
  /// <remarks>
  /// This object is part of the Communication Layer – Layer 1.
  /// </remarks>
  [ApiController]
  [Route("[controller]")]
  public class HighScoreListController : ControllerBase
  {
    private readonly ILogger<HighScoreListController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="HighScoreListController"/> class.
    /// </summary>
    /// <param name="logger">Logger of this <see cref="HighScoreListController"/>.</param>
    public HighScoreListController(ILogger<HighScoreListController> logger)
    {
      this.logger = logger;
    }

    [HttpGet]
    public HighScoreListResult Get()
    {
      return API.Instance.HighScoreListHandler.ProcessRequest();
    }
  }
}
