// <copyright file="HighScoreListController.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using API.Schemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace API.Controllers
{
  /// <summary>
  /// Controller for requesting high scores.
  /// </summary>
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
      return new HighScoreListResult()
      {
        Scores = new List<HighScoreListResult.Score>()
        {
          new HighScoreListResult.Score() { Name = "Laonard", Value = 100 },
          new HighScoreListResult.Score() { Name = "Maka", Value = 99 },
        },
      };
    }
  }
}
