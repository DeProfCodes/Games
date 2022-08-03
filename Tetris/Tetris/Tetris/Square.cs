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
    class Square:Window
    {
        
        Controller control;
        Constraints constraint;
        List<Button> played;
        List<Button> S;

        public Square(List<Button> xs, Canvas x, List<Button> played)
        {
            S = xs;
            this.played = played;

            control = new Controller(x, played);
            constraint = new Constraints(x);

        }

        void piece()
        {
            Canvas.SetLeft(S[1], Canvas.GetLeft(S[0]) + S[0].ActualWidth);
            Canvas.SetLeft(S[2], Canvas.GetLeft(S[0]));
            Canvas.SetLeft(S[3], Canvas.GetLeft(S[2]) + S[2].ActualWidth);

            control.MoveDown(S[2]);
            Canvas.SetTop(S[3], Canvas.GetTop(S[2]));
            Canvas.SetTop(S[0], Canvas.GetTop(S[2]) - S[2].ActualHeight);
            Canvas.SetTop(S[1], Canvas.GetTop(S[0]));
        }

        public void move_Left()
        {
            if (constraint.Is_OnTop_OfPiece(S, played)) return;
            control.MoveLeft(S);
        }

        public void move_Right()
        {
            if (constraint.Is_OnTop_OfPiece(S, played)) return;
            control.MoveRight(S);
        }

        public void play()
        {
            piece();
        }

    }
}
