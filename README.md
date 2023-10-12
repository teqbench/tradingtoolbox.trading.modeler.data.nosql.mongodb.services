# Trading Modeler MongoDB Repository Services API

## Overview
MongoDB repository service implementation for Trading Position Modeler.

# Contents
- [Developer Environment Setup](#Developer+Environment+Setup)
- [Usage](#Usage)
- [DevOps - Configurations, Builds and Deployments](#DevOps)
- [References](#References)
- [License](#License)

# Developer Environment Setup
> [!NOTE]
> In order to access the TeqBench's package registry on GitHub, a personal access token needs to be created with the appropriate scopes and Visual Studio configured to use it. See the [TeqBench Organization's README](https://github.com/teqbench) which outlines how to create a PAT and configure Visual Studio to use it.

## Tooling
- .NET 7.0.x
- Visual Studio

## Dependencies
> [!NOTE]
> Referenced/restored via the project file

- TeqBench.System.Data.NoSql.MongoDB.Repository, 1.0.0
- TradingToolbox.Trading.Modeler.Data.NoSql.MongoDB.Models, 1.1.0

# Usage
## Add NuGet Package To Project
```
dotnet add package TradingToolbox.Trading.Modeler.Data.NoSql.MongoDB.Services
```

## Update Source Code
> [!NOTE]
> For complete usage, see [TradingToolbox.Applications.Trading.Modeler.ServiceApp](https://github.com/teqbench/tradingtoolbox.applications.trading.modeler.serviceapp)

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
                // TODO - Add sort capability to FindAsync in TeqBench.System.Data.NoSql.MongoDb.Repository
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

## Branching Strategy
- GitHub Flow
  - [Introduction From GitHub](https://docs.github.com/en/get-started/quickstart/github-flow)
  - [Indepth Overview](https://githubflow.github.io)

## Branches
- main (production)

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
    - Because used the "Debug" configuration, the NuGet package version created is "0.0.0-dev" to communicate this is a NON-PRODUCTION build/package and should only be used for development/debug purposes; it should NEVER be uploaded to the TeqBench organization's package registry on GitHub.
   
### Deployment
- A local deployment, in effect, is to a local "package source" folder and is configured in Visual Studio at "Visual Studio > Preferences > NuGet > Sources". This is useful when want to test changes to a package before pushing code changes to the project's repository.
- A locally built NuGet package can be deployed locally by copying the "{assembly-name}.0.0.0-dev.nupkg" to the local NuGet package source (i.e. a local folder) as configured in "Visual Studio > Preferences > NuGet > Sources".

## Cloud - Build, Pack(age) & Deploy
- Cloud based build, pack and deployment requires a pull request and successful merge into the main branch in order to start the release workflow.
- If opt to create a NuGet package, the resulting package will be uploaded to the [TeqBench Package Registry](https://github.com/orgs/teqbench/packages) on GitHub.
- As part of the pull request, the "Mergable" option must be set to "PR - Allow merge" in order for the pull request to be merged into the main branch, assuming all other validations pass.
- As part of the pull request, the "Release Type" option must be specified (e.g. "Major (Backwards-incompatible updates and/or bugfixes)", "Minor (Backwards-compatible updates and bugfixes)", or "Patch (Backwards-compatible bugfixes - ONLY)") to determine how the version number will be updated as part of the build. See [TeqBench Org's README](https://github.com/teqbench#version-numbers-in-teqbench) for more information on version numbers in TeqBench.

# License
&copy; 2021 TeqBench. All source code in this repository is only allowed for use by TeqBench; other usage by internal or external parties requires written consent from TeqBench.
