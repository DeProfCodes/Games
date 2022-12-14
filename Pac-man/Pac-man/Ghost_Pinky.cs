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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pac_man
{
    class Ghost_Pinky:Ghost_Inky
    {
        private List<Button> path;
        private static int direction;
        private Walls wall;

        bool top;
        bool left;
        bool LEFT;
        bool RIGHT;
        bool DOWN;
        bool TOP;

        public Ghost_Pinky(Walls wall) : base(wall)
        {
            this.wall = wall;
            path = new List<Button>();
            direction = 0;
            top = false;
            left = false;
            LEFT = false;
            RIGHT = false;
            DOWN = false;
            TOP = false;
        }

        public void pinky_starting_Pos()
        {
            Controller.SetSpritePosition(2, 2.15, Pinky);
            direction = 0;
            Pinky.Source = myGhosts[1];
        }
        
        public void move_Pinky()
        {
            Controller.Move_Sprite(direction, Pinky);
        }
        
        void update_Bools(int i)
        {
            top = Math.Abs(Coordinates.GetTopPosition(Pinky) - Coordinates.GetTopPosition(path[i])) < Pinky.ActualHeight;
            left = Math.Abs(Coordinates.GetLeftPosition(Pinky) - Coordinates.GetLeftPosition(path[i])) < Pinky.ActualWidth;
            LEFT = Coordinates.GetLeftPosition(Pinky) >= Coordinates.GetLeftPosition(path[i]) && top && left;
            RIGHT = Coordinates.GetLeftPosition(Pinky) <= Coordinates.GetLeftPosition(path[i]) && top && left;
            TOP = Coordinates.GetTopPosition(Pinky) >= Coordinates.GetTopPosition(path[i]) && top && left;
            DOWN = Coordinates.GetTopPosition(Pinky) <= Coordinates.GetTopPosition(path[i]) && top && left;
        }

        public void Pinky_path()
        {
            //TURNING POINT CO_ORDINATES
            wall.path_Layout(2.84, 2, path);
            wall.path_Layout(2.84, 2.3 , path);
            wall.path_Layout(3.8, 2.3, path);
            wall.path_Layout(3.8, 3, path);
            wall.path_Layout(6, 3, path);
            wall.path_Layout(6, 1.09, path);
            wall.path_Layout(3.8, 1.09, path);
            wall.path_Layout(3.8, 1.35, path);
            wall.path_Layout(2.22, 1.35, path);
            wall.path_Layout(2.22, 3, path);
            wall.path_Layout(1.575, 3, path);
            wall.path_Layout(1.575, 4.5, path);
            wall.path_Layout(1.22, 4.5, path);
            wall.path_Layout(1.22, 20, path);
            wall.path_Layout(1.09, 20, path);
            wall.path_Layout(1.09, 1.09, path);
            wall.path_Layout(1.2, 1.09, path);
            wall.path_Layout(1.2, 1.185, path);
            wall.path_Layout(1.35, 1.185, path);
            wall.path_Layout(1.35, 1.09, path);
            wall.path_Layout(1.55, 1.09, path);
            wall.path_Layout(1.55, 1.35, path);
            wall.path_Layout(1.38, 1.35, path);
            wall.path_Layout(1.38, 1.85, path);
            wall.path_Layout(1.55, 1.85, path);
            wall.path_Layout(1.55, 1.56, path);
            wall.path_Layout(2.84, 1.56, path);
        }

        void direct_Pinky()
        {
            for (int i = 0; i < path.Count; i++)
            {
                update_Bools(i);
                bool i0 = (i == 0 || i==2 || i==16 || i==20 || i==26);
                bool i1 = (i == 1 || i == 3 || i == 17 || i == 23);
                bool i4 = (i == 4 || i == 18 || i == 24);
                bool i5 = (i == 5 || i == 9);
                bool i6 = (i == 6 || i == 10 || i == 12 || i == 22);
                bool i7 = (i == 7 || i == 11 || i == 13 || i == 21);
                bool i8 = (i == 8 || i == 14);
                bool i15 = (i == 15 || i == 19 || i == 25);

                if (DOWN && i0) direction = 3;
                if (RIGHT && i1) direction = 0;
                if (DOWN && i4) direction = 1;
                if (LEFT && i5) direction = 2;
                if (TOP && i6) direction = 3;
                if (RIGHT && i7) direction = 2;
                if (TOP && i8) direction = 1;
                if (LEFT && i15) direction = 0;
            }
        }

        public void clear_pinky_path()
        {
            for (int i = 0; i < path.Count; i++)
            {
                GUI.GetBoard().Children.Remove(path[i]);
            }
        }

        public void Pinky_Advance()
        {
            direct_Pinky();
            move_Pinky();
            Controller.Reset_Sprite_Exit_Wall(Pinky);
        }
    }
}
