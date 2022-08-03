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

namespace Tetris
{
    class Controller:Window
    {
        Constraints constaint;
        Canvas x;
        List<Button> played;

        public Controller(Canvas x, List<Button> played)
        {
            constaint = new Constraints(x);
            this.x = x;
            this.played = played;
        }

        public void MoveLeft(List<Button> ws)
        {
            if (constaint.Is_Piece_A_LeftWall(ws, played)) return;
            if (constaint.at_Bottom(ws) || constaint.Is_Leftwall_Hit(ws)) return;
            for (int i = 0; i < ws.Count; i++)
            {
                Canvas.SetLeft(ws[i], Canvas.GetLeft(ws[i]) - 48);
            }
        }

        public void MoveRight(List<Button> ws)
        {
            if (constaint.Is_Piece_A_RightWall(ws, played)) return;
            if (constaint.at_Bottom(ws) || constaint.Is_Rightwall_Hit(ws)) return;
            for (int i = 0; i < ws.Count; i++)
            {
                Canvas.SetLeft(ws[i], Canvas.GetLeft(ws[i]) + 48);
            }
        }

        public void MoveDown(Button x3)
        {
            if (Canvas.GetTop(x3) == 682.5) return ;
            Canvas.SetTop(x3, Canvas.GetTop(x3) + 52.5);
        }
    }
}
