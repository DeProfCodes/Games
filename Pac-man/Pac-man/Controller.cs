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
using System.Timers;

namespace Pac_man
{
    class Controller:Window
    {
        private static Canvas board;
        Constraints constraints;
        public Player P;
        Food food;
        Image pMan;
        public Timer timer2 { get; set; }
        public bool special_Food { get; set; }
        public string score { get; set; }
        Dispatcher h;
        List<Button> wall;
        private static bool pacman_eat_ghost;
        private static int direction;

        public Controller(Player P, Timer timer2, List<Button> wall, Food food)
        {
            board = GUI.GetBoard();
            this.P = P;
            this.food = food;
            pMan = Player.GetPacman();
            this.wall = wall;
            constraints = new Constraints(P, wall, food.theFood);
            special_Food = false;

            pacman_eat_ghost = false;
            direction = 3;

            h = Dispatcher.CurrentDispatcher;
            this.timer2 = timer2;
            this.timer2.Enabled = false;
            this.timer2.Interval = 100;
            this.timer2.Elapsed += Timer2_Elapsed;
        }

        private void Timer2_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                h.Invoke(new Action(() =>
               {
                   Move_Sprite(direction ,pMan);
                   if (constraints.Is_Food_Eaten()) special_Food = true; 
                   P.Advance();
                   score = constraints.SCORE;
               }));
            }
            catch(Exception)
            {
                return;
            }
        }

        public void rePostion_Sprite_wallHit(Image pMan)
        {
            if (constraints.isBotWall)
            {
                Canvas.SetTop(pMan, Canvas.GetTop(pMan) - 5);
                timer2.Enabled = true;
            }
            if (constraints.isTopWall)
            {
                Canvas.SetTop(pMan, Canvas.GetTop(pMan) + 5);
                timer2.Enabled = true;
            }
            if (constraints.isLeftWall)
            {
                Canvas.SetLeft(pMan, Canvas.GetLeft(pMan) + 5);
                timer2.Enabled = true;
            }
            if (constraints.isRightWall)
            {
                Canvas.SetLeft(pMan, Canvas.GetLeft(pMan) - 5);
                timer2.Enabled = true;
            }
            constraints.isBotWall = false;
            constraints.isTopWall = false;
            constraints.isLeftWall = false;
            constraints.isRightWall = false;
        }

        public static void Reset_Sprite_Exit_Wall(Image pMan)
        {
            if (Canvas.GetLeft(pMan) < 0)
            {
                Canvas.SetLeft(pMan, board.ActualWidth / 1.045);
            }
            if (Canvas.GetLeft(pMan) + pMan.ActualWidth > board.ActualWidth)
            {
                Canvas.SetLeft(pMan, 0);
            }
        }

        void Wall_Hit_bugs_Fix(int direction)
        {
            if (constraints.isTopWall && direction != 0)
            {
                Canvas.SetTop(pMan, Canvas.GetTop(pMan) + 5);
                timer2.Enabled = true;
                constraints.isTopWall = false;
            }
            if (constraints.isRightWall && direction != 1)
            {
                Canvas.SetLeft(pMan, Canvas.GetLeft(pMan) - 5);
                timer2.Enabled = true;
                constraints.isRightWall = false;
            }
            if (constraints.isBotWall && direction != 2)
            {
                Canvas.SetTop(pMan, Canvas.GetTop(pMan) - 5);
                timer2.Enabled = true;
                constraints.isBotWall = false;
            }
            if (constraints.isLeftWall && direction != 3)
            {
                Canvas.SetLeft(pMan, Canvas.GetLeft(pMan) + 5);
                timer2.Enabled = true;
                constraints.isLeftWall = false;
            }
        }
        
        void wall_Hit_Action()
        {
            if (constraints.Is_Right_Wall_Hit(pMan))
            {
                timer2.Enabled = false;
            }
            if (constraints.Is_Left_Wall_Hit(pMan))
            {
                timer2.Enabled = false;
            }
        }

        public static void Move_Sprite(int direction, Image pMan)
        {
            switch (direction)
            {
                case 0:
                    Canvas.SetTop(pMan, Canvas.GetTop(pMan) - 5);
                    break;
                case 2:
                    Canvas.SetTop(pMan, Canvas.GetTop(pMan) + 5);
                    break;
                case 1:
                    Canvas.SetLeft(pMan, Canvas.GetLeft(pMan) + 5);
                    break;
                case 3:
                    Canvas.SetLeft(pMan, Canvas.GetLeft(pMan) - 5);
                    break;
            }
        }

        public int k { get; set; } = 0;
        public void Advance()
        {
            if(k==0)food.food_fix();
            wall_Hit_Action();
            Wall_Hit_bugs_Fix(direction);
            Reset_Sprite_Exit_Wall(pMan);
            if (constraints.Is_Food_Eaten()) special_Food = true;
            constraints.Is_Food_Eaten();
            k = 1;
            score = constraints.SCORE;
        }

        public static void SetSpritePosition(double left, double top, Image sprite)
        {
            Canvas.SetLeft(sprite, board.ActualWidth / left);
            Canvas.SetTop(sprite, board.ActualHeight / top);
        }

        public static void SetPacmanEatGhost(bool ate)
        {
            pacman_eat_ghost = ate;
        }
        
        public static bool PacmanAteGhost()
        {
            return pacman_eat_ghost;
        }

        public static void Set_Pacman_Direction(Key dir)
        {
            switch (dir)
            {
                case Key.Up: direction = 0; break;
                case Key.Right: direction = 1; break;
                case Key.Down: direction = 2; break;
                case Key.Left: direction = 3; break;
            }
        }
        
        public static int Get_Pacman_direction()
        {
            return direction;
        }

    }

}
