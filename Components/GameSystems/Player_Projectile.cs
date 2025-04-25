using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalDefender.Components.GameSystems
{
    public class Player_Projectile
    {
        // Projectile Location
        public int X {  get; set; }
        public int Y { get; set; }
        public bool IsPlayerProjectile { get; set; }

        // Projectile Movement
        public void Move()
        {
            if (IsPlayerProjectile)
            {
                Y -= 10; // Upward Movement
            }
        }
    }
}
