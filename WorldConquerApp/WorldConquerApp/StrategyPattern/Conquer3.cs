using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldConquerApp.StrategyPattern
{
    public class Conquer3 : IConquerStrategy
    {
        public int[,] Conquer(bool[,] world, int nEmpires, int turns)
        {
            WorldConquer worldConquer = new WorldConquer(world);
            return worldConquer.Conquer3(nEmpires, turns);
        }
    }
}
