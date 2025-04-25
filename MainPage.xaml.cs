
using FinalDefender.Components.GameSystems;

namespace FinalDefender
{
    public partial class MainPage : ContentPage
    {
        public Components.GameSystems.GameState GameState { get; set; } = new Components.GameSystems.GameState
        {
            Player = new PlayerShip { X = 375, Y = 550, Width = 50, Height = 50 },
            Invaders = InitializeEnemies(),
        };
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private static List<EnemyShip> InitializeEnemies()
        {
            var enemies = new List<EnemyShip>();
            for (int i = 0; i < 5; i++)
            {
                enemies.Add(new EnemyShip { X = i * 80, Y = 0 });
            }
            return enemies;
        }
    }
}
