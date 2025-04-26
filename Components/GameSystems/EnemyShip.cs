using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Enemy Class
// Has: Location data, Alive state, and Movement (incomplete)

namespace FinalDefender.Components.GameSystems
{
    public class EnemyShip
    {
        // Enemy Location
        public int X {  get; set; }
        public int Y { get; set; }

        // Enemy State
        public bool IsAlive { get; set; } = true;

        // Enemy Movement
        public void Move() { }
    }
}
