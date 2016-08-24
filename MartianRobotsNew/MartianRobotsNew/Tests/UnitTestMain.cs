using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartianRobotsNew.World;
using NUnit.Framework;

namespace MartianRobotsNew.Tests
{
    [TestFixture]
    public class UnitTestMain
    {
        private string _initialization = "5 3";
        [TestCase]
        public void TestRobots()
        {
            Mars world1 = Mars.GetInstance().InitializeWorld(_initialization);
            Mars world2 = Mars.GetInstance().InitializeWorld(_initialization);
            Assert.AreEqual(world1, world2);
        }
    }
}
