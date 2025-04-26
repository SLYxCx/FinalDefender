using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Player Class
// Has location, movement, and firing
// --Inteded to make a "Character" interface or set up an abstract class

namespace FinalDefender.Components.GameSystems
{
    public class PlayerShip
    {
        // Location Information
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        // Movement
        public void MoveLeft() 
        {
            X -= 5;
        }
        public void MoveRight() 
        {
            X += 5;
        }

        // Shoot
        public Player_Projectile Fire()
            {
                return new Player_Projectile { X = this.X, Y = this.Y, IsPlayerProjectile = true };
            }
    }
}
