using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace API
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
          delegate(IWebHostBuilder webBuilder)
          {
            webBuilder.UseStartup<Startup>();
          }
        ).Build().Run();
    }
  }
}
