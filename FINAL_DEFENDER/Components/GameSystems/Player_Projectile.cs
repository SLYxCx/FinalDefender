using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Player projectile Class
// Location data and movement
// (checks if it is the player's projectile as opposed to the enemies
// --The intension was to have different types of projectiles and to maybe use a different projectile for players and enemies

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
