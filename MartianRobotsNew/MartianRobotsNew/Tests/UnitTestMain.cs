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
        private readonly string[] _bot1Instructions = { "1 1 E", "RFRFRFRF" };
        private readonly string[] _bot2Instructions = { "3 2 N", "FRRFLLFFRRFLL" };
        private readonly string[] _bot3Instructions = { "0 3 W", "LLFFFLFLFL" };
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

            Assert.AreEqual(new Tuple<int, int>(1, 1), bot1.State.CurrPosition);
            Assert.AreEqual(Orientation.E, bot1.State.CurrOrientation);
            Assert.AreEqual(new Tuple<int, int>(3, 3), bot2.State.CurrPosition);
            Assert.AreEqual(Orientation.N, bot2.State.CurrOrientation);
            Assert.AreEqual(new Tuple<int, int>(2, 3), bot3.State.CurrPosition);
            Assert.AreEqual(Orientation.S, bot3.State.CurrOrientation);
        }
    }
}
