# Trading Modeler MongoDB Repository Services API

![Build Status Badge](.badges/build-status.svg) ![Build Number Badge](.badges/build-number.svg) ![Coverage](.badges/code-coverage.svg)

## Overview

MongoDB repository service implementation for Trading Position Modeler.

## Contents
- [Developer Environment Setup](#Developer+Environment+Setup)
- [Usage](#Usage)
- [License](#License)

## Developer Environment Setup

### General
- [Branching Strategy & Practices](https://github.com/teqbench/teqbench.docs/wiki/Branching-Strategy)

### .NET
- [General Tooling](https://github.com/teqbench/teqbench.docs/wiki/.NET-General-Tooling)
- [Configurations](https://github.com/teqbench/teqbench.docs/wiki/.NET-Configuration-Standards)
- [Coding Standards](https://github.com/teqbench/teqbench.docs/wiki/.NET-Coding-Standards)
- [Solutions](https://github.com/teqbench/teqbench.docs/wiki/.NET-Solutions)
- [Projects](https://github.com/teqbench/teqbench.docs/wiki/.NET-Projects)
- [Building](https://github.com/teqbench/teqbench.docs/wiki/.NET-Build-Process)
- [Package & Deployment](https://github.com/teqbench/teqbench.docs/wiki/.NET-Package-Deploy)
- [Versioning](https://github.com/teqbench/teqbench.docs/wiki/.NET-Versioning-Standards)

## Usage

### Add NuGet Package To Project

```
dotnet add package TradingToolbox.Trading.Modeler.Data.NoSql.MongoDB.Services
```

### Update Source Code

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

## Licensing

[License](https://github.com/teqbench/teqbench.docs/wiki/License)
