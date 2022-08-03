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
using System.Windows.Threading;

namespace Pac_man
{
    class Ghost_Inky:Ghost_Clyde
    {
        private List<Button> path;
        private Walls wall;
        private static int direction;

        bool top;
        bool left;
        bool LEFT;
        bool RIGHT;
        bool DOWN;
        bool TOP;

        public Ghost_Inky(Walls wall) : base(wall)
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

        public void inky_starting_Pos()
        {
            Controller.SetSpritePosition(2.3, 2.15, Inky);
            direction = 0;
            Inky.Source = myGhosts[0];
        }

        public void move_Inky()
        {
            Controller.Move_Sprite(direction, Inky);
        }
        
        void update_Bools(int i)
        {
            top = Math.Abs(Coordinates.GetTopPosition(Inky) - Coordinates.GetTopPosition(path[i])) < Inky.ActualHeight;
            left = Math.Abs(Coordinates.GetLeftPosition(Inky) - Coordinates.GetLeftPosition(path[i])) < Inky.ActualWidth;
            LEFT = Coordinates.GetLeftPosition(Inky) >= Coordinates.GetLeftPosition(path[i]) && top && left;
            RIGHT = Coordinates.GetLeftPosition(Inky) <= Coordinates.GetLeftPosition(path[i]) && top && left;
            TOP = Coordinates.GetTopPosition(Inky) >= Coordinates.GetTopPosition(path[i]) && top && left;
            DOWN = Coordinates.GetTopPosition(Inky) <= Coordinates.GetTopPosition(path[i]) && top && left;
        }

        public void Inky_path()
        {
            //TURNING POINT CO_ORDINATES
            wall.path_Layout(2.38, 2.3, path);
            wall.path_Layout(2.38, 2.1, path);
            wall.path_Layout(2.84, 2.1, path);
            wall.path_Layout(2.84,3,path);
            wall.path_Layout(1.57, 3, path);
            wall.path_Layout(1.57, 2.35, path);
            wall.path_Layout(1.365, 2.35, path);
            wall.path_Layout(1.365, 3, path);
            wall.path_Layout(1.22, 3, path);
            wall.path_Layout(1.22, 2.35, path);
            wall.path_Layout(1.09, 2.35, path);
            wall.path_Layout(1.09, 20, path);
            wall.path_Layout(1.21, 20, path);
            wall.path_Layout(1.21, 8, path);
            wall.path_Layout(1.365, 8, path);
            wall.path_Layout(1.365, 20, path);
            wall.path_Layout(1.57, 20, path);
            wall.path_Layout(1.57, 4.6, path);
            wall.path_Layout(2.2, 4.6, path);
            wall.path_Layout(2.23, 1.35, path);
            wall.path_Layout(6, 1.35, path);
            wall.path_Layout(6, 1.55, path);
            wall.path_Layout(3.8, 1.55, path);
            wall.path_Layout(3.8, 1.85, path);
            wall.path_Layout(2.87, 1.85, path);
        }

        void direct_Inky()
        {
            for (int i = 0; i < path.Count; i++)
            {
                update_Bools(i);
                bool i0 = (i == 0 || i == 12 || i == 16);
                bool i1 = (i == 1 || i == 13 || i == 17);
                bool i2 = (i == 2 || i == 14 || i == 18 || i==20);
                bool i3 = (i == 3 || i == 7 || i == 21 || i == 23);
                bool i4 = (i == 4 || i == 8);
                bool i5 = (i == 5 || i == 9);
                bool i6 = (i == 6 || i == 10 || i == 22 || i == 24);
                bool i11 = (i == 11 || i == 15 || i == 19);

                if (DOWN && i0) direction = 1;
                if (LEFT && i1) direction = 0;
                if (DOWN && i2) direction = 3;
                if (RIGHT && i3) direction = 2;
                if (TOP && i4) direction = 1;
                if (LEFT && i5) direction = 2;
                if (TOP && i6) direction = 3;
                if (RIGHT && i11) direction = 0;
             }
        }

        public void clear_inky_path()
        {
            for (int i = 0; i < path.Count; i++)
            {
                GUI.GetBoard().Children.Remove(path[i]);
            }
        }

        public void Inky_Advance()
        {
            //color_Inky_path();
            direct_Inky();
            move_Inky();
            Controller.Reset_Sprite_Exit_Wall(Inky);
        }
    }
}
