using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace WpfCosmosVersion1
{
    class CosmosEventHandler
    {
        public Rocket rocket;
        private SpaceContinuum spaceContinuum;
        private DispatcherTimer gameTick;
        private MainWindow mainWindow;
        private FramePerSecondCounter fps = new FramePerSecondCounter();
        private Random random = new Random();
        private Canvas canvas;
        private GameInput gameInput;
        private bool isGravityOn = false;

        public int FPS { get; set; }

        public CosmosEventHandler(Canvas canvasGame, MainWindow mainWindow)
        {
            gameTick = new DispatcherTimer();
            gameTick.Interval = TimeSpan.FromMilliseconds(15);
            gameTick.Tick += Timer_Tick;

            this.mainWindow = mainWindow;
            canvas = canvasGame;
            canvas.Focusable = true;

            //rocket = new Rocket(new Vector((float)(canvas.ActualWidth * 0.5), (float)(canvas.ActualHeight * 0.8)), canvas);
            gameInput = new GameInput();
            rocket = new Rocket(new Vector((float)(200), (float)(200)), canvas, gameInput);
            spaceContinuum = new SpaceContinuum(canvas);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            spaceContinuum.Update(0.0167, isGravityOn);
            spaceContinuum.HandleRocket(rocket.Update(0.0167, spaceContinuum.SpaceObjects));
            gameInput.fire = false;
            mainWindow.labelFPSDynamic.Content = fps.FPS;
            fps.IncrementFrameCount();
        }

        public void buttonAddPlanet_Click(object sender, RoutedEventArgs e)
        {
            spaceContinuum.AddPlanet(random.Next(50, 100), random.Next(25, 75), new Vector((float)random.Next(75, (int)(canvas.ActualWidth - 75)), (float)random.Next(75, (int)(canvas.ActualHeight - 75))), new Vector((float)random.Next(-100, 100), (float)random.Next(-100, 100)));
        }

        public void buttonAddAsteroid_Click(object sender, RoutedEventArgs e)
        {
            spaceContinuum.AddAsteroid(random.Next(0, 50), random.Next(0, 25), new Vector((float)random.Next(25, (int)(canvas.ActualWidth - 25)), (float)random.Next(25, (int)(canvas.ActualHeight - 25))), new Vector((float)random.Next(-100, 100), (float)random.Next(-100, 100)));
        }
        public void buttonGravity_Click(object sender, RoutedEventArgs e)
        {
            if (isGravityOn)
            {
                isGravityOn = false;
                mainWindow.buttonGravity.Content = "Gravity on";
            }
            else
            {
                isGravityOn = true;
                mainWindow.buttonGravity.Content = "Gravity off";
            }
        }

        public void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            gameTick.Start();
        }

        public void buttonStop_Click(object sender, RoutedEventArgs e)
        {
            gameTick.Stop();
        }

        public void buttonRemovePlanets_Click(object sender, RoutedEventArgs e)
        {
            spaceContinuum.reset();
        }

        public void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            spaceContinuum.Update(1, isGravityOn);
            rocket.Update(1, spaceContinuum.SpaceObjects);
        }

        public void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Close();
        }

        public void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            canvas.Focus();
            e.Handled = true;
            switch (e.Key)
            {
                case Key.Up:
                    gameInput.up = true;
                    break;
                case Key.Down:
                    gameInput.down = true;
                    break;
                case Key.Left:
                    gameInput.left = true;
                    break;
                case Key.Right:
                    gameInput.right = true;
                    break;
                case Key.Space:
                    if (!e.IsRepeat)
                    {
                        gameInput.fire = true;
                    }
                    break;
                case Key.X:
                    gameInput.fire = true;
                    break;
                case Key.Escape:
                    mainWindow.Close();
                    break;
            }
        }

        public void OnKeyUpHandler(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            switch (e.Key)
            {
                case Key.Up:
                    gameInput.up = false;
                    break;
                case Key.Down:
                    gameInput.down = false;
                    break;
                case Key.Left:
                    gameInput.left = false;
                    break;
                case Key.Right:
                    gameInput.right = false;
                    break;
                case Key.Space:
                    gameInput.fire = false;
                    break;
                case Key.X:
                    gameInput.fire = false;
                    break;
            }
        }

    }
}
