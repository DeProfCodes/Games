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
    class Ghosts:Window
    {
        public static Image Inky { get; set; }
        public static Image Pinky { get; set; }
        public static Image Blinky { get; set; }
        public static Image Clyde { get; set; }

        bool top;
        bool left;
        bool LEFT;
        bool RIGHT;
        bool DOWN;
        bool TOP;

        public List<int> which_ghosts_eaten { get; set; }
        public static BitmapImage[] myGhosts { get; private set; }
        public List<Image> Gs { get; private set; }

        public bool ghost_Is_Eaten { get; set; }
        public double gs_eaten_top { get; private set; }
        public double gs_eaten_left { get; private set; }
        
        public Ghosts()
        {
            which_ghosts_eaten = new List<int>();
            myGhosts = GUI.Ghosts.LoadGhost();
            Gs = new List<Image> { Inky, Pinky, Blinky, Clyde };
            SetGhosts();

            top = false;
            left = false;
            LEFT = false;
            RIGHT = false;
            DOWN = false;
            TOP = false;

            ghost_Is_Eaten = false;
            gs_eaten_top = 0;
            gs_eaten_left = 0;
            
        }
        
        private void SetGhosts()
        {
            List<Image> ghosts = GUI.Ghosts.NewGhosts(myGhosts);
            Inky = ghosts[0];
            Pinky = ghosts[1];
            Blinky = ghosts[2];
            Clyde = ghosts[3];
            Gs = new List<Image> { Inky, Pinky, Blinky, Clyde };
        }

        public static List<Image> GetGhost()
        {
            return new List<Image> { Inky, Pinky, Blinky, Clyde };
        }

        public static void Reset_ghosts(List<Image> ghosts)
        {
            for (int i = 0; i < ghosts.Count; i++)
                ghosts[i].Source = myGhosts[i];
        }

        public void special_Food_Eaten()
        {
            if (which_ghosts_eaten.IndexOf(1) == -1) Pinky.Source = myGhosts[4];
            if(which_ghosts_eaten.IndexOf(0)==-1) Inky.Source = myGhosts[4];
            if (which_ghosts_eaten.IndexOf(3)==-1) Clyde.Source = myGhosts[4];
            if (which_ghosts_eaten.IndexOf(2)==-1) Blinky.Source = myGhosts[4];
        }

       

        void update_Bools(Image ghost)
        {
            top = Math.Abs(Coordinates.GetTopPosition(Player.GetPacman()) - Coordinates.GetTopPosition(ghost)) < ghost.ActualHeight;
            left = Math.Abs(Coordinates.GetLeftPosition(Player.GetPacman()) - Coordinates.GetLeftPosition(ghost)) < ghost.ActualWidth;
            
            LEFT = Coordinates.GetLeftPosition(ghost) >= Coordinates.GetLeftPosition(ghost) && top && left;
            RIGHT = Coordinates.GetLeftPosition(ghost) <= Coordinates.GetLeftPosition(ghost) && top && left;
            TOP = Coordinates.GetTopPosition(ghost) >= Coordinates.GetTopPosition(ghost) && top && left;
            DOWN = Coordinates.GetTopPosition(ghost) <= Coordinates.GetTopPosition(ghost) && top && left;
        }
        
        public bool ghost_Hit_Pacman()
        {
            for(int i=0;i<Gs.Count;i++)
            {
                update_Bools(Gs[i]);
                if (TOP || DOWN || LEFT || RIGHT)
                {
                    if (Gs[i].Source == myGhosts[4])
                    {
                        which_ghosts_eaten.Add(i);
                        ghost_Is_Eaten = true;
                        gs_eaten_left = Canvas.GetLeft(Gs[i]);
                        gs_eaten_top = Canvas.GetTop(Gs[i]);
                    }
                    return true;
                }
            }
            return false;
        }

    }
}
