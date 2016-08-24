using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobotsNew.World
{
    public class Mars
    {
        private static Mars _instance;

        private Mars(int xRange, int yRange)
        {
            if (xRange > MaxSize || yRange > MaxSize)
            {
                throw new ArgumentException(string.Format("Range cannot be larger than {0}.", MaxSize));
            }
            _instance.XRange = xRange;
            _instance.YRange = yRange;
            InitializeWorld();
        }

        /// <summary>
        /// Singleton getter.
        /// </summary>
        /// <param name="xRange">Boundary of world's x-axis.</param>
        /// <param name="yRange">Boundary of world's y-axis.</param>
        /// <returns></returns>
        public static Mars GetInstance(int xRange, int yRange)
        {
            if (_instance == null)
            {
                _instance = new Mars(xRange, yRange);
            }
            return _instance;
        }

        public int XRange { get; set; }
        public int YRange { get; set; }
        
        //Key is the coordinate point (represented by a Tuple).
        //Value indicates whether or not the point is "scented" (default is false).
        public Dictionary<Tuple<int, int>, bool> Surface { get; set; }

        public void InitializeWorld()
        {
            Surface = new Dictionary<Tuple<int, int>, bool>();
            for (var i = 0; i < XRange; i++)
            {
                for (var j = 0; j < YRange; j++)
                {
                    Surface[new Tuple<int, int>(i, j)] = false;
                }
            }
        }

        /// <summary>
        /// Indicates that a robot has "fallen off" the world beyond this point.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MarkPointScented(int x, int y)
        {
            Surface[new Tuple<int, int>(x, y)] = true;
        }

        public static int MaxSize = 50;
    }
}
