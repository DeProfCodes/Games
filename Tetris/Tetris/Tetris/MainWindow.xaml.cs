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
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Windows.Resources;
using System.Timers;


namespace Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Timer timer;
        DispatcherTimer stopWatch;
        Stopwatch sw;

        L_Piece1 L1;
        List<Button> L_piece1;
        L_Piece2 L2;
        List<Button> L_piece2;
        
        Square Sqr;
        List<Button> Sqr_pieces;
        Line line;
        List<Button> Line_pieces;
        
        Four1 four1;
        List<Button> four1_pieces;
        Four2 four2;
        List<Button> four2_pieces;
        
        Tree tree;
        List<Button> tree_pieces;
        
        List<Button> OverallPlayed;
        List<Button> playedPieces;

        Constraints constraint;
        ReadMe t;
        int seconds = 0;
       
        static Uri pathToFile = new Uri("pack://Application:,,,/POL-mr-krabs-short.wav");
        static StreamResourceInfo strm = Application.GetResourceStream(pathToFile);
        SoundPlayer sp = new SoundPlayer(strm.Stream);
        Dispatcher h;

        private bool playSound;
        public MainWindow()
        {
            InitializeComponent();

            initializeGUI_Components();
            instantiate_Timers();
            KeyDown += Window_KeyDow;

            h = Dispatcher.CurrentDispatcher;
            t = new ReadMe();
            constraint = new Constraints(canvas1);
            playedPieces = new List<Button>();
            OverallPlayed = new List<Button>();
            
            if(this.WindowState == System.Windows.WindowState.Normal)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
            }

            playSound = false;

            Closing += Window_Closing;
        }

        private void clangSound()
        {
            Uri pathToFile = new Uri("pack://Application:,,,/clang.wav");
            StreamResourceInfo strm = Application.GetResourceStream(pathToFile);
            SoundPlayer sp = new SoundPlayer(strm.Stream);
            //sp.Play();
        }

        void instantiate_Timers()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Enabled = false;
            timer.Elapsed += DispatcherTimer_Tick;

            stopWatch = new DispatcherTimer();
            stopWatch.Interval = TimeSpan.FromSeconds(1);
            stopWatch.IsEnabled = true;
            stopWatch.Tick += StopWatch_Tick;

            sw = new Stopwatch();
        }

        void initializeGUI_Components()
        {
            GameOver.Visibility = Visibility.Hidden;
            GGAME_OVER.Visibility = Visibility.Hidden;
            
            New_game.Focusable = false;
            Quit.Focusable = false;
            Resume.Focusable = false;
            Pause.Focusable = false;
            Time.Focusable = false;
            textBox.Focusable = false;
            textBox.Visibility = Visibility.Hidden;
            LevelDown.Focusable = false;
            LevelUP.Focusable = false;
            
            LevelDown.Visibility = Visibility.Hidden;
            LevelUP.Visibility = Visibility.Hidden;
        }

        void instatiate_New_Pieces()
        {
            L_piece1 = constraint.createPiece(L_1);
            L1 = new L_Piece1(L_piece1, canvas1, playedPieces);
            L_piece2 = constraint.createPiece(L2_1);
            L2 = new L_Piece2(L_piece2, canvas1, playedPieces);

            Sqr_pieces = constraint.createPiece(Square_1);
            Sqr = new Square(Sqr_pieces, canvas1, playedPieces);
            Line_pieces = constraint.createPiece(Line_1);
            line = new Line(Line_pieces, canvas1, playedPieces);

            four1_pieces = constraint.createPiece(Four1_1);
            four1 = new Four1(four1_pieces, canvas1, playedPieces);
            four2_pieces = constraint.createPiece(Four2_1);
            four2 = new Four2(four2_pieces, canvas1, playedPieces);

            tree_pieces = constraint.createPiece(Tree_1);
            tree = new Tree(tree_pieces, canvas1, playedPieces);

            startingPoint_Overwrite();
        }

        int secondsToChangeSound = 0;
        bool isScore = false;

        private void StopWatch_Tick(object sender, EventArgs e)
        {
            Time.Text = "" + sw.Elapsed;
            seconds++;
            if (constraint.Is_game_Over(playedPieces))
            {
                Canvas.SetTop(Decoy, Canvas.GetTop(Decoy) - 52.5);
            }
            if(isScore)
            {
                secondsToChangeSound++; 
            }
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            h.Invoke(new Action(() => { updateGame(); }));
        }
        
        void update_OverallPlayedPieces(List<Button> xs)
        {
            for (int i = 0; i < xs.Count; i++)
            {
                if (OverallPlayed.IndexOf(xs[i]) == -1) OverallPlayed.Add(xs[i]);
            }
        }

        void update_playedPieces(List<Button> xs)
        {
            for(int i=0;i<xs.Count;i++)
            {
                if (playedPieces.IndexOf(xs[i]) == -1 && OverallPlayed.IndexOf(xs[i])==-1) playedPieces.Add(xs[i]);
            }
        }

        bool IsPiecePlayed2(List<Button> xs)
        {
            
            for(int i=0;i<xs.Count;i++)
            {
                //if (Canvas.GetTop(xs[i]) == -71) return false;
                if (Canvas.GetTop(xs[i]) == -225 || constraint.at_Bottom(xs)) return true;
            }
            return false;
        }

        bool newPiece(List<Button> xs)
        {
            return (!IsPiecePlayed2(xs) && !constraint.Is_OnTop_OfPiece(xs, playedPieces));
        }

        bool IsPiecePlayed(List<Button> xs)
        {
           
            return (IsPiecePlayed2(xs) || ((constraint.at_Bottom(xs)) || constraint.Is_OnTop_OfPiece(xs, playedPieces)));
        }

        int countButtonsRecorded (List<Button> p)
        {
            if (playedPieces.IndexOf(p[0]) != -1 && playedPieces.IndexOf(p[0]) != -1 && playedPieces.IndexOf(p[2]) != -1 && playedPieces.IndexOf(p[3]) != -1) return 4;
            return 0;
        }

        void watch(List<Button> played)
        {
            string result = string.Format(" Pieces recorded : {0}\nTotal piece : {1}\n Pieces per row : {2}",countButtonsRecorded(played),playedPieces.Count,string.Join(",",constraint.number_of_buttons_OnRow(playedPieces)));
            MessageBox.Show(result);
        }
        
        string filePath = "..\\..\\HIGH SCORE.txt";
        void update_HighScore()
        {
            TextReader file = File.OpenText(filePath);
            string line = file.ReadLine();
            string[] contents = line.Split('_');
            High_Score.Content = contents[0];
            name_HS.Content = contents[1];
            file.Close();

            int score = Convert.ToInt32(Score.Content);
            int H_Score = Convert.ToInt32(High_Score.Content);
            if(score>H_Score)
            {
                File.WriteAllText(filePath, "" + score+"_"+name_HS);
                TextReader file1 = File.OpenText(filePath);
                line = file1.ReadLine();
                contents = line.Split('_');
                High_Score.Content = contents[0];
                name_HS.Content = contents[1];
                file1.Close();
            }
            High_Score.Content = contents[0];
        }

        void update_Level()
        {
            int gameSpeed = 0;
            if(constraint.level <=10)
            {
                gameSpeed = 1000-(constraint.level - 1) * 100;
                timer.Interval = gameSpeed;
            }
            if(constraint.level>10)
            {
                gameSpeed = 100 - (constraint.level % 10) * 10;
                timer.Interval = gameSpeed;
            }
        }

        void GUI_layout()
        {
            int multiplier1 = (int)this.ActualWidth / 48;
            int multiplier2 = (int)(this.ActualWidth - canvas1.ActualWidth) / 48;
            canvas1.Width = multiplier1 * 48 - multiplier2 * 48;
            Canvas.SetLeft(Time_label, canvas1.ActualWidth + 6);
            Canvas.SetLeft(Time, canvas1.ActualWidth + 6);
            Canvas.SetLeft(Score_label, canvas1.ActualWidth + 6);
            Canvas.SetLeft(Score, canvas1.ActualWidth + 6);
            Canvas.SetLeft(HighScore_label, canvas1.ActualWidth + 6);
            Canvas.SetLeft(High_Score, canvas1.ActualWidth + 6);
            Canvas.SetLeft(Pause, canvas1.ActualWidth + 6);
            Canvas.SetLeft(Resume, canvas1.ActualWidth + 6);
            Canvas.SetLeft(New_game, canvas1.ActualWidth + 6);
            Canvas.SetLeft(Quit, Canvas.GetLeft(Pause) + Pause.ActualWidth + 5);
            Canvas.SetLeft(Level_label, canvas1.ActualWidth + 6);
            Canvas.SetLeft(Level, Canvas.GetLeft(Level_label)+Level_label.ActualWidth+5);
            Canvas.SetLeft(LevelDown, Canvas.GetLeft(Level_label) + Level_label.ActualWidth - LevelDown.ActualWidth);
            Canvas.SetLeft(LevelUP, Canvas.GetLeft(Level));
        }

        void startingPoint_Overwrite()
        {
            double p1 = canvas1.ActualWidth / 2;
            int p2 = (int)p1 / 48;
            int p3 = p2 * 48;
            Canvas.SetLeft(L_piece1[0], p3);
            Canvas.SetLeft(L_piece2[0], p3);
            Canvas.SetLeft(four1_pieces[0], p3);
            Canvas.SetLeft(four2_pieces[0], p3);
            Canvas.SetLeft(Sqr_pieces[0], p3);
            Canvas.SetLeft(tree_pieces[0], p3);
            Canvas.SetLeft(Line_pieces[0], p3);
        }

        void GUI_updates()
        { 
            Score.Content = constraint.score;
            update_HighScore();
            update_Level();
            Level.Content = "" + constraint.level;
            textBox.Width = canvas1.ActualWidth;
        }

        void canvas_GUI_fix()
        {
            canvas1.Children.Add(Time_label);
            canvas1.Children.Add(Time);
            canvas1.Children.Add(Level);
            canvas1.Children.Add(Level_label);
            canvas1.Children.Add(LevelDown);
            canvas1.Children.Add(LevelUP);
            canvas1.Children.Add(Score_label);
            canvas1.Children.Add(Score);
            canvas1.Children.Add(HighScore_label);
            canvas1.Children.Add(High_Score);
            canvas1.Children.Add(Pause);
            canvas1.Children.Add(Resume);
            canvas1.Children.Add(New_game);
            canvas1.Children.Add(Quit);
            canvas1.Children.Add(textBox);
            canvas1.Children.Add(GameOver);
            canvas1.Children.Add(Welcome);
            canvas1.Children.Add(GGAME_OVER);
        }

        void clean_Canvas()
        {
            canvas1.Children.Clear();
            canvas_GUI_fix();

            playedPieces.Clear();
            OverallPlayed.Clear();
            GC.Collect();
        }

        void gameOver_GUI_Layout()
        {
            GGAME_OVER.Visibility = Visibility.Visible;
            if (Canvas.GetTop(Decoy) == 525)
            {
                GGAME_OVER.Visibility = Visibility.Hidden;
                GameOver.Visibility = Visibility.Visible;
                Congrats_Label.Visibility = Visibility.Hidden;
                name_Prompt.Visibility = Visibility.Hidden;
                Name_Input.Visibility = Visibility.Hidden;
                button1.Visibility = Visibility.Hidden;
                G_Over_HighScore.Content = High_Score.Content; 
                clean_Canvas();
                timer.Enabled = false;
                sw.Stop();
                
                if(Convert.ToInt32(Score.Content)== Convert.ToInt32(High_Score.Content))
                {
                    name_HS.Content = "";
                    Congrats_Label.Visibility = Visibility.Visible;
                    name_Prompt.Visibility = Visibility.Visible;
                    Name_Input.Visibility = Visibility.Visible;
                    Name_Input.Focus();
                    name_HS.Content = Name_Input.Text;
                    File.WriteAllText(filePath, High_Score.Content + "_" + name_HS.Content);
                    button1.Visibility = Visibility.Visible; 
                }
            }
        }

        static Random rng = new Random();
        int generator = rng.Next(1,8);

        int shuffle()
        {
            
            int[] r =  { 1, 2, 3, 4, 5, 6,7 };
            int[] r1 = new int[7];
            for (int i = 0; i < 7; i++) r1[i] = rng.Next();
            Array.Sort(r1, r);

            return (r[rng.Next(0, 7)]);
        }

        void advance_To_NewPiece(List<Button> xs)
        {
            constraint.score += 2;
            generator = shuffle();
            update_playedPieces(xs);
            update_OverallPlayedPieces(xs);
        }
        
        void play_Piece(List<Button> xs)
        {
            if (IsPiecePlayed(xs))
            {
                advance_To_NewPiece(xs);
                instatiate_New_Pieces();
            }
        }
        
        void updateGame()
        {
            GUI_updates();
            constraint.Is_Score(playedPieces);
            
            if (constraint.Is_game_Over(playedPieces))
            {
                gameOver_GUI_Layout();
                return;
            }

            if(constraint.IsScore)
            {
                clangSound();
                constraint.IsScore = false;
                isScore = true;
            }

            if(secondsToChangeSound==3)
            {
                sp.PlayLooping();
                isScore = false;
                secondsToChangeSound = 0;
            }

            {
                if (generator == 1) goto L1a;
                if (generator == 2) goto L2a;
                if (generator == 3) goto Sqrr;
                if (generator == 4) goto Line_a;
                if (generator == 5) goto four11;
                if (generator == 6) goto four22;
                if (generator == 7) goto Tree1;
            }
                
            L1a:;
            {
                L1.play();
                play_Piece(L_piece1);
                return;
            }
        L2a:;
            if (newPiece(L_piece2))
            {
                L2.play();
                play_Piece(L_piece2);
                return;
            }
        Sqrr:;
            if (newPiece(Sqr_pieces))
            {
                Sqr.play();
                play_Piece(Sqr_pieces);
                return;
            }
        Line_a:;
            if (newPiece(Line_pieces))
            {
                line.play();
                play_Piece(Line_pieces);
                return;
            }
        four11:;
            if (newPiece(four1_pieces))
            {
                four1.play();
                play_Piece(four1_pieces);
                return;
            }
        four22:;
            if (newPiece(four2_pieces))
            {
                four2.play();
                play_Piece(four2_pieces);
                return;
            }
        Tree1:;
            if (newPiece(tree_pieces))
            {
                tree.play();
                play_Piece(tree_pieces);
                return;
            }
           
        }

        #region KeyBoard Inputs

        void Key_Left()
        {
            L1.move_Left();
            L2.move_Left();
            Sqr.move_Left();
            line.move_Left();
            four1.move_Left();
            four2.move_Left();
            tree.move_Left();
        }

        void Key_Right()
        {
            L1.move_Right();
            L2.move_Right();
            Sqr.move_Right();
            line.move_Right();
            four1.move_Right();
            four2.move_Right();
            tree.move_Right();
        }

        void Key_Up()
        {
            L1.switchPiece();
            L2.switchPiece();
            line.switchPiece();
            four1.switchPiece();
            four2.switchPiece();
            tree.switchPiece();
        }

        void Key_Down()
        {
            L1.switchPiece2();
            L2.switchPiece2();
            line.switchPiece();
            four1.switchPiece();
            four2.switchPiece();
            tree.switchPiece2();
        }

    #endregion

        void ToggleSoundPlay()
        {
          if(!playSound)
          {
            playSound = true;
            sp.PlayLooping();
          }
          else
          {
            playSound = false;
            sp.Stop();
          }  
        }
        void Window_KeyDow(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.Left:
                    Key_Left();
                    break;
                case Key.Right:
                    Key_Right();
                    break;
                case Key.Up:
                    Key_Up();        
                    break;
                case Key.Down:
                    Key_Down();
                    break;
                case Key.Escape:
                    Application.Current.Shutdown();
                    break;
                case Key.P:
                    pauseGame();
                    break;
                case Key.R:
                    resume();
                    break;
                case Key.OemMinus:
                    level_Down();
                    break;
                case Key.OemPlus:
                    level_UP();
                    break;
                case Key.H:
                  ToggleSoundPlay();
                  break;
            }
        }
        
        void startGame()
        {
            GUI_updates();

            Welcome.Visibility = Visibility.Hidden;
            timer.Enabled = true;
            timer.Interval = 1000;
            constraint.level = 1;
            constraint.score = 0;
            Resume.IsEnabled = false;
            Level.Content = "1";
            sw.Reset();
            sw.Start();
            t.Close();

            instatiate_New_Pieces();
            playedPieces.Clear();
            OverallPlayed.Clear();
            GUI_layout();
            startingPoint_Overwrite();
            generator = shuffle();

            LevelUP.Visibility = Visibility.Visible;
            LevelDown.Visibility = Visibility.Visible;
            textBox.Visibility = Visibility.Visible;
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            startGame();
        }

        private void New_game_Click(object sender, RoutedEventArgs e)
        {
            timer.Enabled = false;
            sw.Stop();
            if (MessageBox.Show("Are you sure you want to start a new game?", "Start new game", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                clean_Canvas();
                startGame();
            }
            else
            {
                timer.Enabled = true;
                sw.Start();
            }
        }

        void pauseGame()
        {
            Pause.IsEnabled = false;
            Resume.IsEnabled = true;
            timer.Enabled = false;
            sw.Stop();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            pauseGame();
        }

        void QuitGame()
        {
            timer.Enabled = false;
            sw.Stop();

            if (MessageBox.Show("Are you sure you want to quit?", "Quit game", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
            else
            {
                timer.Enabled = true;
                sw.Start();
            }
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            QuitGame();
        }

        void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            QuitGame();
        }

        void resume()
        {
            Pause.IsEnabled = true;
            Resume.IsEnabled = false;
            timer.Enabled = true;
            sw.Start();
        }

        private void Resume_Click(object sender, RoutedEventArgs e)
        {
            resume();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            QuitGame();
        }

        void level_Down()
        {
            if (constraint.level > 1) constraint.level--;
        }

        void level_UP()
        {
           if(constraint.level<19) constraint.level++;
        }

        private void LevelDown_Click(object sender, RoutedEventArgs e)
        {
            level_Down();   
        }

        private void LevelUP_Click(object sender, RoutedEventArgs e)
        {
            level_UP();
        }

        private void gameOver_start_Click(object sender, RoutedEventArgs e)
        {
            GameOver.Visibility = Visibility.Hidden;
            Welcome.Visibility = Visibility.Visible;
        }

        private void gameOver_Quit_Click(object sender, RoutedEventArgs e)
        {
            QuitGame();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (Name_Input.Text!="")
            {
                name_HS.Content = Name_Input.Text;
                File.WriteAllText(filePath, High_Score.Content + "_" + name_HS.Content);
            }
            else
            {
                MessageBox.Show("Enter your name in the box provided");
                Name_Input.Focus();
            }
        }

        private void ReadMe_Click(object sender, RoutedEventArgs e)
        {
            t.Show();
            t.Owner = this;
        }
    }
}
