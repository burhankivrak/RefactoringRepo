using ConsoleAppSquareMaster;
using MongoDB.Bson;
using WorldConquerApp.Datalaag;
using WorldConquerApp.StrategyPattern;

namespace WorldConquerApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "mongodb://localhost:27017/";
            WorldRepository repo = new WorldRepository(connectionString);
            EmpireRepository empireRepo = new EmpireRepository(connectionString);
            BitmapWriter bmw = new BitmapWriter();

            WorldModel newWorld;
           
            for (int i = 1; i <= 10; i++)
            {

                string worldName = "World_" + i;
                int maxX = 100;
                int maxY = 100;
                double coverage = 0.6;
                bool[,] worldBuild = new World().BuildWorld2(maxX, maxY, coverage);

                int nEmpires = 5;
                int turns = 25000;
                WorldConquer worldConquer = new WorldConquer(worldBuild);
                int[,] generatedWorld = new int[maxX, maxY];
                int selectedAlgorithm = 1 + (i % 3);

                IConquerStrategy conquerStrategy;
                WorldConquerContext ctx;
                switch (selectedAlgorithm)
                {
                    case 1:
                        conquerStrategy = new Conquer1();
                        break;
                    case 2:
                        conquerStrategy = new Conquer2();
                        break;
                    case 3:
                        conquerStrategy = new Conquer3();
                        break;
                    default:
                        throw new Exception("Ongeldig algorithme");
                }
                ctx = new WorldConquerContext(conquerStrategy);
                generatedWorld = ctx.ExecuteConquer(worldBuild, nEmpires, turns);

                newWorld = new WorldModel
                {
                    Name = worldName,
                    AlgorithmType = selectedAlgorithm,
                    MaxX = maxX,
                    MaxY = maxY,
                    Coverage = coverage,
                    GeneratedWorld = generatedWorld,
                    EmpireIds = new List<ObjectId>()
                };

                repo.SaveWorld(newWorld);
                worldConquer.CalculateEmpiresSize(nEmpires, generatedWorld, selectedAlgorithm, newWorld.Id);
                bmw.DrawWorld(generatedWorld, worldName);
            }



            var allWorlds = repo.GetAllWorlds();
            foreach (var w in allWorlds)
            {
                var allEmpires = empireRepo.GetAllEmpires();
                Console.WriteLine($"Details voor {w.Name} - Conquer{w.AlgorithmType}:");
                foreach (var empire in allEmpires)
                {
                    if (empire.WorldId == w.Id)
                    {
                        Console.WriteLine($"Empire {empire.Name}:");
                        Console.WriteLine($"- Size: {empire.Size} cells");
                        Console.WriteLine($"- Percentage of world: {empire.Percentage}%");
                        Console.WriteLine($"- Algorithm used: {empire.AlgorithmType}");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}


