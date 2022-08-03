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
using System.Media;
using System.Windows.Resources;

namespace Tetris
{
    class Constraints:Window
    {
        private Canvas canvas1;
        public int score { get; set; }
        public int level { get; set; }
        public bool IsScore { get; set; }

        public Constraints(Canvas x)
        {
            canvas1 = x;
            score = 0;
            level = 1;
            IsScore = false;
        }
        
        public bool at_Bottom(List<Button> xs)
        {
            for (int i = 0; i < xs.Count; i++)
            {
                if (Canvas.GetTop(xs[i]) == 682.5) return true;
            }
            return false;
        }

        public bool Is_Leftwall_Hit(List<Button> x1)
        {
            for (int i = 0; i < x1.Count; i++)
            {
                if (Canvas.GetLeft(x1[i]) == 0) return true;
            }
            return false;
        }

        public bool Is_Piece_A_LeftWall(List<Button> piece, List<Button> played)
        {
            for(int i=0;i<played.Count; i++)
            {
                for(int k=0;k<piece.Count; k++)
                {
                    if (Canvas.GetTop(played[i]) == Canvas.GetTop(piece[k]))
                    {
                        if (Canvas.GetLeft(played[i]) + played[i].ActualWidth == Canvas.GetLeft(piece[k]))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool Is_Piece_A_RightWall(List<Button> piece, List<Button> played)
        {
            for (int i = 0; i < played.Count; i++)
            {
                for (int k = 0; k < piece.Count; k++)
                {
                    if (Canvas.GetTop(played[i]) == Canvas.GetTop(piece[k]))
                    {
                        if (Canvas.GetLeft(played[i]) == Canvas.GetLeft(piece[k]) + piece[k].ActualWidth)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool Is_Rightwall_Hit(List<Button> x2)
        {
            for (int i = 0; i < x2.Count; i++)
            {
                if (Canvas.GetLeft(x2[i])+x2[i].ActualWidth == canvas1.ActualWidth) return true;
            }
            return false;
        }

        public bool Is_Piece_Overlapping(List<Button> xs)
        {
            for (int i = 0; i < xs.Count; i++)
            {
                if (Canvas.GetLeft(xs[i]) + xs[i].ActualWidth > canvas1.ActualWidth) return true;
                if (Canvas.GetLeft(xs[i]) < -3) return true;
            }
            return false;
        }

        public bool Is_Piece_Overlapping2(List<Button> xs, List<Button> played)
        {
            for (int i = 0; i < played.Count; i++)
            {
                for(int k =0;k<xs.Count;k++)
                {
                    if(Canvas.GetTop(played[i])==Canvas.GetTop(xs[k]))
                    {
                        if (Canvas.GetLeft(played[i]) + played[i].ActualWidth == Canvas.GetLeft(xs[k]) + xs[k].ActualWidth) return true;
                        if (Canvas.GetLeft(played[i]) == Canvas.GetLeft(xs[k])) return true;
                    }
                }
            }
            return false;
        }

        public bool Is_OnTop_OfPiece(List<Button> xs, List<Button> ys)
        {
            for (int i = 0; i < xs.Count; i++)
            {
                for (int k = 0; k < ys.Count; k++)
                {
                    if ((Canvas.GetLeft(xs[i]) == Canvas.GetLeft(ys[k])) && (Canvas.GetLeft(xs[i])+xs[i].ActualWidth == Canvas.GetLeft(ys[k]) + ys[k].ActualWidth))
                    {
                        if (Canvas.GetTop(xs[i])+xs[i].ActualHeight == Canvas.GetTop(ys[k])) return true;
                    }
                }
            }
            return false;
        }

        public void piece_Properties(List<Button> xs, Button L)
        {
            for (int i = 0; i < xs.Count; i++)
            {
                xs[i].Background = L.Background;
                xs[i].Foreground = null;
                xs[i].BorderBrush = L.BorderBrush;
                xs[i].Width = L.Width;
                xs[i].Height = L.Height;
                xs[i].Focusable = false;
                canvas1.Children.Add(xs[i]);
                Canvas.SetTop(xs[i], -105);
            }
        }

        public void MoveDown(Button x3)
        {
            if ((Canvas.GetTop(x3) + x3.ActualHeight) == canvas1.ActualHeight + 3) return;
            Canvas.SetTop(x3, Canvas.GetTop(x3) + 35);
        }

        public List<int> number_of_buttons_OnRow(List<Button> played)
        {
            List<int> result = new List<int>();
            double row = 0;
            int count = 0;
            while (row <= 682.5)
            {
                for (int i = 0; i < played.Count; i++)
                {
                    if (Canvas.GetTop(played[i]) == row) count++;
                }
                result.Add(count);
                count = 0;
                row += 52.5;
            }
            return result;
        }

        public void Is_Score(List<Button> played)
        {

        loop:;
            int winningNum = (int)canvas1.ActualWidth / 48;
            List<int> numOfButtons = number_of_buttons_OnRow(played);
            for (int i= numOfButtons.Count-1; i>=0;i--)
            {
                if(numOfButtons[i]==winningNum)
                {
                    score += 5*level;
                    IsScore = true;
                    if (score % 10 == 0 && score != 0)
                    {
                        level++;
                    }
                    int count = 0;
                tryAgain:;
                    for (int k=0;k<played.Count;k++)
                    {
                        if(Canvas.GetTop(played[k])==(i*52.5))
                        {
                            Canvas.SetTop(played[k], -225);
                            played.Remove(played[k]);
                            count++;
                            if (k != 0) k--;
                        }
                    }
                    if (count < winningNum) goto tryAgain;
                    int h = i;
                    while (h > 0)
                    {
                        for (int k = 0; k < played.Count; k++)
                        {
                            if (Canvas.GetTop(played[k]) == (h * 52.5) - 52.5)
                            {
                                Canvas.SetTop(played[k], (h * 52.5));
                            }
                        }
                        h--;
                    }
                    goto loop;
                }
            }
            
        }
        
        public List<Button> createPiece(Button B)
        {
            Button p1 = new Button();
            Button p2 = new Button();
            Button p3 = new Button();
            Button p4 = new Button();
            List<Button> result = new List<Button> { p1, p2, p3, p4 };
            piece_Properties(result, B);

            return result;
        }
        
        public bool Is_game_Over(List<Button> played)
        {
            for(int i=0;i<played.Count;i++)
            {
                if (Canvas.GetTop(played[i]) == 0) return true;
            }
            return false;
        }
    }
}
