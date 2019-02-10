using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoulWarriors
{
    interface IEnemyObjects
    {
        int Health { get; set; }
    }

    class EnemyCharacters : IEnemyObjects
    {
        public int Health { get; set; }
    }
}
