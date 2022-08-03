using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Pac_man
{
    class Coordinates
    {

        /// <summary>
        ///  Given an object such as Image (for a sprite), button (for a wall), or ellipse (for food), returns TOP coordinate in the Canvas of that object
        /// </summary>
        /// <param name="obj"> Image (for a sprite), button (for a wall), or ellipse (for food) </param>
        /// <returns> TOP Y coordinate of an object </returns>
        public static double GetTopPosition(Object obj)
        {
            if (obj is Image)
            {
                Image item = (Image)obj;
                return Canvas.GetTop(item);
            }
            else if (obj is Button)
            {
                Button item = (Button)obj;
                return Canvas.GetTop(item);
            }
            else if (obj is Ellipse)
            {
                Ellipse item = (Ellipse)obj;
                return Canvas.GetTop(item);
            }
            else return -1;
        }

        /// <summary>
        ///  Given an object such as Image (for a sprite), button (for a wall), or ellipse (for food), returns BOTTOM coordinate in the Canvas of that object
        /// </summary>
        /// <param name="obj"> Image (for a sprite), button (for a wall), or ellipse (for food) </param>
        /// <returns> BOTTOM Y coordinate of an object </returns>
        public static double GetBottomPosition(Object obj)
        {
            if (obj is Image)
            {
                Image item = (Image)obj;
                return Canvas.GetTop(item) + item.ActualHeight;
            }
            else if (obj is Button)
            {
                Button item = (Button)obj;
                return Canvas.GetTop(item) + item.ActualHeight;
            }
            else if (obj is Ellipse)
            {
                Ellipse item = (Ellipse)obj;
                return Canvas.GetTop(item) + item.ActualHeight;
            }
            else return -1;
        }

        /// <summary>
        ///  Given an object such as Image (for a sprite), button (for a wall), or ellipse (for food), returns LEFT coordinate in the Canvas of that object
        /// </summary>
        /// <param name="obj"> Image (for a sprite), button (for a wall), or ellipse (for food) </param>
        /// <returns> LEFT X coordinate of an object </returns>
        public static double GetLeftPosition(Object obj)
        {
            if (obj is Image)
            {
                Image item = (Image)obj;
                return Canvas.GetLeft(item);
            }
            else if (obj is Button)
            {
                Button item = (Button)obj;
                return Canvas.GetLeft(item);
            }
            else if (obj is Ellipse)
            {
                Ellipse item = (Ellipse)obj;
                return Canvas.GetLeft(item);
            }
            else return -1;
        }

        /// <summary>
        ///  Given an object such as Image (for a sprite), button (for a wall), or ellipse (for food), returns RIGHT coordinate in the Canvas of that object
        /// </summary>
        /// <param name="obj"> Image (for a sprite), button (for a wall), or ellipse (for food) </param>
        /// <returns> RIGHT X coordinate of an object </returns>
        public static double GetRightPosition(Object obj)
        {
            if (obj is Image)
            {
                Image item = (Image)obj;
                return Canvas.GetLeft(item) + item.ActualWidth;
            }
            else if (obj is Button)
            {
                Button item = (Button)obj;
                return Canvas.GetLeft(item) + item.ActualWidth;
            }
            else if (obj is Ellipse)
            {
                Ellipse item = (Ellipse)obj;
                return Canvas.GetLeft(item) + item.ActualWidth;
            }
            else return -1;
        }
        
    }
}
