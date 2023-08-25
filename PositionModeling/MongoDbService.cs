using TradingToolbox.System.Data.NoSql.MongoDB.Repository.Config;

namespace TradingToolbox.Trading.Modeler.Data.NoSql.MongoDB.Services.PositionModeling
{
    /// <summary>
    /// MongoDb service for trade modeler repositories.
    /// </summary>
    /// <seealso cref="IMongoDbService" />
    public class MongoDbService : IMongoDbService
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDbService"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public MongoDbService(IRepositoryConfig config)
        {
            // Init repository from supplied config values.
            this.PositionModelRepository = new PositionModelRepository(config);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the trade modeler position model repository.
        /// </summary>
        /// <value>
        /// The trade modeler position model repository.
        /// </value>
        public IPositionModelRepository PositionModelRepository { get; private set; }
        #endregion
    }
}
