using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace snake_Game
{
    /// <summary>
    /// Logika interakcji dla klasy MediumMapWindow.xaml
    /// </summary>
    public partial class MediumMapWindow : Window
    {
        private int rows = 18, cols = 18;
        private Image[,] imagesGrid;

        private Dictionary<AreaValue, ImageSource> areaValueToImage = new()
        {
            { AreaValue.EmptyArea,  VisualSidePhotos.EmptyField },
            { AreaValue.BodySnake, VisualSidePhotos.SnakeBody },
            { AreaValue.Food, VisualSidePhotos.FoodImage },
        };

        private Dictionary<Direction, int> directionToRotate = new()
        {
            { Direction.Up, 0 },
            { Direction.Down, 180 },
            { Direction.Left, 270 },
            { Direction.Right, 90 }
        };

        private StateOfTheGame gameStateRightNow;
        private bool isGameOn;
        public MediumMapWindow()
        {
            InitializeComponent();
            imagesGrid = AddImagesToGrid();
            gameStateRightNow = new StateOfTheGame(rows, cols);
        }

        private async Task RunTheGame()
        {
            MakeTheTable();
            await GetReadyCountDown();
            StartOverLay.Visibility = Visibility.Hidden;
            await GameLoop();
            await GameOverScreen();
            gameStateRightNow = new StateOfTheGame(rows, cols);
        }

        private async void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (StartOverLay.Visibility == Visibility.Visible)
            {
                e.Handled = true;
            }

            if (!isGameOn)
            {
                isGameOn = true;
                await RunTheGame();
                isGameOn = false;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameStateRightNow.isGameOver)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Left:
                    gameStateRightNow.ChangeDir(Direction.Left);
                    break;

                case Key.Right:
                    gameStateRightNow.ChangeDir(Direction.Right);
                    break;

                case Key.Up:
                    gameStateRightNow.ChangeDir(Direction.Up);
                    break;

                case Key.Down:
                    gameStateRightNow.ChangeDir(Direction.Down);
                    break;

            };


        }

        private async Task GameLoop()
        {
            while (!gameStateRightNow.isGameOver)
            {
                await Task.Delay(70);
                gameStateRightNow.Run();
                MakeTheTable();
            }
        }

        private Image[,] AddImagesToGrid()
        {
            Image[,] images = new Image[rows, cols];
            GamePlace.Rows = rows;
            GamePlace.Columns = cols;
            GamePlace.Width = GamePlace.Height * (cols / (double)rows);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Image image = new Image
                    {
                        Source = VisualSidePhotos.EmptyField,
                        RenderTransformOrigin = new Point(0.5, 0.5),
                    };

                    images[i, j] = image;
                    GamePlace.Children.Add(image);
                }
            }

            return images;
        }

        private void MakeTheTable()
        {
            CreateGridLook();
            MakeTheSnakeHead();
            ScoreTxt.Text = $"SCORE: {gameStateRightNow.Score}";
        }


        private void CreateGridLook()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    AreaValue gridValue = gameStateRightNow.Area[i, j];
                    imagesGrid[i, j].Source = areaValueToImage[gridValue];
                    imagesGrid[i, j].RenderTransform = Transform.Identity;
                }
            }
        }

        private void MakeTheSnakeHead()
        {
            Position headPosition = gameStateRightNow.HeadPos();
            Image headIm = imagesGrid[headPosition.Row, headPosition.Column];
            headIm.Source = VisualSidePhotos.SnakeHead;

            int rotationValue = directionToRotate[gameStateRightNow.direction];
            headIm.RenderTransform = new RotateTransform(rotationValue);
        }

        private async Task MakeDeadSnake()
        {
            List<Position> positions = new List<Position>(gameStateRightNow.SnakePos());

            for (int i = 0; i < positions.Count; i++)
            {
                Position pos = positions[i];
                ImageSource src = (i == 0) ? VisualSidePhotos.CrashHead : VisualSidePhotos.CrashBody;
                imagesGrid[pos.Row, pos.Column].Source = src;

                await Task.Delay(100);
            }
        }

        private async Task GetReadyCountDown()
        {
            for (int i = 3; i >= 1; i--)
            {
                IntroText.Text = i.ToString();
                await Task.Delay(700);
            }

        }

        private async Task GameOverScreen()
        {
            await MakeDeadSnake();
            MessageBox.Show("You have lost!");
            await Task.Delay(700);
            StartOverLay.Visibility = Visibility.Visible;
            IntroText.Text = "PRESS KEY TO START";
        }

    }
}