using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace _2048
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game game { get; set; }

        public Image[] imageBoxList { get; set; }


        public MainWindow()
        {
            InitializeComponent();

            imageBoxList = new Image[] { image1, image2, image3, image4, image5, image6, image7, image8,
                                image9, image10, image11, image12, image13, image14, image15, image16};

            BeginNewGame();
        }
        

        void BeginNewGame()
        {
            game = new Game();
            DisplayGrid();
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            var played = true;

            if (e.Key == Key.Up)
            {
                game.Play('U');
            }
            else if (e.Key == Key.Down)
            {
                game.Play('D');
            }
            else if (e.Key == Key.Right)
            {
                game.Play('R');
            }
            else if (e.Key == Key.Left)
            {
                game.Play('L');
            }
            else
            {
                played = false;
            }

            if (played)
            {
                DisplayGrid();
            }
        }


        private void DisplayGrid()
        {
            var cpt = 0;
            foreach(Tile tile in game.grid.gameGrid)
            {
                imageBoxList[cpt].Source = tile.image;
                cpt++;
            }
        }

        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            BeginNewGame();
        }
    }
}
