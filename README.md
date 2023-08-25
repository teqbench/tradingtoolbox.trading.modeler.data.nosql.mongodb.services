# Overview
Data service implementations for Trading Position Modeler. Will be added to service API app as a singleton upon startup.
# Example Usage
```json
// appsettings.json in a service's API application for CORS configuration values (dev environment setting example)
{
  "AlgorithmicTradingTradeModelerMongoDbRepositoryConfig": {
    "ConnectionString": "",
    "DatabaseName": "at-trade-modeler"
  }
}
```
```c#
// Startup.cs in Service API application.
public class Startup
{
    public Startup(IConfiguration configuration)
    {
    }

    public IConfiguration Configuration { get; }

    /// <summary>
    /// Configures the services.
    /// </summary>
    /// <param name="services">The service descriptors.</param>
    /// <remarks>This method gets called by the runtime. Use this method to add services to the container.</remarks>
    public void ConfigureServices(IServiceCollection services)
    {
        // Per https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1 the following 
        // AddSingleton method signature will NOT use automatic disposal. But in this case, need to create an instance
        // of the MongoDbService so can supply values to its constructor. So, to ensure it will be disposed of when
        // the app shutsdown, See https://andrewlock.net/four-ways-to-dispose-idisposables-in-asp-net-core/ for correct
        // way to dispose using IApplicationLifetime event

        MongoDbService dbService = new MongoDbService(Configuration.GetSection("AlgorithmicTradingTradeModelerMongoDbRepositoryConfig").Get<RepositoryConfig>());
        dbService.Initialize();

        services.AddSingleton<IMongoDbService>(dbService);
    }

    /// <summary>
    /// Configures the specified application.
    /// </summary>
    /// <param name="app">The application mechanisms to configure an application's pipeline.</param>
    /// <param name="env">The web hosting environmwent information the application will run in.</param>
    /// <param name="lifetime">The application lifetime event provider.</param>
    /// <param name="dbService">The MongoDB database service instance to dispose when app is shutting down.</param>
    /// <remarks>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </remarks>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime, IMongoDbService dbService)
    {
        lifetime.ApplicationStopping.Register(state => {
            ((IDisposable) state).Dispose();
        }, dbService);
    }
}
```
# Build
# Deployment