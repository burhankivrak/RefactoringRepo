using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldConquerApp.StrategyPattern
{
    public class WorldConquerContext
    {
        private readonly IConquerStrategy _strategy;

        public WorldConquerContext(IConquerStrategy strategy)
        {
            _strategy = strategy;
        }

        public int[,] ExecuteConquer(bool[,] world, int nEmpires, int turns)
        {
            return _strategy.Conquer(world, nEmpires, turns);
        }
    }
}
