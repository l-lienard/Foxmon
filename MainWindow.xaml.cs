using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Foxmon;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

private void JouerButton_Click(object sender, RoutedEventArgs e)
{
    CreatePlayerWindow createPlayer = new CreatePlayerWindow();
    createPlayer.Show();
    this.Close();
}

private void QuitButton_Click(object sender, RoutedEventArgs e)
{
    Application.Current.Shutdown();
}

private void OptionsButton_Click(object sender, RoutedEventArgs e)
{
    OptionsWindow opt = new OptionsWindow();
    opt.ShowDialog();
}
}