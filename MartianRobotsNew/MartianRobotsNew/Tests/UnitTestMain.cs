using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartianRobotsNew.Robot;
using MartianRobotsNew.World;
using NUnit.Framework;

namespace MartianRobotsNew.Tests
{
    [TestFixture]
    public class UnitTestMain
    {
        private string _initialization = "5 3";
        private string[] _bot1Instructions = { "1 1 E", "RFRFRFRF" };
        private string[] _bot2Instructions = { "3 2 N", "FRRFLLFFRRFLL" };
        private string[] _bot3Instructions = { "0 3 W", "LLFFFLFLFL" };
        [TestCase]
        public void TestRobots()
        {
            Mars world = Mars.GetInstance().InitializeWorld(_initialization);
            var bot1 = new MarsBot();
            var bot2 = new MarsBot();
            var bot3 = new MarsBot();

            foreach (var inst in _bot1Instructions)
            {
                bot1.ProcessInstructions(inst);
            }

            foreach (var inst in _bot2Instructions)
            {
                bot2.ProcessInstructions(inst);
            }

            foreach (var inst in _bot3Instructions)
            {
                bot3.ProcessInstructions(inst);
            }

            Assert.AreEqual(bot1.State.CurrPosition, new Tuple<int, int>(1, 1));
            Assert.AreEqual(bot2.State.CurrPosition, new Tuple<int, int>(3, 3));
            Assert.AreEqual(bot3.State.CurrPosition, new Tuple<int, int>(2, 3));
        }
    }
}
