# Trading Modeler MongoDB Services API

## Overview
MongoDB data service implementation for Trading Modeler. Will be added to service API app as a singleton upon startup.

# Contents
- [Developer Environment Setup](#Developer+Environment+Setup)
- [Usage](#Usage)
- [DevOps - Configurations, Builds and Deployments](#DevOps)
- [References](#References)
- [License](#License)

# Developer Environment Setup
> [!NOTE]
> In order to access the Trading Toolbox's package registry on GitHub, a personal access token needs to be created with the appropriate scopes and Visual Studio configured to use it. See the [Trading Toolbox Organization's README](https://github.com/trading-toolbox) which outlines how to create a PAT and configure Visual Studio to use it.

## Tooling
- .NET 7.0.x
- Visual Studio

## Dependencies
> [!NOTE]
> Referenced/restored via the project file

- MongoDB.Bson, 2.21.0
- TradingToolbox.System.Data.NoSql.MongoDB.Models, 5.1.0

# Usage
## Add NuGet Package To Project
```
dotnet add package TradingToolbox.Trading.Modeler.Data.NoSql.MongoDB.Models
```

## Update Source Code
> [!NOTE]
> For complete usage, see [TradingToolbox.Applications.Trading.Modeler.ServiceApp](https://github.com/trading-toolbox/tradingtoolbox.applications.trading.modeler.serviceapp)

```csharp
namespace TradingToolbox.Applications.Trading.Modeler.ServiceApp.Controllers
{
    /// <summary>
    /// Controller for trading position modeling.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("api/tradingtoolbox/trading/[controller]")]
    public class ModelerController : ControllerBase
    {
        private readonly IMongoDbService _mongoDbService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelerController" /> class.
        /// </summary>
        /// <param name="mongoDbService">The mongo database service to do DB operstaions.</param>
        public ModelerController(IMongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        /// <summary>
        /// Gets all of the position documents from the DB
        /// </summary>
        /// <returns>List of position documents.</returns>
        [HttpGet("positions")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<PositionModelDocument> items = new List<PositionModelDocument>();

            try
            {
                // For now, just force sort to be by ListPosition and do here...can move to FindAsync later...
                // TODO - Add sort capability to FindAsync in TradingToolbox.System.Data.NoSql.MongoDb.Repository
                items = (await _mongoDbService.PositionModelRepository.FindAsync(_ => true)).OrderBy(item => item.ListPosition);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }

            return Ok(items);
        }
    }
}
```

# DevOps
## Configurations
- Release
    - This configuration is used for compilation of releases to non-debug environments, i.e. production and preview environments.
- Debug
    - This configuration is used for compilation of releases to development/debug environments.

## Branches
- main (production)
- staging (production preview)
- dev (developer integration)

## Local - Build, Pack(age) & Deploy
- To build/pack locally use the "Debug" configuration.

### Build
- To create NuGet build locally, can be done either in Visual Studio or command line.
  - Visual Studio
    - Load the project
    - Right-mouse clicking the project file to bring up the context menu and selecting "Build {project's name}".
  - Command Line
    - Open terminal.
    - Navigate to the project's root folder and issue the command "dotnet build -c:Debug".
  - Build Output
    - Build output for Visual Studio or command line, i.e. assembly, will be found in the "{project}/bin/Debug/" folder.

### Pack(age)
- To create NuGet package locally, can be done either in Visual Studio or command line.
  - Visual Studio
    - Load the project
    - Right-mouse clicking the project file to bring up the context menu and selecting "Pack {project's name}". 
  - Command Line
    - Open terminal.
    - Navigate to the project's root folder and issue the command "dotnet pack -c:Debug"
  - Pack Output
    - Pack command output for Visual Studio or command line, i.e. NuGet package file ".0.0.0-dev.nupkg", will be found in the "{project}/bin/Debug/" folder.
    - Because used the "Debug" configuration, the NuGet package version created is "0.0.0-dev" to communicate this is a NON-PRODUCTION build/package and should only be used for development/debug purposes; it should NEVER be uploaded to the Trading Toolbox organization's package registry on GitHub.
   
### Deployment
- A local deployment, in effect, is to a local "package source" folder and is configured in Visual Studio at "Visual Studio > Preferences > NuGet > Sources". This is useful when want to test changes to a package before pushing code changes to the project's repository.
- A locally built NuGet package can be deployed locally by copying the "{assembly-name}.0.0.0-dev.nupkg" to the local NuGet package source (i.e. a local folder) as configured in "Visual Studio > Preferences > NuGet > Sources".

## Cloud - Build, Pack(age) & Deploy
- Cloud based build/pack use the "Release" configuration, and currently, ONLY builds from the "main" branch.
- Cloud based build/pack/deploy use the [Production Release Workflow](https://github.com/trading-toolbox/production-release-workflow/actions/workflows/production-release-workflow.yml) Trading Toolbox Action to build and optionally pack/deploy a NuGet package for a selected project (i.e. repository).
- If opt to create a NuGet package, the resulting package will be uploaded to the [Trading Toolbox Package Registry](https://github.com/orgs/trading-toolbox/packages) on GitHub.
- As part of the [Production Release Workflow](https://github.com/trading-toolbox/production-release-workflow/actions/workflows/production-release-workflow.yml) build options, select what type of update the release is, e.g. "Major (Backwards-incompatible functionality added)", "Minor (Backwards-compatible functionality added)", or "Patch/Revision (Bugfixes/updates for a specific release)" to determine how the version number will be updated as part of the build. See [Trading Toolbox Org's README](https://github.com/trading-toolbox#version-numbers-in-trading-toolbox) for more information on version numbers in Trading Toolbox.

# License
&copy; 2021 Trading Toolbox. All source code in this repository is only allowed for use by Trading Toolbox; other usage by internal or external parties requires written consent from Trading Toolobx.
