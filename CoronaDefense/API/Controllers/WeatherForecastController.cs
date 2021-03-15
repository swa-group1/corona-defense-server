﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private static readonly string[] Summaries = { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

    private readonly ILogger<WeatherForecastController> logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
      this.logger = logger;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
      Random rng = new Random();
      return Enumerable.Range(1, 5).Select(
          delegate(int index)
          {
            return new WeatherForecast { Date = DateTime.Now.AddDays(index), TemperatureC = rng.Next(-20, 55), Summary = Summaries[rng.Next(Summaries.Length)] };
          }
        )
        .ToArray();
    }
  }
}
