using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalDefender.Components.GameSystems
{
    public class GameState
    {
        public int Score { get; set; } = 0;
        public bool IsGameOver { get; set; } = false;
        public PlayerShip Player { get; set; }
        public List<EnemyShip> Invaders { get; set; } = new List<EnemyShip>();
        public List<Player_Projectile> player_Projectiles { get; set; } = new List<Player_Projectile>();

        public GameState()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            Player = new PlayerShip { X = 375, Y = 550, Width = 50, Height = 50 };
            Invaders = GenerateEnemies();
        }

        private List<EnemyShip> GenerateEnemies()
        {
            var enemies = new List<EnemyShip>();
            for (int i = 0; i < 5; i++)
            {
                enemies.Add(new EnemyShip { X = i * 80, Y = 50 });
            }
            return enemies;
        }

        public void UpdateGameState()
        {
            if (IsGameOver) return;


            foreach (var projectile in player_Projectiles.ToList())
            {
                projectile.Move();
                if (projectile.Y < 0 || projectile.Y > 600)
                {
                    player_Projectiles.Remove(projectile);
                    continue;
                }
                CheckHit(projectile);
            }

            foreach (var enemy in Invaders)
            {
                enemy.Move();

                if (enemy.IsAlive && enemy.Y + 40 > Player.Y)
                {
                    IsGameOver = true;
                }
            }
        }

        private void CheckHit(Player_Projectile projectile)
        {
            if (projectile.IsPlayerProjectile)
            {
                foreach (var enemy in Invaders)
                {
                    if (enemy.IsAlive &&
                        projectile.X < enemy.X + 40 &&
                        projectile.X + 10 > enemy.X &&
                        projectile.Y < enemy.Y + 40 &&
                        projectile.Y + 20 > enemy.Y)
                    {
                        enemy.IsAlive = false;
                        Score += 10;
                        player_Projectiles.Remove(projectile);
                        break;
                    }
                }
            }
        }

        private async Task GameLoop()
        {
            while (!IsGameOver)
            {
                UpdateGameState();
                await Task.Delay(16); // ~60 fps
            }
            Console.WriteLine("Game Over");
        }
    }
}
