namespace TradingToolbox.Trading.Modeler.Data.NoSql.MongoDB.Services.PositionModeling
{
    /// <summary>
    /// MongoDb service interface for trade modeler repositories.
    /// </summary>
    public interface IMongoDbService
    {
        /// <summary>
        /// Gets the trade modeler position model repository.
        /// </summary>
        /// <value>
        /// The trade modeler position model repository.
        /// </value>
        IPositionModelRepository PositionModelRepository { get; }
    }
}
