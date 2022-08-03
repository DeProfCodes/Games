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
    class Constraints:Window
    {
        Canvas board;
        Image p;
        Player pMan;
        List<Button> walls;
        List<Ellipse> food;

        public bool isSpecial { get; set; }
        public bool isTopWall { get; set; }
        public bool isBotWall { get; set; }
        public bool isLeftWall { get; set; }
        public bool isRightWall { get; set; }

        public int Score { get; set; }
        public string SCORE { get; set; }

        public Constraints(Player pMan, List<Button> walls, List<Ellipse> food)
        {
            board = GUI.GetBoard();
            this.pMan = pMan;
            p = Player.GetPacman();
            this.walls = walls;
            this.food = food;

            isSpecial = false;
            isTopWall = false;
            isBotWall = false;
            isLeftWall = false;
            isRightWall = false;

            Score = 0;
            SCORE = "000000000";
        }

        /// <summary>
        ///  Given an object such as Image (for a sprite), button (for a wall), or ellipse (for food), returns TOP coordinate in the Canvas of that object
        /// </summary>
        /// <param name="obj"> Image (for a sprite), button (for a wall), or ellipse (for food) </param>
        /// <returns> TOP Y coordinate of an object </returns>
        private double GetTopPosition(Object obj)
        {
            return Coordinates.GetTopPosition(obj);
        }

        /// <summary>
        ///  Given an object such as Image (for a sprite), button (for a wall), or ellipse (for food), returns BOTTOM coordinate in the Canvas of that object
        /// </summary>
        /// <param name="obj"> Image (for a sprite), button (for a wall), or ellipse (for food) </param>
        /// <returns> BOTTOM Y coordinate of an object </returns>
        private double GetBottomPosition(Object obj)
        {
            return Coordinates.GetBottomPosition(obj);
        }

        /// <summary>
        ///  Given an object such as Image (for a sprite), button (for a wall), or ellipse (for food), returns LEFT coordinate in the Canvas of that object
        /// </summary>
        /// <param name="obj"> Image (for a sprite), button (for a wall), or ellipse (for food) </param>
        /// <returns> LEFT X coordinate of an object </returns>
        public double GetLeftPosition(Object obj)
        {
            return Coordinates.GetLeftPosition(obj);
        }

        /// <summary>
        ///  Given an object such as Image (for a sprite), button (for a wall), or ellipse (for food), returns RIGHT coordinate in the Canvas of that object
        /// </summary>
        /// <param name="obj"> Image (for a sprite), button (for a wall), or ellipse (for food) </param>
        /// <returns> RIGHT X coordinate of an object </returns>
        public double GetRightPosition(Object obj)
        {
            return Coordinates.GetRightPosition(obj);
        }
        
        /// <summary>
        /// Detects whether a (any) wall in the game is hit from the RIGHT
        /// </summary>
        /// <param name="sprite"> Pac-man </param>
        /// <returns> true if wall hit, otherwise false</returns>
        public bool Is_Right_Wall_Hit(Image sprite)
        {
            for (int i=0;i<walls.Count; i++)
            {
                if(GetTopPosition(sprite)>=GetTopPosition(walls[i]) && GetBottomPosition(sprite)<=GetBottomPosition(walls[i]))
                {
                    if ( GetRightPosition(sprite)>=GetLeftPosition(walls[i])-10 && GetRightPosition(sprite) < GetRightPosition(walls[i]))
                    {
                        isRightWall = true;
                        return true;
                    }
                }
                if (Math.Abs(GetTopPosition(sprite) - GetTopPosition(walls[i])) < sprite.ActualHeight || Math.Abs(GetBottomPosition(sprite) - GetBottomPosition(walls[i])) < sprite.ActualHeight)
                {
                    if (GetRightPosition(sprite)>=GetLeftPosition(walls[i])-10 && GetRightPosition(sprite) < GetRightPosition(walls[i]))
                    {
                        if (Math.Abs(GetTopPosition(sprite) - GetBottomPosition(walls[i])) < 10) isTopWall = true;
                        if (Math.Abs(GetBottomPosition(sprite) - GetTopPosition(walls[i])) < 10) isBotWall = true;
                        if (Math.Abs(GetRightPosition(sprite) - GetLeftPosition(walls[i])) < 10) isRightWall = true;
                        if (Math.Abs(GetLeftPosition(sprite) - GetRightPosition(walls[i])) < 10) isLeftWall = true;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Detects whether a (any) wall in the game is hit from the LEFT
        /// </summary>
        /// <param name="sprite"> Pac-man </param>
        /// <returns> true if wall hit, otherwise false</returns>
        public bool Is_Left_Wall_Hit(Image sprite)
        {
            for (int i = 0; i < walls.Count; i++)
            {
                if (GetTopPosition(sprite) >= GetTopPosition(walls[i]) && GetBottomPosition(sprite) <= GetBottomPosition(walls[i]))
                {
                    if (GetLeftPosition(sprite) <= GetRightPosition(walls[i]) && GetRightPosition(sprite) > GetLeftPosition(walls[i]))
                    {
                        isLeftWall = true;
                        return true;
                    }
                }
                if (Math.Abs(GetTopPosition(sprite) - GetTopPosition(walls[i])) < p.ActualHeight || Math.Abs(GetBottomPosition(sprite) - GetBottomPosition(walls[i])) < p.ActualHeight)
                {
                    if (GetLeftPosition(sprite) <= GetRightPosition(walls[i]) && GetLeftPosition(sprite) > GetLeftPosition(walls[i]))
                    {
                        if (Math.Abs(GetTopPosition(sprite) - GetBottomPosition(walls[i])) < 10) isTopWall = true;
                        if (Math.Abs(GetBottomPosition(sprite) - GetTopPosition(walls[i])) < 10) isBotWall = true;
                        if (Math.Abs(GetRightPosition(sprite) - GetLeftPosition(walls[i])) < 10) isRightWall = true;
                        if (Math.Abs(GetLeftPosition(sprite) - GetRightPosition(walls[i])) < 10) isLeftWall = true;
                        return true;
                    }
                }
            }
            return false;
        }
        
        public string update_Score()
        {
            if (isSpecial) Score += 80;
            string t = "00000000" + "" + Score;
            string t1 = "" + Score;
            SCORE = t.Substring(t1.Length - 1);
            return SCORE;
        }

        private void food_eaten(int i)
        {
            int special_food = (int)(board.ActualWidth / 50);
            isSpecial = (food[i].Width == special_food ? true : false);
            board.Children.Remove(food[i]);
            food.Remove(food[i]);
            Score += 20;
            update_Score();
        }

        public bool Is_Food_Eaten()
        {
            // Ghost is food :)
            if (Controller.PacmanAteGhost())
            {
                Score += 500;
                Controller.SetPacmanEatGhost(false);
                update_Score();
            }
            // Regular food
            for (int i=0;i<food.Count;i++)
            {
                int direction = Controller.Get_Pacman_direction();
                if (direction == 1 && (GetRightPosition(p) >= GetLeftPosition(food[i]) && GetLeftPosition(p)<= GetRightPosition(food[i])))
                {
                    if(GetTopPosition(p)<=GetTopPosition(food[i]) && GetBottomPosition(p)>= GetBottomPosition(food[i]))
                    {
                        food_eaten(i);
                        i--;
                    }
                }
                else if (direction == 3 && (GetLeftPosition(p)<= GetRightPosition(food[i])-10 && GetRightPosition(p)>=GetLeftPosition(food[i])))
                {
                    if (GetTopPosition(p) <= GetTopPosition(food[i]) && GetBottomPosition(p) >= GetBottomPosition(food[i]))
                    {
                        food_eaten(i);
                        i--;
                    }
                }
                else if (direction == 0 && (GetTopPosition(p)<=GetBottomPosition(food[i]) && GetBottomPosition(p)>= GetTopPosition(food[i])))
                {
                    if(GetLeftPosition(p)<=GetLeftPosition(food[i])-5 && GetRightPosition(p)>= GetRightPosition(food[i])-5)
                    {
                        food_eaten(i);
                        i--;
                    }
                }
                else if (direction == 2 && (GetBottomPosition(p) >= GetTopPosition(food[i]) && GetTopPosition(p) <= GetBottomPosition(food[i])))
                {
                    if (GetLeftPosition(p) <= GetLeftPosition(food[i])-5 && GetRightPosition(p) >= GetRightPosition(food[i])-5)
                    {
                        food_eaten(i);
                        i--;
                    }
                }
            }
            //if (isSpecial) MessageBox.Show("OK");
            return isSpecial;
        }
    }
}
