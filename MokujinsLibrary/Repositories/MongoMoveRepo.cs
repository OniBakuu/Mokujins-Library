using System.Collections.Generic;
using System.Linq;
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
        
        public IEnumerable<Move> GetMoves(string character)
        {
            var filter = filterBuilder.Eq(move => move.character, character);
            return movesCollection.Find(filter).ToList();
        }

        public Move GetMove(string character, string input)
        {
            var filter = filterBuilder.Where(move => move.character == character && move.input == input);
            return movesCollection.Find(filter).SingleOrDefault();
        }

        public void CreateMove(Move move)
        {
            movesCollection.InsertOne(move);
        }

        public void UpdateMove(Move move)
        {
            var filter = filterBuilder.Where(existingMove => existingMove.character == move.character && existingMove.input == move.input);
            movesCollection.ReplaceOne(filter, move);
        }

        public void DeleteMove(string character, string input)
        {
            var filter = filterBuilder.Where(move => move.character == character && move.input == input);;
            movesCollection.DeleteOne(filter);
        }
    }
}