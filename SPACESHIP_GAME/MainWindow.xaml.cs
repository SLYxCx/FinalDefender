using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Threading;

namespace Spaceship_Game
{

    //      TODO
    // Convert background to original gif format for "moving effect"
    // Add asteroids to descend like enemy spacecraft (maybe indestructible)
    // Add shoot capabilities for descending enemies (in other video)
    // Add extra lives
    // Add "boosts" during gameplay that improve the shooting capabilities and movement of the player
    // explosion effects when player hits ship or asteriods and produces sounds
    // GET ENEMY SPACESHIPS TO STOP MOVING INFRONT OF SCORE AND DAMAGE COUNTERS
    // Hitting the different enemy ships gives different points (empty missiles give 0 points and 0 damage)
    // Make it so that only charged missiles do the most damage if they get past the player (like 10)
    // Maybe change it so the enemy spaceships don't deal damage or deal 0.5 damage if he make it past the player
    



    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        bool moveLeft, moveRight;
        List<Rectangle> itemRemover = new List<Rectangle>();

        Random rand = new Random();

        int enemySpriteCounter = 0;
        int enemyCounter = 100;

        // ***********Frenzy mode slows down enemies and boosts your rate of fire and speed
        // Intiated when an object is hit that only comes once every 30 seconds and is very fast (does no damage to player unless player is directly hit)
        int playerSpeed = 10;
        int limit = 50;
        int score = 0;
        int damage = 0;
        int enemySpeed = 10;

        Rect playerHitBox;

        public MainWindow()
        {
            InitializeComponent();

            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

            MyCanvas.Focus();

            ImageBrush bg = new ImageBrush();

            bg.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/space background.gif"));
            bg.TileMode = TileMode.Tile;
            bg.Viewport = new Rect(0, 0, 0.15, 0.15);
            bg.ViewboxUnits = BrushMappingMode.RelativeToBoundingBox;
            MyCanvas.Background = bg;

            //ImageBrush playerImage = new ImageBrush();
            //playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/galaga spaceship.png"));
            //player.Fill = playerImage;

            BitmapImage playerImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/galaga spaceship.png"));


            //player.Width = playerImageSource.PixelWidth;
            //player.Height = playerImageSource.PixelHeight;

            //ImageBrush playerImage = new ImageBrush();
            //playerImage.ImageSource = playerImageSource;
            //player.Fill = playerImage;



            double targetWidth = 70;
            double aspectRatio = (double)playerImageSource.PixelHeight / playerImageSource.PixelWidth;
            player.Width = targetWidth;
            player.Height = targetWidth * aspectRatio;

            ImageBrush playerImage = new ImageBrush(playerImageSource);
            player.Fill = playerImage;
            playerImage.Stretch = Stretch.Uniform;



        }

        private void GameLoop(object sender, EventArgs e)
        {
            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);

            enemyCounter --;

            scoreText.Content = "Score: " + score;
            damageText.Content = "Damage: " + damage;

            if (enemyCounter < 0)
            {
                MakeEnemies();
                enemyCounter = limit;

            }

