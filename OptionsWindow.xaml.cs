using System.Windows;

namespace Foxmon
{
    public partial class OptionsWindow : Window
    {
        public OptionsWindow()
        {
            InitializeComponent();

            // Charger les paramètres actuels
            ChkSon.IsChecked = GameSettings.SonsActives;
            SliderVolume.Value = GameSettings.Volume;
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            // Sauvegarde
            GameSettings.SonsActives = ChkSon.IsChecked == true;
            GameSettings.Volume = SliderVolume.Value;

            Close();
        }
    }
}