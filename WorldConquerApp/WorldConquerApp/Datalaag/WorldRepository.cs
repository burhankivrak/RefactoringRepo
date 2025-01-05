using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldConquerApp.Datalaag
{
    public class WorldRepository
    {
        private readonly IMongoCollection<WorldModel> worlds;

        public WorldRepository(string connectionString)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("WorldsDatabase");
            worlds = database.GetCollection<WorldModel>("worlds");
        }

        public void SaveWorld(WorldModel world)
        {
            worlds.InsertOne(world);
        }

        public List<WorldModel> GetAllWorlds()
        {
            return worlds.Find(world => true).ToList();
        }
    }
}
