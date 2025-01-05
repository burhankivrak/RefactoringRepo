using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster
{
    public class WorldConquer
    {
        private bool[,] world;
        private int[,] worldempires;
        private int maxx, maxy;
        private Random random = new Random(1);
        private int[] empireAlgorithms;
        public WorldConquer(bool[,] world, int[] empireAlgorithms)
        {
            this.world = world;
            this.empireAlgorithms = empireAlgorithms;
            maxx = world.GetLength(0);
            maxy = world.GetLength(1);
            worldempires = new int[maxx,maxy];
            for (int i = 0; i < world.GetLength(0); i++) for (int j = 0; j < world.GetLength(1); j++) if (world[i, j]) worldempires[i, j] = 0; else worldempires[i, j] = -1;
        }
        //public int[,] ConquerWithDifferentAlgorithms(int nEmpires, int turns)
        //{
        //    Dictionary<int, List<(int, int)>> empires = new(); 
        //    int x, y;

        //    for (int i = 0; i < nEmpires; i++)
        //    {
        //        bool ok = false;
        //        while (!ok)
        //        {
        //            x = random.Next(maxx); y = random.Next(maxy);
        //            if (world[x, y])
        //            {
        //                ok = true;
        //                worldempires[x, y] = i + 1;
        //                empires.Add(i + 1, new List<(int, int)>() { (x, y) });
        //            }
        //        }
        //    }

        //    int direction; 
        //    int index;

        //    for (int i = 0; i < turns; i++)
        //    {
        //        for (int e = 1; e <= nEmpires; e++)
        //        {
        //            int algorithmChoice = empireAlgorithms[random.Next(empireAlgorithms.Length)];

        //            switch (algorithmChoice)
        //            {
        //                case 1:
        //                    //Conquer1 algoritme
        //                    index = random.Next(empires[e].Count);
        //                    x = empires[e][index].Item1;
        //                    y = empires[e][index].Item2;
        //                    TryExpandEmpire(e, x, y);
        //                    break;
        //                case 2:
        //                    //Conquer2 algoritme
        //                    index = FindWithMostEmptyNeighbours(e, empires[e]);
        //                    x = empires[e][index].Item1;
        //                    y = empires[e][index].Item2;
        //                    TryExpandEmpire(e, x, y);
        //                    break;
        //                case 3:
        //                    // Conquer3 algoritme
        //                    index = random.Next(empires[e].Count);
        //                    pickEmpty(empires[e], index, e);
        //                    break;
        //                default:
        //                    throw new ArgumentException($"Onbekend algoritme keuze voor empire {e}");
        //            }
        //        }
        //    }

        //    return worldempires;
        //}
        public int[,] Conquer1(int nEmpires, int turns)
        {
            Dictionary<int, List<(int, int)>> empires = new(); //key is the empire id, value is the list of cells (x,y) the empire controls
            //search random start positions of each empire
            //start positions must be located on the world and each empire requires a different start position
            int x, y;
            for (int i = 0; i < nEmpires; i++)
            {
                bool ok = false;
                while (!ok)
                {
                    x = random.Next(maxx); y = random.Next(maxy);
                    if (world[x, y])
                    {
                        ok = true;
                        worldempires[x, y] = i + 1;
                        empires.Add(i + 1, new List<(int, int)>() { (x, y) });
                    }
                }
            }
            int index;
            int direction;//0-right,1-left,2-top,3-bottom
            for (int i = 0; i < turns; i++)
            {
                for (int e = 1; e <= nEmpires; e++)
                {
                    index = random.Next(empires[e].Count);
                    direction = random.Next(4);
                    x = empires[e][index].Item1;
                    y = empires[e][index].Item2;
                    switch (direction)
                    {
                        case 0:
                            if (x < maxx - 1 && worldempires[x + 1, y] == 0)
                            {
                                worldempires[x + 1, y] = e;
                                empires[e].Add((x + 1, y));
                            }
                            break;
                        case 1:
                            if (x > 0 && worldempires[x - 1, y] == 0)
                            {
                                worldempires[x - 1, y] = e;
                                empires[e].Add((x - 1, y));
                            }
                            break;
                        case 2:
                            if (y < maxy - 1 && worldempires[x, y + 1] == 0)
                            {
                                worldempires[x, y + 1] = e;
                                empires[e].Add((x, y + 1));
                            }
                            break;
                        case 3:
                            if (y > 0 && worldempires[x, y - 1] == 0)
                            {
                                worldempires[x, y - 1] = e;
                                empires[e].Add((x, y - 1));
                            }
                            break;
                    }
                }
            }
            return worldempires;
        }
        public int[,] Conquer3(int nEmpires, int turns)
        {
            Dictionary<int, List<(int, int)>> empires = new();//key is the empire id, value is the list of cells (x,y) the empire controls
            //search random start positions of each empire
            //start positions must be located on the world and each empire requires a different start position
            int x, y;
            for (int i = 0; i < nEmpires; i++)
            {
                bool ok = false;
                while (!ok)
                {
                    x = random.Next(maxx); y = random.Next(maxy);
                    if (world[x, y])
                    {
                        ok = true;
                        worldempires[x, y] = i + 1;
                        empires.Add(i + 1, new List<(int, int)>() { (x, y) });
                    }
                }
            }
            int index;
            for (int i = 0; i < turns; i++)
            {
                for (int e = 1; e <= nEmpires; e++)
                {
                    index = random.Next(empires[e].Count);
                    pickEmpty(empires[e], index, e);
                }
            }
            return worldempires;
        }
        public int[,] Conquer2(int nEmpires, int turns)
        {
            Dictionary<int, List<(int, int)>> empires = new();//key is the empire id, value is the list of cells (x,y) the empire controls
            //search random start positions of each empire
            //start positions must be located on the world and each empire requires a different start position
            int x, y;
            for (int i = 0; i < nEmpires; i++)
            {
                bool ok = false;
                while (!ok)
                {
                    x = random.Next(maxx); y = random.Next(maxy);
                    if (world[x, y])
                    {
                        ok = true;
                        worldempires[x, y] = i + 1;
                        empires.Add(i + 1, new List<(int, int)>() { (x, y) });
                    }
                }
            }
            int index;
            int direction;//0-right,1-left,2-top,3-bottom
            for (int i = 0; i < turns; i++)
            {
                for (int e = 1; e <= nEmpires; e++)
                {
                    index = FindWithMostEmptyNeighbours(e, empires[e]);
                    direction = random.Next(4);
                    x = empires[e][index].Item1;
                    y = empires[e][index].Item2;
                    switch (direction)
                    {
                        case 0:
                            if (x < maxx - 1 && worldempires[x + 1, y] == 0)
                            {
                                worldempires[x + 1, y] = e;
                                empires[e].Add((x + 1, y));
                            }
                            break;
                        case 1:
                            if (x > 0 && worldempires[x - 1, y] == 0)
                            {
                                worldempires[x - 1, y] = e;
                                empires[e].Add((x - 1, y));
                            }
                            break;
                        case 2:
                            if (y < maxy - 1 && worldempires[x, y + 1] == 0)
                            {
                                worldempires[x, y + 1] = e;
                                empires[e].Add((x, y + 1));
                            }
                            break;
                        case 3:
                            if (y > 0 && worldempires[x, y - 1] == 0)
                            {
                                worldempires[x, y - 1] = e;
                                empires[e].Add((x, y - 1));
                            }
                            break;
                    }
                }
            }
            return worldempires;
        }

        public void CalculateEmpiresSize(int nEmpires, int[,] world, int worldAlgorithm, ObjectId worldId)
        {
            // Totaal aantal vakjes in de wereld
            int totalWorldSize = world.GetLength(0) * world.GetLength(1);

            // Itereer over de empires en bereken hun grootte en percentage van de wereld
            for (int empire = 1; empire <= nEmpires; empire++)
            {
                int empireSize = 0;

                // Itereer over de wereldempires array en tel de vakjes van het huidige empire
                for (int i = 0; i < world.GetLength(0); i++)
                {
                    for (int j = 0; j < world.GetLength(1); j++)
                    {
                        if (world[i, j] == empire)
                        {
                            empireSize++;
                        }
                    }
                }

                // Bereken het percentage
                double empirePercentage = (double)empireSize / totalWorldSize * 100;

                // Voeg de gegevens toe aan de lijst
                EmpireModel empireModel = new EmpireModel
                {
                    Name = empire,
                    Size = empireSize,
                    Percentage = empirePercentage,
                    AlgorithmType = worldAlgorithm,
                    WorldId = worldId
                };
                
                EmpireRepository empireRepo = new EmpireRepository("mongodb://localhost:27017/");
                empireRepo.SaveEmpire(empireModel);

            }

        }

        private bool TryExpandEmpire(int empire, int x, int y)
        {
            int direction = random.Next(4);
            switch (direction)
            {
                case 0:
                    if (x < maxx - 1 && worldempires[x + 1, y] == 0) 
                    {
                        worldempires[x + 1, y] = empire;
                        return true;
                    }
                    break;
                case 1:
                    if (x > 0 && worldempires[x - 1, y] == 0) 
                    {
                        worldempires[x - 1, y] = empire;
                        return true;
                    }
                    break;
                case 2:
                    if (y < maxy - 1 && worldempires[x, y + 1] == 0) 
                    {
                        worldempires[x, y + 1] = empire;
                        return true;
                    }
                    break;
                case 3:
                    if (y > 0 && worldempires[x, y - 1] == 0)
                    {
                        worldempires[x, y - 1] = empire;
                        return true;
                    }
                    break;
            }
            return false;
        }

        private void pickEmpty(List<(int,int)> empire,int index,int e)
        {
            List<(int, int)> n = new List<(int, int)>();
            if (IsValidPosition(empire[index].Item1-1, empire[index].Item2)
                && (worldempires[empire[index].Item1 - 1, empire[index].Item2]==0)) n.Add((empire[index].Item1-1, empire[index].Item2));
            if (IsValidPosition(empire[index].Item1+1, empire[index].Item2)
                && (worldempires[empire[index].Item1 + 1, empire[index].Item2] == 0)) n.Add((empire[index].Item1+1, empire[index].Item2));
            if (IsValidPosition(empire[index].Item1, empire[index].Item2-1)
                && (worldempires[empire[index].Item1, empire[index].Item2-1] == 0)) n.Add((empire[index].Item1, empire[index].Item2-1));
            if (IsValidPosition(empire[index].Item1, empire[index].Item2+1)
                && (worldempires[empire[index].Item1, empire[index].Item2+1] == 0)) n.Add((empire[index].Item1, empire[index].Item2+1));
            int x = random.Next(n.Count);
            if (n.Count > 0)
            {
                empire.Add(n[x]);
                worldempires[n[x].Item1, n[x].Item2] = e;
            }
        }

        private int FindWithMostEmptyNeighbours(int e, List<(int, int)> empire)
        {
            List<int> indexes = new List<int>();
            int n = 0;
            int calcN;
            for (int i = 0; i < empire.Count; i++)
            {
                calcN = EmptyNeighbours(e, empire[i].Item1, empire[i].Item2);
                if (calcN >= n)
                {
                    indexes.Clear();
                    n = calcN;
                    indexes.Add(i);
                }
            }
            return indexes[random.Next(indexes.Count)];
        }
        private int EmptyNeighbours(int empire, int x, int y)
        {
            int n = 0;
            if (IsValidPosition(x - 1, y) && worldempires[x - 1, y] == 0) n++;
            if (IsValidPosition(x + 1, y) && worldempires[x + 1, y] == 0) n++;
            if (IsValidPosition(x, y - 1) && worldempires[x, y - 1] == 0) n++;
            if (IsValidPosition(x, y + 1) && worldempires[x, y + 1] == 0) n++;
            return n;
        }


        private bool IsValidPosition(int x, int y)
        {
            if (x<0) return false;
            if (x >= world.GetLength(0)) return false;
            if (y<0) return false;
            if (y>= world.GetLength(1)) return false;
            return true;
        }
    }
}
