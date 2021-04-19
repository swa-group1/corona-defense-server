using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BackEnd.Communication.API
{
  /// <summary>
  /// Class for starting the API.
  /// </summary>
  public class Startup
  {
    public IConfiguration Configuration { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Startup"/> class.
    /// </summary>
    /// <param name="configuration">The <see cref="IConfiguration"/> to use.</param>
    public Startup(IConfiguration configuration)
    {
      this.Configuration = configuration;
    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    /// <param name="services">Collection of services to modify.</param>
    public void ConfigureServices(IServiceCollection services)
    {
      _ = services.AddControllers();
      _ = services.AddSwaggerGen(
        delegate (SwaggerGenOptions c)
        {
          c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoronaDefense_API", Version = "v1.2" });
        }
      );
    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app">Application builder used to configure.</param>
    /// <param name="env">Environment to use information from.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoronaDefense_API v1"));
      }

      // app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(
        endpoints =>
        {
          endpoints.MapControllers();
        }
      );
    }
  }
}
