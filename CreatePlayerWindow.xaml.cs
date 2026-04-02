using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input; // ← pour Cursors

namespace Foxmon
{
    public partial class CreatePlayerWindow : Window
    {
        private string emojiChoisi = "";

        private List<string> emojisDisponibles = new List<string>
        {
            "🧑", "👦", "👧", "🧒", "👨", "👩",
            "🧙", "🧝", "🧛", "🧟", "🥷", "🦸"
        };

        public static Dresseur DresseurCree { get; private set; }

        public CreatePlayerWindow()
        {
            InitializeComponent();
            ChargerEmojis();
        }

        private void ChargerEmojis()
        {
            foreach (var emoji in emojisDisponibles)
            {
                var btn = new Button
                {
                    Content = emoji,
                    FontSize = 32,
                    Width = 60,
                    Height = 60,
                    Margin = new Thickness(5),
                    Background = new SolidColorBrush(Color.FromRgb(42, 42, 78)),
                    Foreground = Brushes.White,
                    BorderBrush = new SolidColorBrush(Color.FromRgb(100, 50, 180)),
                    BorderThickness = new Thickness(2),
                    Cursor = Cursors.Hand,
                    Tag = emoji
                };
                btn.Click += EmojiBtn_Click;
                EmojiPanel.Children.Add(btn);
            }
        }

        private void EmojiBtn_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            emojiChoisi = btn.Tag.ToString();
            EmojiSelectionne.Text = $"Personnage choisi : {emojiChoisi}";

            // Highlight sélection
            foreach (Button b in EmojiPanel.Children)
                b.BorderBrush = new SolidColorBrush(Color.FromRgb(100, 50, 180));

            btn.BorderBrush = Brushes.White;
        }

        private void IntroVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            IntroVideo.Visibility = Visibility.Collapsed;
            CreationPanel.Visibility = Visibility.Visible;
        }

        private async void CommencerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NomInput.Text))
            {
                EmojiSelectionne.Text = "⚠️ Entre ton nom !";
                return;
            }
            if (string.IsNullOrEmpty(emojiChoisi))
            {
                EmojiSelectionne.Text = "⚠️ Choisis un personnage !";
                return;
            }

            // Écran de chargement
            CreationPanel.Visibility = Visibility.Collapsed;
            LoadingPanel.Visibility = Visibility.Visible;

            for (int i = 0; i <= 100; i += 10)
            {
                LoadingBar.Value = i;
                await Task.Delay(150);
            }

            DresseurCree = new Dresseur(NomInput.Text, emojiChoisi);
            var flamby = new FoxmonCreature("Flamby", 30, 5, 8, "Feu", 10);
            flamby.Attaques.Add(new Attaque("Flammèche",  10, "Feu"));
            flamby.Attaques.Add(new Attaque("Braise",     14, "Feu"));
            flamby.Attaques.Add(new Attaque("Charge",      8, "Normal"));
            flamby.Attaques.Add(new Attaque("Rugissement",  5, "Normal"));
            DresseurCree.Equipe.Add(flamby);

            GameWindow game = new GameWindow(DresseurCree);
            game.Show();
            this.Close();
        }
    }
}