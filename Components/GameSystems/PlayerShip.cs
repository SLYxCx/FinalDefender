using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void MoveLeft() { /* Logic for moving left */ }
        public void MoveRight() { /* Logic for moving right */ }

        // Shoot
        public Player_Projectile Fire()
            {
                return new Player_Projectile { X = this.X, Y = this.Y, IsPlayerProjectile = true };
            }
    }
}
