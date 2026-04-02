using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Foxmon
{
    public partial class GameWindow : Window
    {
        private Carte carte = new Carte();

        private Dresseur dresseur;

        public GameWindow(Dresseur dresseur)
        {
            InitializeComponent();
            this.dresseur = dresseur;
            AfficherCarte();
        }

        private void AfficherCarte()
        {
            CarteDisplay.Items.Clear();

            for (int y = 0; y < carte.Height; y++)
            {
                StackPanel ligne = new StackPanel { Orientation = Orientation.Horizontal };

                for (int x = 0; x < carte.Width; x++)
                {
                    char c = carte.Grille[x, y];
                    string texte;
                    Brush couleur;

                    if (x == carte.X && y == carte.Y)
                    {
                        texte = dresseur.Emoji;
                        couleur = Brushes.Orange;
                    }
                    else
                    {
                        switch (c)
                        {
                            case '#':
                                texte = "🌲";
                                couleur = Brushes.Green;
                                break;
                            case '*':
                                texte = "🌿";
                                couleur = Brushes.LightGreen;
                                break;
                            case 'C':
                                texte = "🏠";
                                couleur = Brushes.Brown;
                                break;
                            default:
                                texte = "  ";
                                couleur = Brushes.LightGray;
                                break;
                        }
                    }

                    ligne.Children.Add(new TextBlock
                    {
                        Text = texte,
                        Foreground = couleur,
                        FontSize = 18,
                        FontFamily = new FontFamily("Segoe UI Emoji"),
                        Width = 24,
                        TextAlignment = TextAlignment.Center
                    });
                }

                CarteDisplay.Items.Add(ligne);
            }
        }

                private void Window_KeyDown(object sender, KeyEventArgs e)
                    {
                        int newX = carte.X;
                        int newY = carte.Y;

                        if (e.Key == Key.Up)    newY--;
                        if (e.Key == Key.Down)  newY++;
                        if (e.Key == Key.Left)  newX--;
                        if (e.Key == Key.Right) newX++;

                        if (carte.Grille[newX, newY] == '#') return;

                        carte.X = newX;
                        carte.Y = newY;

                        // Bâtiment = centre de soin
                        if (carte.EstBatiment(newX, newY))
                        {
                            SoignerEquipe();
                            return;
                        }

                        // Combat uniquement sur haute herbe (*)
                        if (carte.EstHauteHerbe(newX, newY) && CombatManager.TenterRencontre())
                        {
                            if (dresseur.Equipe.Count > 0)
                            {
                                var ennemi = CombatManager.GenererSauvage();
                                var combat = new CombatWindow(dresseur, ennemi, this);
                                this.Hide();
                                combat.Show();
                                return;
                            }
                        }

                        AfficherCarte();
                    }

                    private void SoignerEquipe()
                    {
                        foreach (var f in dresseur.Equipe)
                            f.Soigner();

                        // Affiche un message temporaire
                        MessageBox.Show("🏥 Toute ton équipe a été soignée !", "Centre de soin",
                                        MessageBoxButton.OK, MessageBoxImage.Information);
                        AfficherCarte();
                    }
    }
}