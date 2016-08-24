using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobotsNew.Robot
{
    public class State
    {
        public Tuple<int, int> CurrPosition { get; set; }
        public Tuple<int, int> PrevPosition { get; set; }
        public Orientation CurrOrientation { get; set; }
        public Orientation PrevOrientation { get; set; }
    }
}
