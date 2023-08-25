using TradingToolbox.Trading.Modeler.Data.NoSql.MongoDB.Models;
using TradingToolbox.System.Data.NoSql.MongoDB.Repository;
using TradingToolbox.System.Data.NoSql.MongoDB.Repository.Config;

namespace TradingToolbox.Trading.Modeler.Data.NoSql.MongoDB.Services.PositionModeling
{
    /// <summary>
    /// Position model respository for position model documents.
    /// </summary>
    /// <seealso cref="System.Data.NoSql.MongoDB.Repository{PositionModelDocument}" />
    /// <seealso cref="IPositionModelRepository" />
    public class PositionModelRepository : Repository<PositionModelDocument>, IPositionModelRepository
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PositionModelRepository"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public PositionModelRepository(IRepositoryConfig config) : base(config)
        {
        } 
        #endregion
    }
}
