using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldConquerApp.StrategyPattern
{
    public interface IConquerStrategy
    {
        int[,] Conquer(bool[,] world, int nEmpire, int turns);
    }
}
