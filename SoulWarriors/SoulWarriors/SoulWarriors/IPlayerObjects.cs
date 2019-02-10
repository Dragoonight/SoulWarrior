using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoulWarriors
{
    interface IPlayerObjects
    {
        int Health { get; set; }
        int SMeter { get; set; }
    }

   
    class PlayCharacters : IPlayerObjects
    {
        public int Health { get; set; }

        public int SMeter { get; set; }

    }

    
}
