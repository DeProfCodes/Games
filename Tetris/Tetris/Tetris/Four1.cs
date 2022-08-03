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
    class Four1:Window
    {
        int four1_State;
        
        Constraints constraint;
        Controller control;
        List<Button> played;
        List<Button> F;

        public Four1(List<Button> xs, Canvas x, List<Button> p)
        {
            four1_State = 0;
            F = xs;
            played = p;

            constraint = new Constraints(x);
            control = new Controller(x, played);

        }

        void piece1()
        {
            Canvas.SetLeft(F[1], Canvas.GetLeft(F[0]) + F[0].ActualWidth);
            Canvas.SetLeft(F[2], Canvas.GetLeft(F[0]));
            Canvas.SetLeft(F[3], Canvas.GetLeft(F[2]) - F[2].ActualWidth);

            control.MoveDown(F[2]);
            Canvas.SetTop(F[0], Canvas.GetTop(F[2]) - F[2].ActualHeight);
            Canvas.SetTop(F[1], Canvas.GetTop(F[0]));
            Canvas.SetTop(F[3], Canvas.GetTop(F[2]));
        }

        void piece2()
        {
            Canvas.SetLeft(F[1], Canvas.GetLeft(F[0]));
            Canvas.SetLeft(F[3], Canvas.GetLeft(F[0]) + F[0].ActualWidth);
            Canvas.SetLeft(F[2], Canvas.GetLeft(F[3]));

            control.MoveDown(F[2]);
            Canvas.SetTop(F[3], Canvas.GetTop(F[2]) - F[2].ActualHeight);
            Canvas.SetTop(F[0], Canvas.GetTop(F[3]));
            Canvas.SetTop(F[1], Canvas.GetTop(F[0]) - F[0].ActualHeight);
        }

        public void move_Left()
        {
            if (constraint.Is_OnTop_OfPiece(F, played)) return;
            control.MoveLeft(F);
        }

        public void move_Right()
        {
            if (constraint.Is_OnTop_OfPiece(F, played)) return;
            control.MoveRight(F);
        }

        public void switchPiece()
        {
            if (constraint.Is_OnTop_OfPiece(F, played)) return;
            if (constraint.at_Bottom(F)) return;
            four1_State = (four1_State + 1) % 2;
        }

        private void fix_Overrlapping(List<Button> xs)
        {
            if ((constraint.Is_Piece_Overlapping2(xs, played) || constraint.Is_Piece_Overlapping(xs)) && four1_State == 1) piece1();
            if ((constraint.Is_Piece_Overlapping2(xs, played) || constraint.Is_Piece_Overlapping(xs)) && four1_State == 0) piece2();
        }

        private bool Is_Overlapping(List<Button> xs)
        {
            return ((constraint.Is_Piece_Overlapping(xs) || constraint.Is_Piece_Overlapping2(xs, played)) && !constraint.at_Bottom(xs));
        }

        public void play()
        {
            if (four1_State == 0)
            {
                piece1();
                if (Is_Overlapping(F))
                {
                    piece2();
                    four1_State = 1;
                }
            }
            if (four1_State == 1)
            {
                piece2();
                if (Is_Overlapping(F))
                {
                    piece1();
                    four1_State = 0;
                }
            }
            if (constraint.at_Bottom(F))
            {
                fix_Overrlapping(F);
            }
        }

    }
}
