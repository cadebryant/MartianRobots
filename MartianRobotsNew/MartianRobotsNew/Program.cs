using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartianRobotsNew.Robot;
using MartianRobotsNew.World;

namespace MartianRobotsNew
{
    class Program
    {
        private static string _initialization = "5 3";
        private static readonly string[] _bot1Instructions = { "1 1 E", "RFRFRFRF" };
        private static readonly string[] _bot2Instructions = { "3 2 N", "FRRFLLFFRRFLL" };
        private static readonly string[] _bot3Instructions = { "0 3 W", "LLFFFLFLFL" };

        static void Main(string[] args)
        {
            Mars world = Mars.GetInstance().InitializeWorld(_initialization);
            var bot1 = new MarsBot(1);
            var bot2 = new MarsBot(2);
            var bot3 = new MarsBot(3);
            var bots = new List<MarsBot>();

            foreach (var inst in _bot1Instructions)
            {
                bot1.ProcessInstructions(inst);
            }
            bots.Add(bot1);

            foreach (var inst in _bot2Instructions)
            {
                bot2.ProcessInstructions(inst);
            }
            bots.Add(bot2);

            foreach (var inst in _bot3Instructions)
            {
                bot3.ProcessInstructions(inst);
            }
            bots.Add(bot3);

            foreach (var bot in bots)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("{0} {1} {2}", bot.State.CurrPosition.Item1, bot.State.CurrPosition.Item2, bot.State.CurrOrientation);
                if (!bot.Active)
                {
                    sb.Append(" LOST");
                }
                Console.WriteLine(sb.ToString());
            }
            Console.ReadLine();
        }
    }
}
