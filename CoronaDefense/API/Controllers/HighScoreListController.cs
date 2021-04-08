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
    private readonly ILogger<WeatherForecastController> logger;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    public HighScoreListController(ILogger<WeatherForecastController> logger)
    {
      this.logger = logger;
    }

    [HttpGet]
    public HighScoreList Get()
    {
      return new HighScoreList()
      {
        Scores = new List<HighScoreList.Score>()
        {
          new HighScoreList.Score() { Name = "Laonard", Value = 100 },
          new HighScoreList.Score() { Name = "Maka", Value = 99 },
        },
      };
    }
  }
}
