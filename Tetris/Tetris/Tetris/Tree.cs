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
    class Tree : Window
    {

        int tree_State;
        List<Button> played;
        List<Button> T;

        Constraints constraint;
        Controller control;

        public Tree(List<Button> xs, Canvas x, List<Button> played)
        {
            tree_State = 0;
            T = xs;
            this.played = played;

            constraint = new Constraints(x);
            control = new Controller(x, played);

        }

        void piece1()
        {
            Canvas.SetLeft(T[2], Canvas.GetLeft(T[0]));
            Canvas.SetLeft(T[1], Canvas.GetLeft(T[2]) - T[2].ActualWidth);
            Canvas.SetLeft(T[3], Canvas.GetLeft(T[2]) + T[2].ActualWidth);

            control.MoveDown(T[2]);
            Canvas.SetTop(T[0], Canvas.GetTop(T[2]) - T[2].ActualHeight);
            Canvas.SetTop(T[1], Canvas.GetTop(T[2]));
            Canvas.SetTop(T[3], Canvas.GetTop(T[2]));
        }

        void piece2()
        {
            Canvas.SetLeft(T[1], Canvas.GetLeft(T[0]) + T[0].ActualWidth);
            Canvas.SetLeft(T[2], Canvas.GetLeft(T[1]));
            Canvas.SetLeft(T[3], Canvas.GetLeft(T[1]));

            control.MoveDown(T[2]);
            Canvas.SetTop(T[1], Canvas.GetTop(T[2]) - T[2].ActualHeight);
            Canvas.SetTop(T[0], Canvas.GetTop(T[1]));
            Canvas.SetTop(T[3], Canvas.GetTop(T[1]) - T[1].ActualHeight);
        }

        void piece3()
        {
            Canvas.SetLeft(T[2], Canvas.GetLeft(T[0]));
            Canvas.SetLeft(T[1], Canvas.GetLeft(T[2]) - T[2].ActualWidth);
            Canvas.SetLeft(T[3], Canvas.GetLeft(T[2]) + T[2].ActualWidth);

            control.MoveDown(T[2]);
            Canvas.SetTop(T[0], Canvas.GetTop(T[2]) - T[2].ActualHeight);
            Canvas.SetTop(T[1], Canvas.GetTop(T[0]));
            Canvas.SetTop(T[3], Canvas.GetTop(T[0]));
        }

        void piece4()
        {
            Canvas.SetLeft(T[1], Canvas.GetLeft(T[0]) - T[0].ActualWidth);
            Canvas.SetLeft(T[2], Canvas.GetLeft(T[1]));
            Canvas.SetLeft(T[3], Canvas.GetLeft(T[1]));

            control.MoveDown(T[2]);
            Canvas.SetTop(T[1], Canvas.GetTop(T[2]) - T[2].ActualHeight);
            Canvas.SetTop(T[0], Canvas.GetTop(T[1]));
            Canvas.SetTop(T[3], Canvas.GetTop(T[1]) - T[1].ActualHeight);
        }

        public void move_Left()
        {
            if (constraint.Is_OnTop_OfPiece(T, played)) return;
            control.MoveLeft(T);
        }

        public void move_Right()
        {
            if (constraint.Is_OnTop_OfPiece(T, played)) return;
            control.MoveRight(T);
        }
        
        public void switchPiece()
        {
            if (constraint.Is_OnTop_OfPiece(T, played)) return;
            if (constraint.at_Bottom(T)) return;
            tree_State = (tree_State + 1) % 4;
        }

        public void switchPiece2()
        {
            if (constraint.Is_OnTop_OfPiece(T, played)) return;
            if (constraint.at_Bottom(T)) return;

            tree_State = (tree_State - 1) % 4;
            if (tree_State < 0) tree_State = 3;
        }

        private void fix_Overlapping(List<Button> xs)
        {
            if ((constraint.Is_Piece_Overlapping2(xs, played) || constraint.Is_Piece_Overlapping(xs)) && tree_State == 3) piece3();
            if ((constraint.Is_Piece_Overlapping2(xs, played) || constraint.Is_Piece_Overlapping(xs)) && tree_State == 2) piece2();
            if ((constraint.Is_Piece_Overlapping2(xs, played) || constraint.Is_Piece_Overlapping(xs)) && tree_State == 1) piece1();
            if ((constraint.Is_Piece_Overlapping2(xs, played) || constraint.Is_Piece_Overlapping(xs)) && tree_State == 0) piece4();
            if (constraint.Is_Piece_Overlapping2(xs, played) || constraint.Is_Piece_Overlapping(xs)) piece3();
            if (constraint.Is_Piece_Overlapping2(xs, played) || constraint.Is_Piece_Overlapping(xs)) piece2();
            if (constraint.Is_Piece_Overlapping2(xs, played) || constraint.Is_Piece_Overlapping(xs)) piece4();
            if (constraint.Is_Piece_Overlapping2(xs, played) || constraint.Is_Piece_Overlapping(xs)) piece1();
        }

        private bool Is_Overlapping(List<Button> xs)
        {
            return ((constraint.Is_Piece_Overlapping(xs) || constraint.Is_Piece_Overlapping2(xs, played)) && !constraint.at_Bottom(xs));
        }

        public void play()
        {
        theStates:;
            if (tree_State == 0)
            {
                piece1(); 
                if (Is_Overlapping(T)) tree_State = 3;
            }
            if (tree_State == 1)
            {
                piece2();

                if (Is_Overlapping(T))
                {
                    tree_State = 0;
                    goto theStates;
                }
            }
            if (tree_State == 2)
            {
                piece3();
                if (Is_Overlapping(T))
                {
                    tree_State = 1;
                    goto theStates;
                }
            }
            if (tree_State == 3)
            {
                piece4();
                if (Is_Overlapping(T))
                {
                    tree_State = 2;
                    goto theStates;
                }
            }
            if (constraint.at_Bottom(T))
            {
                fix_Overlapping(T);
            }
        }

    }
}
