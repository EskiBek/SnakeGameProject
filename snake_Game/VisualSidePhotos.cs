using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace snake_Game
{
    public static class VisualSidePhotos
    {
        public static ImageSource EmptyField = LoadImage("EmptyField.png");
        public static ImageSource SnakeHead = LoadImage("HeadSnake.png");
        public static ImageSource SnakeBody = LoadImage("BodySnake.png");
        public static ImageSource CrashHead = LoadImage("HeadCrash.png");
        public static ImageSource CrashBody = LoadImage("BodyCrash.png");
        public static ImageSource FoodImage = LoadImage("PineappleFood.png");

        private static ImageSource LoadImage(string fileName)
        {
            return new BitmapImage(new Uri($"Assets/{fileName}", UriKind.Relative));
        }
    }
}
