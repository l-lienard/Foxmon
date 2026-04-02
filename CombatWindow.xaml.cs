using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Input;
using System.Media;
using System.Diagnostics; // 🔊 pour les sons

namespace Foxmon
{
    public partial class CombatWindow : Window
    {
        private Dresseur dresseur;
        private FoxmonCreature monFoxmon;
        private FoxmonCreature ennemi;
        private GameWindow gameWindow;

        // 🔊 Sons Pokémon
        private SoundPlayer sonAttaque        = new SoundPlayer("Sounds/attaque.wav");
        private SoundPlayer sonDegats         = new SoundPlayer("Sounds/degats.wav");
        private SoundPlayer sonFuite          = new SoundPlayer("Sounds/fuite.wav");
        private SoundPlayer sonCaptureReussie = new SoundPlayer("Sounds/capture_reussie.wav");
        private SoundPlayer sonCaptureFail    = new SoundPlayer("Sounds/capture_fail.wav");

        private string EmojiType(string type) => type switch
        {
            "Feu" => "🔥",
            "Eau" => "💧",
            "Plante" => "🌿",
            _ => "⭐"
        };

        public CombatWindow(Dresseur d, FoxmonCreature e, GameWindow g)
        {
            InitializeComponent();

            dresseur = d;
            ennemi = e;
            gameWindow = g;

            if (dresseur.Equipe.Count == 0)
            {
                MessageBox.Show("Aucun Foxmon !");
                Close();
                return;
            }

            monFoxmon = dresseur.Equipe[0];

            MettreAJourUI();
            ChargerAttaques();
            LogCombat.Text = $"Un {ennemi.Nom} sauvage apparaît !";
        }

        private void ChargerAttaques()
        {
            AttaquesPanel.Children.Clear();

            foreach (var atk in monFoxmon.Attaques)
                AttaquesPanel.Children.Add(CreerBoutonAttaque(atk));
        }

        private Button CreerBoutonAttaque(Attaque atk)
        {
            var btn = new Button
            {
                Content = $"⚔️ {atk.Nom}",
                Width = 150,
                Height = 40,
                Margin = new Thickness(5),
                Background = Brushes.DarkSlateBlue,
                Foreground = Brushes.White,
                Cursor = Cursors.Hand,
                Tag = atk
            };

            btn.Click += BtnAttaque_Click;
            return btn;
        }

        private async void BtnAttaque_Click(object sender, RoutedEventArgs e)
        {
            foreach (Button b in AttaquesPanel.Children)
                b.IsEnabled = false;

            var attaque = (sender as Button)?.Tag as Attaque;

            if (GameSettings.SonsActives)
                sonAttaque.Play();

            await AnimerAttaqueJoueur();

            int degats = monFoxmon.CalculerDegats(ennemi, attaque);
            ennemi.SubirDegats(degats);

            if (GameSettings.SonsActives)
                sonDegats.Play();

            await AnimerDegatsEnnemi();

            LogCombat.Text = $"{monFoxmon.Nom} inflige {degats} dégâts !";

            if (ennemi.EstKO())
            {
                MettreAJourUI();
                if (GameSettings.SonsActives)
                    sonCaptureReussie.Play(); // Victoire = capture réussie

                await FinCombat($"🏆 {ennemi.Nom} est KO ! Victoire !");
                return;
            }

            await Task.Delay(500);

            int d2 = ennemi.CalculerDegats(monFoxmon);
            monFoxmon.SubirDegats(d2);

            if (GameSettings.SonsActives)
                sonDegats.Play();

            LogCombat.Text += $"\n{ennemi.Nom} riposte ({d2})";

            MettreAJourUI();

            foreach (Button b in AttaquesPanel.Children)
                b.IsEnabled = true;

            if (monFoxmon.EstKO())
                await FinCombat($"💀 {monFoxmon.Nom} est KO... Défaite !");
        }

        private void MettreAJourUI()
        {
            NomJoueur.Text = monFoxmon.Nom;
            EmojiJoueur.Text = EmojiType(monFoxmon.TypeElementaire);
            PVJoueur.Value = (double)monFoxmon.PV / monFoxmon.PVMax * 100;

            NomEnnemi.Text = ennemi.Nom;
            EmojiEnnemi.Text = EmojiType(ennemi.TypeElementaire);
            PVEnnemi.Value = (double)ennemi.PV / ennemi.PVMax * 100;
        }

        private async Task AnimerAttaqueJoueur()
        {
            var anim = new DoubleAnimation(0, 40, TimeSpan.FromMilliseconds(120))
            {
                AutoReverse = true
            };
            TransformJoueur.BeginAnimation(TranslateTransform.XProperty, anim);
            await Task.Delay(200);
        }

        private async Task AnimerDegatsEnnemi()
        {
            EmojiEnnemi.Foreground = Brushes.Red;
            await Task.Delay(150);
            EmojiEnnemi.Foreground = Brushes.White;
        }

        private async Task FinCombat(string msg)
        {
            LogCombat.Text = msg;
            await Task.Delay(2000);
            gameWindow.Show();
            Close();
        }

        private void BtnCapturer_Click(object sender, RoutedEventArgs e)
        {
            if (ennemi.PV > ennemi.PVMax * 0.3)
            {
                LogCombat.Text = "⚠️ Affaiblis le FoxMon avant de capturer !";
                return;
            }

            if (CombatManager.TenterCapture(ennemi))
            {
                if (dresseur.Equipe.Count < 6)
                {
                    dresseur.Equipe.Add(ennemi);
                    if (GameSettings.SonsActives)
                        sonCaptureReussie.Play();

                    FinCombat($"🎉 {ennemi.Nom} a été capturé !");
                }
                else
                {
                    if (GameSettings.SonsActives)
                        sonCaptureFail.Play();

                    FinCombat($"🎉 {ennemi.Nom} capturé mais équipe pleine !");
                }
            }
            else
            {
                LogCombat.Text = $"😤 {ennemi.Nom} s'est échappé !";
                if (GameSettings.SonsActives)
                    sonCaptureFail.Play();
            }
        }

        private void BtnFuir_Click(object sender, RoutedEventArgs e)
        {
            if (CombatManager.TenterFuite())
            {
                if (GameSettings.SonsActives)
                    sonFuite.Play();

                FinCombat("🏃 Vous avez pris la fuite !");
            }
            else
            {
                LogCombat.Text = "❌ Impossible de fuir !";
            }
        }
    }
}

