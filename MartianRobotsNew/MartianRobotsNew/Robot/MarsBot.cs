using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using MartianRobotsNew.World;
using NUnit.Framework;

namespace MartianRobotsNew.Robot
{
    public class MarsBot
    {
        private Mars _world;

        public MarsBot(int id)
        {
            ID = id;
            Active = true;
            State = new State
            {
                CurrPosition = new Tuple<int, int>(0, 0)
            };
            _world = Mars.GetInstance();
        }

        public bool Active { get; set; }
        public State State { get; set; }
        public int ID { get; set; }

        public void ProcessInstructions(string instructionString)
        {
            if (Active) //Ignore if robot has fallen off surface
            {
                try
                {
                    var arrInstructionString = instructionString.Split(' ');
                    var chrInstructionString = instructionString.ToCharArray();
                    int tmp;
                    var isPosition = instructionString.Contains(" ") &&
                                      int.TryParse(arrInstructionString[0], out tmp) &&
                                      int.TryParse(arrInstructionString[1], out tmp) &&
                                      new[]
                                      {
                                          Orientation.N.ToString(),
                                          Orientation.E.ToString(),
                                          Orientation.S.ToString(),
                                          Orientation.W.ToString()
                                      }.Contains(arrInstructionString[2]);
                    var isNavigation = !instructionString.Contains(" ") &&
                                        chrInstructionString
                                            .Any(
                                                c =>
                                                    new[]
                                                    {
                                                        Direction.F.ToString(), Direction.L.ToString(),
                                                        Direction.R.ToString()
                                                    }.Contains(c.ToString()));
                    if (isPosition)
                    {
                        if (Active)
                        {
                            var orientation = (Orientation) Enum.Parse(typeof(Orientation), arrInstructionString[2]);
                            MoveTo(int.Parse(arrInstructionString[0]), int.Parse(arrInstructionString[1]), orientation);
                        }
                    }
                    else if (isNavigation)
                    {
                        foreach (var c in chrInstructionString)
                        {
                            if (Active)
                            {
                                var direction = (Direction) Enum.Parse(typeof(Direction), c.ToString());
                                RotateOrMove(direction);
                            }
                        }
                    }

                }
                catch (Exception e)
                {
                    throw;
                } 
            }
        }

        public void RotateOrMove(Direction direction)
        {
            var num = (int)State.CurrOrientation;
            var numL = 0;
            switch(num)
            {
                case 0:
                    numL = 3;
                    break;
                default:
                    numL = num - 1;
                    break;
            }
            switch (direction)
            {
                case Direction.R:
                    State.CurrOrientation = (Orientation)((num + 1) % 4);
                    break;
                case Direction.L:
                    State.CurrOrientation = (Orientation)(numL);
                    break;
                case Direction.F:
                    Move();
                    break;

            }
        }

        public void Move()
        {
            switch (State.CurrOrientation)
            {
                case Orientation.E:
                    MoveTo(State.CurrPosition.Item1 + 1, State.CurrPosition.Item2, Orientation.E);
                    break;
                case Orientation.N:
                    MoveTo(State.CurrPosition.Item1, State.CurrPosition.Item2 + 1, Orientation.N);
                    break;
                case Orientation.S:
                    MoveTo(State.CurrPosition.Item1, State.CurrPosition.Item2 - 1, Orientation.S);
                    break;
                case Orientation.W:
                    MoveTo(State.CurrPosition.Item1 - 1, State.CurrPosition.Item2, Orientation.W);
                    break;
            }  
        }

        public void MoveTo(int x, int y, Orientation orientation)
        {
            if (Active && !_world.PointIsScented(x, y))
            {
                State.PrevPosition = State.CurrPosition;
                State.CurrPosition = new Tuple<int, int>(x, y);
                State.PrevOrientation = State.CurrOrientation;
                State.CurrOrientation = orientation;
                if (x > _world.XRange || y > _world.YRange)
                {
                    _world.MarkPointScented(State.PrevPosition.Item1, State.PrevPosition.Item2);
                    State.CurrPosition = State.PrevPosition;
                    State.CurrOrientation = State.PrevOrientation;
                    Active = false;
                }
            }
        }
    }
}
