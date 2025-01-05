using ConsoleAppSquareMaster;
using MongoDB.Bson;

namespace ConsoleAppSquareMaster
{
    public class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "mongodb://localhost:27017/";
            WorldRepository repo = new WorldRepository(connectionString);
            EmpireRepository empireRepo = new EmpireRepository(connectionString);

            int[] empireAlgorithms = { 1, 2, 3 };
            World world = new World();
            var worldBuild = world.BuildWorld2(100, 100, 0.6);
            int nEmpires = 5;
            int turns = 25000;
            WorldConquer worldConquer = new WorldConquer(worldBuild, empireAlgorithms);
            WorldModel newWorld;
            int[,] generatedWorld;
            BitmapWriter bitmapWriter = new BitmapWriter();

            for (int i = 1; i <= 10; i++)
            {

                string worldName = "World_" + i;
                int maxX = 100;
                int maxY = 100;
                double coverage = 0.6;
                int selectedAlgorithm;



                generatedWorld = new int[maxX, maxY];
                switch (i % 3)
                {
                    case 0:
                        generatedWorld = worldConquer.Conquer1(nEmpires, turns);
                        selectedAlgorithm = 1;
                        break;
                    case 1:
                        generatedWorld = worldConquer.Conquer3(nEmpires, turns);
                        selectedAlgorithm = 2;
                        break;
                    default:
                        generatedWorld = worldConquer.Conquer3(nEmpires, turns);
                        selectedAlgorithm = 3;
                        break;
                }

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
                bitmapWriter.DrawWorld(generatedWorld, worldName);
            }



            var allWorlds = repo.GetAllWorlds();
            foreach (var w in allWorlds)
            {

                //worldConquer.CalculateEmpiresSize(nEmpires, w.GeneratedWorld, w.AlgorithmType);
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


