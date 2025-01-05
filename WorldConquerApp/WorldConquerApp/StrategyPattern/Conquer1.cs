using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldConquerApp.StrategyPattern
{
    public class Conquer1 : IConquerStrategy
    {
        public int[,] Conquer(bool[,] world, int nEmpires, int turns)
        {
            WorldConquer worldConquer = new WorldConquer(world);
            return worldConquer.Conquer1(nEmpires, turns);
        }
    }
}