            //  These set the padding left and right of the player from the boundaries
            // might have to change player rectangle dimensions on main window
            if (moveLeft == true && Canvas.GetLeft(player) > 0) {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - playerSpeed);

            }

            if (moveRight == true && Canvas.GetLeft(player) + 90 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + playerSpeed);

            }

            // Pushes bullets up after creation
            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                if (x is Rectangle && (string)x.Tag == "bullet")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) - 20);

                    Rect bulletHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);


                    if (Canvas.GetTop(x) < 10)
                    {
                        itemRemover.Add(x);
                    }


                    foreach (var y in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if (y is Rectangle && (string)y.Tag == "enemy")
                        {
                            Rect enemyHit = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);

                            if (bulletHitBox.IntersectsWith(enemyHit))
                            {
                                itemRemover.Add(x);

                                // Add explosion and delay here

                                itemRemover.Add(y);
                                score++;
                            }

                        }
                    }

                }



                if (x is Rectangle && (string)x.Tag == "enemy")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + enemySpeed);

                    if (Canvas.GetTop(x) > 750)
                    {
                        itemRemover.Add(x);
                        damage += 1;
                    }

                    Rect enemyHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox.IntersectsWith(enemyHitBox))
                    {
                        itemRemover.Add(x);
                        damage += 10;

                    }

                }


            }


            foreach (Rectangle i in itemRemover)
            {
                MyCanvas.Children.Remove(i);
            }


            if (score > 10)
            {
                limit = 20;
                enemySpeed = 11;
            }

            // Add extra lives somewhere here



            // Change ending to game over screen and button to reset that is not external
            if (damage > 99)
            {
                gameTimer.Stop();
                damageText.Content = "Damage: 100";
                damageText.Foreground = Brushes.Red;
                MessageBox.Show("Game Over! All your ships have been destroyed!\nHighscore: " + score + "\nPress OK to play again.");

                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();

            }



        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                moveLeft = true;
            }

            if (e.Key == Key.Right)
            {
                moveRight = true;
            }


        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                moveLeft = false;
            }

            if (e.Key == Key.Right)
            {
                moveRight = false;
            }

            if (e.Key == Key.Space)
            {
                Rectangle newBullet = new Rectangle
                {
                    Tag = "bullet",
                    Height = 20,
                    Width = 5,
                    Fill = Brushes.White,
                    Stroke = Brushes.Red,
                };

                Canvas.SetLeft(newBullet, Canvas.GetLeft(player) + player.Width / 2);
                Canvas.SetTop(newBullet, Canvas.GetTop(player) - newBullet.Height);

                MyCanvas.Children.Add(newBullet);

            }



        }

        private void MakeEnemies()
        {
            ImageBrush enemySprite = new ImageBrush();



            enemySpriteCounter = rand.Next(1, 6);

            switch (enemySpriteCounter)
            {
                case 1:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/1 new.png"));
                    break;
                case 2:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/2 new.png"));
                    break;
                case 3:
                    // have these missiles move slower than everything else (also rectangle is too big have the image fit properly)
                    //enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/missile uncharged.png"));

                    BitmapImage missileImage = new BitmapImage(new Uri("pack://application:,,,/Images/missile uncharged.png"));

                    Rectangle newMissile = new Rectangle
                    {

                        Tag = "enemy",
                        Width = 20,
                        Height = 20 * (double)missileImage.PixelHeight / missileImage.PixelWidth,
                        Fill = new ImageBrush(missileImage) { Stretch = Stretch.Uniform }
                    };

                    Canvas.SetTop(newMissile, -100);
                    Canvas.SetLeft(newMissile, rand.Next(30, 430)); 
                    MyCanvas.Children.Add(newMissile);



                    break;
                case 4:
                    // have these spaceships move faster than the other ones
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/evil spaceship.png"));
                    break;
                case 5:
                    // have these do more damage than the uncjarged missiles
                    //enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/missile charged.png"));

                    BitmapImage missileImage2 = new BitmapImage(new Uri("pack://application:,,,/Images/missile charged.png"));

                    Rectangle newMissile2 = new Rectangle
                    {

                        Tag = "enemy",
                        Width = 20,  
                        Height = 20 * (double)missileImage2.PixelHeight / missileImage2.PixelWidth, 
                        Fill = new ImageBrush(missileImage2) { Stretch = Stretch.Uniform }
                    };


                    Canvas.SetTop(newMissile2, -100);
                    Canvas.SetLeft(newMissile2, rand.Next(30, 430));
                    MyCanvas.Children.Add(newMissile2);


                    break;
            }

            Rectangle newEnemy = new Rectangle
            {
                Tag = "enemy",
                Height = 50,
                Width = 50,
                Fill = enemySprite
            };



            Canvas.SetTop(newEnemy, -100);
            Canvas.SetLeft(newEnemy, rand.Next(30, 430));       // Position on x axis enemy spawns we can change this to limit where the enemy will spawn
            MyCanvas.Children.Add(newEnemy);



        }


    }






        //<Image gif:ImageBehavior.AnimatedSource="pack://application:,,,/Images/background 2.gif" Stretch="Fill" Panel.ZIndex="0" IsHitTestVisible="False" Height="599" Width="540" HorizontalAlignment="Center" VerticalAlignment="Center" RenderTransformOrigin="0.5,0.5">
        //    <Image.RenderTransform>
        //        <TransformGroup>
        //            <ScaleTransform/>
        //            <SkewTransform/>
        //            <RotateTransform Angle = "180" />
        //            < TranslateTransform />
        //        </ TransformGroup >
        //    </ Image.RenderTransform >
        //</ Image >




}
