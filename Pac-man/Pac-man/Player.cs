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
    /// <summary>
    /// A class that represent the Pacman player's properties.
    /// The GUI representation of Pacman is using Images. There are two arrays used. 
    /// 1) The first Array is a 2-Dimensional array, its index positions represent pac-man direction e.g index 0 means pac-man is facing up
    /// 2) The second Array is of size 2, all stored within the first array, and that inner arrays contain pac-man open mouth and closed to simulate bitting/eating food on the screen.
    /// </summary>
    class Player:Window
    {
        private BitmapImage[] twoFaces;             // Array that contains Open Mouthed and Closed mouth of Pac-man
        private BitmapImage[][] allFaces;           // 2 - D array that contains all sets of Opend and closed mouths of pac-man for all directions.
        private static Image pacman;                // Current pacman face (either opened mouth or closed mouth)
        private static int pacman_lives;            // Number of pac-man lives left in the game
        private int dir_face;                       // indexer to choose direction in Direction array
        private int curr_dir_face;                  // indexer to choose opened or closed mouth

        /// <summary>
        /// Constructor to initialize all the above properties.
        /// </summary>
        public Player()
        {
            pacman_lives = 1;
            allFaces = GUI.Pacman.All_Faces();
            pacman = GUI.Pacman.NewPacman();
            twoFaces = allFaces[Controller.Get_Pacman_direction()];

            dir_face = 0;
            curr_dir_face = 0;
        }
        
        /// <summary>
        /// A method that switch pacman faces in case of death
        /// </summary>
        public void pacman_dead()
        {
            twoFaces = allFaces[4];
            pacman.Source = twoFaces[dir_face];
            dir_face = (dir_face + 1) % 4;
        }
        
        /// <summary>
        /// A method that simulate pac-man open-close mouth during game run-time
        /// </summary>
        public void Advance()
        {   
            
            twoFaces = allFaces[Controller.Get_Pacman_direction()];
            pacman.Source = twoFaces[curr_dir_face];
            curr_dir_face = (curr_dir_face + 1) % 2;
        }

        /// <summary>
        /// Method to query current pacman face
        /// </summary>
        /// <returns> Current pacman face</returns>
        public static Image GetPacman()
        {
            return pacman;
        }

        /// <summary>
        /// Method to reduce pac-man lives
        /// </summary>
        public static void Kill_Pacman()
        {
            pacman_lives--;
        }

        /// <summary>
        /// Method to query current pac-man lives
        /// </summary>
        /// <returns></returns>
        public static int GetPacmanLives()
        {
            return pacman_lives;
        }
    }
}
