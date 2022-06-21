using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MokujinsLibrary.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace MokujinsLibrary.Repositories
{
    public class MongoMoveRepo : IMoveRepo
    {
        private const string databaseName = "catalog";
        private const string collectionName = "moves";
        private readonly IMongoCollection<Move> movesCollection;
        private readonly FilterDefinitionBuilder<Move> filterBuilder = Builders<Move>.Filter;

        public MongoMoveRepo(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            movesCollection = database.GetCollection<Move>(collectionName);
        }
        
        public async Task<IEnumerable<Move>> GetCharMovesAsync(string character)
        {
            var filter = filterBuilder.Eq(move => move.character, character);
            return await movesCollection.Find(filter).ToListAsync();
        }

        public async Task<Move> GetMoveAsync(string character, string input)
        {
            var filter = filterBuilder.Where(move => move.character == character && move.input == input);
            return await movesCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task CreateMoveAsync(Move move)
        {
            await movesCollection.InsertOneAsync(move);
        }

        public async Task UpdateMoveAsync(Move move)
        {
            var filter = filterBuilder.Where(existingMove => existingMove.character == move.character && existingMove.input == move.input);
            await movesCollection.ReplaceOneAsync(filter, move);
        }

        public async Task DeleteMoveAsync(string character, string input)
        {
            var filter = filterBuilder.Where(move => move.character == character && move.input == input);;
            await movesCollection.DeleteOneAsync(filter);
        }
    }
}