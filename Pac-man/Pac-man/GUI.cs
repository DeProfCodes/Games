using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Pac_man
{
    class GUI
    {
        private static Canvas board;
        public GUI(Canvas board)
        {
            GUI.board = board;
        }

        public static Canvas GetBoard()
        {
            return board;
        }

        public static class Pacman
        {

            public static Image NewPacman()
            {
                Image pacman = new Image();
                board.Children.Add(pacman);
                return pacman;
            }


            public static BitmapImage[][] All_Faces()
            {
                
                string inThisProject = "pack://application:,,,/";
                BitmapImage[][] pMan_Pieces = new BitmapImage[][] {new BitmapImage[] {
                                                new BitmapImage(new Uri(inThisProject+ "pMan_Up1.jpg")),
                                                new BitmapImage(new Uri(inThisProject+ "pMan_Up2.png"))},
                                               new BitmapImage[] {
                                                new BitmapImage(new Uri(inThisProject+ "pMan_Right1.jpg")),
                                                new BitmapImage(new Uri(inThisProject+ "pMan_Right2.png"))},
                                               new BitmapImage[] {
                                                new BitmapImage(new Uri(inThisProject+ "pMan_Down1.jpg")),
                                                new BitmapImage(new Uri(inThisProject+ "pMan_Down2.png"))},
                                               new BitmapImage[] {
                                                new BitmapImage(new Uri(inThisProject+ "pMan_Left1.jpg")),
                                                new BitmapImage(new Uri(inThisProject+ "pMan_Left2.png"))},
                                               new BitmapImage[] {
                                                new BitmapImage(new Uri(inThisProject+ "pman_dead_1.png")),
                                                new BitmapImage(new Uri(inThisProject+ "pman_dead_2.png")),
                                                new BitmapImage(new Uri(inThisProject+ "pman_dead_3.png")),
                                                new BitmapImage(new Uri(inThisProject+ "pman_dead_4.png"))} };

                return pMan_Pieces;
            }

            private static void Dimensions(Canvas board, Image p_man)
            {
                p_man.Width = (int)board.ActualWidth / 25;
                p_man.Height = (int)board.ActualHeight / 25;
            }

            public static void Starting_Position()
            {
                Image pacman = Player.GetPacman();
                Dimensions(board, pacman);

                pacman.Source = new BitmapImage(new Uri("pack://application:,,,/pMan_start.png"));
                Canvas.SetLeft(pacman, (int)(board.ActualWidth / 2.13));
                Canvas.SetTop(pacman, (int)(board.ActualHeight / 1.83));
                Controller.Set_Pacman_Direction(Key.Right);
                pacman.Visibility = Visibility.Visible;
            }
            
        }

        public static class Ghosts
        {

            public static BitmapImage[] LoadGhost()
            {
                string path = "pack://application:,,,/";
                BitmapImage[] Ghosts = new BitmapImage[] { new BitmapImage(new Uri(path + "INKY.jpg")),
                                           new BitmapImage(new Uri(path+"PINKY.jpg")),
                                           new BitmapImage(new Uri(path+"BLINKY.jpg")),
                                           new BitmapImage(new Uri(path+"CLYDE.jpg")),
                                           new BitmapImage(new Uri(path+"FOOD.jpg"))
                };

                return Ghosts;
            }

            private static void Set_Ghost_Dimensions(List<Image> ghosts)
            {
                foreach (Image ghost in ghosts)
                {
                    ghost.Width = (int)board.ActualWidth / 22;
                    ghost.Height = (int)board.ActualHeight / 22;
                }
            }

            private static void Load_ghostsImages(List<Image> ghosts, BitmapImage[] GhostSource)
            {
                for (int i = 0; i < ghosts.Count; i++)
                {
                    ghosts[i].Source = GhostSource[i];
                    board.Children.Add(ghosts[i]);
                    ghosts[i].Visibility = Visibility.Hidden;
                }
            }

            public static List<Image> NewGhosts(BitmapImage[] GhostSource)
            {
                List<Image> ghosts = new List<Image>();
                for (int i=0; i<4; i++) ghosts.Add(new Image());

                Load_ghostsImages(ghosts, GhostSource);
                Set_Ghost_Dimensions(ghosts);

                return ghosts;
            }
            
            public static void Starting_Position(List<Image> ghosts)
            {
                Controller.SetSpritePosition(2.3, 2.15, ghosts[0]);
                Controller.SetSpritePosition(2, 2.15, ghosts[1]);
                Controller.SetSpritePosition(1.8, 2.15, ghosts[2]);
                Controller.SetSpritePosition(1.85, 2.87, ghosts[3]);
            }

            public static void Show_Ghosts(List<Image> ghosts)
            {
                for(int i=0;i<ghosts.Count;i++)
                    ghosts[i].Visibility = Visibility.Visible;
            }

            public static void Reset_ghosts(List<Image> ghosts, List<BitmapImage> Gsource)
            {
                for (int i = 0; i < ghosts.Count; i++)
                    ghosts[i].Source = Gsource[i];
            }

        }
    }
}
