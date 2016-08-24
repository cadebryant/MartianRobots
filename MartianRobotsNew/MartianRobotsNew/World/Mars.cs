using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Constraints;

namespace MartianRobotsNew.World
{
    public class Mars
    {
        private static Mars _instance;

        private Mars()
        {
        }

        /// <summary>
        /// Singleton getter.
        /// </summary>
        /// <returns></returns>
        public static Mars GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Mars();
            }
            return _instance;
        }

        public int XRange { get; set; }
        public int YRange { get; set; }

        //Key is the coordinate point (represented by a Tuple).
        //Value indicates whether or not the point is "scented" (default is false).
        public Dictionary<Tuple<int, int>, bool> Surface { get; set; }

        /// <summary>
        /// Initializes the surface
        /// </summary>
        /// <param name="initializationString">X and Y boundaries (e.g. "5 3")</param>
        public Mars InitializeWorld(string initializationString)
        {
            var arrInit = initializationString.Split(' ');
            try
            {
                int xRange;
                int yRange;
                if (int.TryParse(arrInit[0], out xRange) && int.TryParse(arrInit[1], out yRange))
                {
                    return InitializeWorld(xRange, yRange);
                }
                return null;
            }
            catch (Exception)
            {
                throw new ArgumentException("Input must be of form \"int int \"");
            }
        }

        public Mars InitializeWorld(int xRange, int yRange)
        {
            if (xRange >= MaxSize || yRange >= MaxSize)
            {
                throw new ArgumentException(string.Format("xRange or yRange cannot be larger than {0}.", MaxSize));
            }

            _instance.XRange = xRange;
            _instance.YRange = yRange;
            _instance.Surface = new Dictionary<Tuple<int, int>, bool>();
            for (var i = 0; i < XRange; i++)
            {
                for (var j = 0; j < YRange; j++)
                {
                    _instance.Surface[new Tuple<int, int>(i, j)] = false;
                }
            }
            return _instance;
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
