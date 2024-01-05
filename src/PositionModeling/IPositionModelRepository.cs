using TradingToolbox.Trading.Modeler.Data.NoSql.MongoDB.Models;
using TeqBench.System.Data.NoSql.MongoDB.Repository;

namespace TradingToolbox.Trading.Modeler.Data.NoSql.MongoDB.Services.PositionModeling
{
    /// <summary>
    /// Position model respository interface for position model documents.
    /// </summary>
    /// <seealso cref="IRepository{Models}" />
    public interface IPositionModelRepository : IRepository<PositionModelDocument>
    {
    }
}
