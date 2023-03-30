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

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();//Creates a new timer
        int tenthOfSecondsElapsed;//Keeps track of time elapsed
        int matchesFound;//Number of matches the player has found
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetupGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthOfSecondsElapsed++;
            timeTextBlock.Text = (tenthOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
        }

        private void SetupGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "🐮","🐮",
                "🐪","🐪",
                "🐋","🐋",
                "🦕","🦕",
                "🦥","🦥",
                "🦮","🦮",
                "🦚","🦚",
                "🐅","🐅",
            };
            Random random = new Random();
            
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>()) 
            {
                if (textBlock.Name != "timeTextBlock")//Skips the TextBlock that holds the timer in our window
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }
            }

            timer.Start();//Start the timer when the game starts
            tenthOfSecondsElapsed = 0;
            matchesFound = 0;//Set the number of matches that the player has found so far to 0
        }

        TextBlock lastTextBlockClicked;
        bool findingMatch = false;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;

            if(findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }

            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }

            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(matchesFound == 8)//Resets the game if all 8 matched pairs have been found(Otherwise it does nothing because the game is still running).
            {
                SetupGame();
            }
        }
    }
}
