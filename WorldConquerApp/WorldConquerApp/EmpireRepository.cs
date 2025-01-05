using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster
{
    public class EmpireRepository
    {
        private readonly IMongoCollection<EmpireModel> empires;

        public EmpireRepository(string connectionString)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("WorldsDatabase");
            empires = database.GetCollection<EmpireModel>("empires");
        }

        public void SaveEmpire(EmpireModel empire)
        {
            empires.InsertOne(empire);
        }

        public List<EmpireModel> GetAllEmpires()
        {
            return empires.Find(empire => true).ToList();
        }
    }
}
