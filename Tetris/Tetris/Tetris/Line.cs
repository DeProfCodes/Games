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
    class Line:Window
    {
        int Line_State;
        List<Button> played;
        List<Button> L;

        Constraints constraint;
        Controller control;

        public Line(List<Button> x1, Canvas x, List<Button> played)
        {
            Line_State = 0;
            L = x1;
            this.played = played;

            constraint = new Constraints(x);
            control = new Controller(x,played);
            
        }

        void piece1()
        {
            Canvas.SetLeft(L[1], Canvas.GetLeft(L[0]) + L[0].ActualWidth);
            Canvas.SetLeft(L[2], Canvas.GetLeft(L[1]) + L[1].ActualWidth);
            Canvas.SetLeft(L[3], Canvas.GetLeft(L[2]) + L[2].ActualWidth);

            control.MoveDown(L[0]);
            Canvas.SetTop(L[1], Canvas.GetTop(L[0]));
            Canvas.SetTop(L[2], Canvas.GetTop(L[0]));
            Canvas.SetTop(L[3], Canvas.GetTop(L[0]));
        }

        void piece2()
        {
            Canvas.SetLeft(L[1], Canvas.GetLeft(L[0]));
            Canvas.SetLeft(L[2], Canvas.GetLeft(L[0]));
            Canvas.SetLeft(L[3], Canvas.GetLeft(L[0]));

            control.MoveDown(L[0]);
            Canvas.SetTop(L[1], Canvas.GetTop(L[0]) - L[0].ActualHeight);
            Canvas.SetTop(L[2], Canvas.GetTop(L[1]) - L[1].ActualHeight);
            Canvas.SetTop(L[3], Canvas.GetTop(L[2]) - L[2].ActualHeight);
        }

        public void move_Left()
        {
            if (constraint.Is_OnTop_OfPiece(L, played)) return;
            control.MoveLeft(L);
        }

        public void move_Right()
        {
            if (constraint.Is_OnTop_OfPiece(L, played)) return;
            control.MoveRight(L);
        }

        public void switchPiece()
        {
            if (constraint.Is_OnTop_OfPiece(L, played)) return;
            if (constraint.at_Bottom(L)) return;
            Line_State = (Line_State + 1) % 2; 
        }

        private void fix_Overrlapping(List<Button> xs)
        {
            if ((constraint.Is_Piece_Overlapping2(xs, played) || constraint.Is_Piece_Overlapping(xs)) && Line_State == 1) piece1();
            if ((constraint.Is_Piece_Overlapping2(xs, played) || constraint.Is_Piece_Overlapping(xs)) && Line_State == 0) piece2();
        }

        private bool Is_Overlapping(List<Button> xs)
        {
            return ((constraint.Is_Piece_Overlapping(xs) || constraint.Is_Piece_Overlapping2(xs, played)) && !constraint.at_Bottom(xs));
        }

        public void play()
        {
            if (Line_State == 0)
            {
                piece1();
                if (Is_Overlapping(L))
                {
                    piece2();
                    Line_State = 1;
                }
            }
            if (Line_State == 1)
            {
                piece2();
                if (Is_Overlapping(L))
                {
                    piece1();
                    Line_State = 0;
                }
            }
            if (constraint.at_Bottom(L))
            {
                fix_Overrlapping(L);
            }
        }
        
    }
}
