using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartianRobotsNew.World;

namespace MartianRobotsNew.Robot
{
    public class MarsBot
    {
        private Mars _world;

        public MarsBot()
        {
            Active = true;
        }
        public bool Active { get; set; }

    }
}
