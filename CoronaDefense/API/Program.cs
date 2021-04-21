// <copyright file="Program.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BackEnd.Communication.API
{
  /// <summary>
  /// Entry point of API program.
  /// </summary>
  public static class Program
  {
    /// <summary>
    /// Start API.
    /// </summary>
    /// <param name="args">Arguments supplied to the host builder.</param>
    public static void Main(string[] args)
    {
      Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(
          delegate (IWebHostBuilder webBuilder)
          {
            _ = webBuilder.UseStartup<Startup>();
            _ = webBuilder.UseUrls("https://*:5001", "http://*:5000");
          }
        ).Build().Run();
    }
  }
}
