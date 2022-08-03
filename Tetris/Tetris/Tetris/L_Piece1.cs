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
    class L_Piece1:Window
    {
        int L1_state;
        Constraints constraint;
        Controller control;

        List<Button> L;
        List<Button> played;

        public L_Piece1(List<Button> xs, Canvas x, List<Button> played)
        {
            L = xs;
            L1_state = 0;
            this.played = played;

            constraint = new Constraints(x);
            control = new Controller(x, played);
        }

        void piece()
        {

            Canvas.SetLeft(L[2], Canvas.GetLeft(L[0]));
            Canvas.SetLeft(L[1], Canvas.GetLeft(L[0]));
            Canvas.SetLeft(L[3], Canvas.GetLeft(L[2]) + L[2].ActualWidth);

            control.MoveDown(L[2]);
            Canvas.SetTop(L[1], Canvas.GetTop(L[2]) - L[2].ActualHeight);
            Canvas.SetTop(L[0], Canvas.GetTop(L[1]) - L[1].ActualHeight);
            Canvas.SetTop(L[3], Canvas.GetTop(L[2]));
        }

        void piece2()
        {
            Canvas.SetLeft(L[2], Canvas.GetLeft(L[0]));
            Canvas.SetLeft(L[1], Canvas.GetLeft(L[2]) - L[2].ActualWidth);
            Canvas.SetLeft(L[3], Canvas.GetLeft(L[1]) - L[2].ActualWidth);

            control.MoveDown(L[2]);
            Canvas.SetTop(L[0], Canvas.GetTop(L[2]) - L[2].ActualHeight);
            Canvas.SetTop(L[1], Canvas.GetTop(L[2]));
            Canvas.SetTop(L[3], Canvas.GetTop(L[2]));
        }

        void piece3()
        {
            Canvas.SetLeft(L[2], Canvas.GetLeft(L[0]));
            Canvas.SetLeft(L[1], Canvas.GetLeft(L[2]) + L[2].ActualWidth);
            Canvas.SetLeft(L[3], Canvas.GetLeft(L[1]) + L[2].ActualWidth);

            control.MoveDown(L[2]);
            Canvas.SetTop(L[0], Canvas.GetTop(L[2]) - L[2].ActualHeight);
            Canvas.SetTop(L[1], Canvas.GetTop(L[0]));
            Canvas.SetTop(L[3], Canvas.GetTop(L[0]));
        }

        void piece4()
        {
            Canvas.SetLeft(L[2], Canvas.GetLeft(L[0]));
            Canvas.SetLeft(L[1], Canvas.GetLeft(L[0]));
            Canvas.SetLeft(L[3], Canvas.GetLeft(L[0]) - L[0].ActualWidth);

            control.MoveDown(L[2]);
            Canvas.SetTop(L[1], Canvas.GetTop(L[2]) - L[2].ActualHeight);
            Canvas.SetTop(L[0], Canvas.GetTop(L[1]) - L[1].ActualHeight);
            Canvas.SetTop(L[3], Canvas.GetTop(L[0]));
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
            if (!constraint.at_Bottom(L))
            {
                L1_state = (L1_state + 1) % 4;
            }
        }

        public void switchPiece2()
        {
            if (constraint.Is_OnTop_OfPiece(L, played)) return;
            if (!constraint.at_Bottom(L))
            {
                L1_state = (L1_state - 1) % 4;
                if (L1_state < 0) L1_state = 3;
            }
        }

        private bool Is_Overlapping(List<Button> xs)
        {
            return ((constraint.Is_Piece_Overlapping(xs) || constraint.Is_Piece_Overlapping2(xs, played)) && !constraint.at_Bottom(xs));
        }

        private void fix_Overrlapping(List<Button> xs)
        {
            if ((constraint.Is_Piece_Overlapping2(xs, played) || constraint.Is_Piece_Overlapping(xs)) && L1_state == 3) piece3();
            if ((constraint.Is_Piece_Overlapping2(xs, played) || constraint.Is_Piece_Overlapping(xs)) && L1_state == 2) piece2();
            if ((constraint.Is_Piece_Overlapping2(xs, played) || constraint.Is_Piece_Overlapping(xs)) && L1_state == 1) piece();
            if ((constraint.Is_Piece_Overlapping2(xs, played) || constraint.Is_Piece_Overlapping(xs)) && L1_state == 0) piece4();
        }

        public void play()
        {
            if (L1_state == 0)
            {
                piece();
                if (Is_Overlapping(L))
                {
                    piece4();
                    L1_state = 3;
                }
            }
            if (L1_state == 1)
            {
                piece2();
                if (Is_Overlapping(L))
                {
                    piece();
                    L1_state = 0;
                }
            }
            if (L1_state == 2)
            {
                piece3();
                if (Is_Overlapping(L))
                {
                    piece2();
                    L1_state = 1;
                }
            }
            if (L1_state == 3)
            {
                piece4();
                if (Is_Overlapping(L))
                {
                    piece3();
                    L1_state = 2;
                }
            }
            if (constraint.at_Bottom(L))
            {
                fix_Overrlapping(L);
            }
        }

    }
}
