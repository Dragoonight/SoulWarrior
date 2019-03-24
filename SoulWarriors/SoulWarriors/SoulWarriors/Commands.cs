using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using XNAGameConsole;

namespace SoulWarriors
{
    class BeepCommand : IConsoleCommand
    {
        public string Name { get; } = "beep";
        public string Description { get; } = "boop";

        public string Execute(string[] arguments)
        {
            try
            {
                if (arguments.Length != 0)
                {
                    Console.Beep(int.Parse(arguments[0]), int.Parse(arguments[1]));
                }
                else Console.Beep();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return e.ToString();
            }
            return "boop";
        }
    }
}
